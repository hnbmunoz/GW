using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models.Authentication;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MLAB.PlayerEngagement.Core.Logging;

namespace MLAB.PlayerEngagement.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserFactor _userFactory;
    private readonly ILogger<MessagePublisherService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly int _loggedInUserId = 0;
    private string currentUserFullName = "";
    private string mcoreUserId = "";

    public AuthenticationService(IMediator mediator, IConfiguration configuration, ILogger<MessagePublisherService> logger, IUserFactor userFactory, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _logger = logger;
        _userFactory = userFactory;
        _mediator = mediator;
        int.TryParse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value, out _loggedInUserId);
    }

    public async Task<LoginResponse> VerifyAccountAsync(LoginRequest request)
    {

        var adminAccess = await VerifyAdminAccountAccessAsync(request);

        if (adminAccess != null)
        {
            return adminAccess;
        }

        var user = await _userFactory.VerifyAccountAsync(request);      


        if(user != null && user.UserStatus == UserStatus.Active && user.IsValidUser)
        {
            currentUserFullName = user.FullName;
            mcoreUserId = user.MCoreUserId;
            return await GetUserModulePermissionAsync(user.UserId);
        }
        else if (user != null && user.UserStatus == UserStatus.Locked && user.IsCurrentlyLocked)
            throw new Exception("Your account is currently locked, please wait for 5 minutes or contact administrator for further assistance.");
        else if (user != null && user.UserStatus == UserStatus.Inactive)
            throw new Exception("Your account is inactive, please contact administrator for further assistance.");
        else
            throw new Exception("Incorrect Email Address or Password, please click reset password if you forgot your password or contact administrator for further assistance.");


    }

    public async Task<bool> ValidateEmailAsync(string email)
    {
        return await _userFactory.ValidateEmailAsync(email);
    }
    public async Task<bool> UpdateUserStatusByIdAsync(UpdateUserOnlineStatusRequest request)
    {
        return await _userFactory.UpdateUserStatusByIdAsync(request);

    }
    public async Task<bool> ActivateAccountAsync(UserStatusRequest request)
    {
        var updateUserStatusRequest = new UpdateUserStatusRequest()
        {
            UserId = request.UserId,
            UserStatus = UserStatus.Active,
            UpdatedBy = _loggedInUserId
        };

        return await _userFactory.UpdateUserStatusAsync(updateUserStatusRequest);

    }

    public async Task<bool> DeactivateAccountAsync(UserStatusRequest request)
    {
        var updateUserStatusRequest = new UpdateUserStatusRequest()
        {
            UserId = request.UserId,
            UserStatus = UserStatus.Inactive,
            UpdatedBy = _loggedInUserId
        };

        return await _userFactory.UpdateUserStatusAsync(updateUserStatusRequest);
    }

    public async Task<LoginResponse> GetUserModulePermissionAsync(int userId)
    {

        var permission = await _userFactory.GetUserModulePermissionAsync(userId);

        if(permission != null)
        {
            string combinedAccess = string.Join("|", permission.Select(i => i.Access));
            string[] strArrayOne = new string[] { };
            strArrayOne = combinedAccess.Split('|');
            var distinctList = strArrayOne.Distinct();

            var access = string.Join("|", distinctList);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWTKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ModulePermissions.ClaimType, access)
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["AuthTokenExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponse()
            {
                UserId = userId,
                Access = access,
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = tokenDescriptor.Expires,
                FullName = currentUserFullName,
                MCoreUserId = mcoreUserId
            };

        }
        else
        {
            throw new Exception("");
        }
    }

    public async Task<LoginResponse> VerifyAdminAccountAccessAsync(LoginRequest request)
    {

        var access = await _userFactory.VerifyAdminAccountAccessAsync(request);


        // Check if account is admin
        if (access != null)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWTKey"]);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "0"),
                    new Claim(ModulePermissions.ClaimType, access.Access)
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["AuthTokenExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponse()
            {
                UserId = 0,
                Access = access.Access,
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = tokenDescriptor.Expires,
                FullName = currentUserFullName
            };
        }
        else
        {
            return Enumerable.Empty<LoginResponse>().FirstOrDefault();
        }

    }

    public Task<bool> VerifyUserChatByProviderAsync(VerifyUserChatProviderRequest request)
    {
        return _userFactory.VerifyUserChatByProviderAsync(request);
    }

}

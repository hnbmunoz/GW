using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models.Authentication;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Gateway.Attributes;
using System.Net;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMessagePublisherService _messagePublisherService;

    public AuthenticationController(IAuthenticationService authenticationService, IMessagePublisherService messagePublisherService)
    {
        _authenticationService = authenticationService;
        _messagePublisherService = messagePublisherService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        try
        {
            var verifyResponse = await _authenticationService.VerifyAccountAsync(request);
            return Ok(verifyResponse);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [ModulePermissionAttribute(ModulePermissions.UserManagement_Permission_Write)]
    [HttpPost]
    public async Task<IActionResult> ActivateAccountAsync([FromBody] UserStatusRequest request)
    {
        try
        {
            bool isSuccess = await _authenticationService.ActivateAccountAsync(request);

            if (isSuccess)
                return Ok();
            else
                return BadRequest(new { message = "Invalid User Id" });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [ModulePermissionAttribute(ModulePermissions.UserManagement_Permission_Write)]
    [HttpPost]
    public async Task<IActionResult> DeactivateAccountAsync([FromBody] UserStatusRequest request)
    {
        try
        {
            bool isSuccess = await _authenticationService.DeactivateAccountAsync(request);

            if (isSuccess)
                return Ok();
            else
                return BadRequest(new { message = "Invalid User Id" });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserStatusByIdAsync([FromBody] UpdateUserOnlineStatusRequest request)
    {
        try
        {
            bool isSuccess = await _authenticationService.UpdateUserStatusByIdAsync(request);

            if (isSuccess)
                return Ok();            else
                return BadRequest(new { message = "Invalid User Id" });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ResponseModel> CreateNewPasswordAsync([FromBody] CreateNewPasswordRequest request)
    {
        var result = await _messagePublisherService.CreatePasswordAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ResponseModel> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
    {
        var isEmailExists = await _authenticationService.ValidateEmailAsync(request.Email);
        if (isEmailExists)
        {
            var result = await _messagePublisherService.ResetPasswordAsync(request);
            if (result)
            {
                return new ResponseModel();
            }
            else
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
            }
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.BadRequest, "Email address not found, please input the correct email address or contact administrator");
        }            
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> VerifyUserChatByProviderAsync([FromBody] VerifyUserChatProviderRequest request)
    {
        var result = await _authenticationService.VerifyUserChatByProviderAsync(request);

        if (result)
           return Ok();
        else
            return BadRequest(new { message = "Unable to login with different account" });
    }

}

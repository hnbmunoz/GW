using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models.Authentication;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.Option;
using Newtonsoft.Json;
using MLAB.PlayerEngagement.Core.Models.Users.Udt;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class UserFactor : IUserFactor
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserFactor> _logger;

    #region Constructor
    public UserFactor(IMainDbFactory mainDbFactory, IConfiguration configuration, ILogger<UserFactor> logger)
    {
        _mainDbFactory = mainDbFactory;
        _configuration = configuration;
        _logger = logger;
    }
    #endregion

    public async Task<VerifyAccountResponse> VerifyAccountAsync(LoginRequest login)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | VerifyAccountAsync - [login: {login}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<VerifyAccountResponse>
                            (   DatabaseFactories.UserManagementDb,
                                StoredProcedures.USP_VerifyAccount, new
                                {
                                    Email = login.Email,
                                    AccountPwd = login.Password,
                                    MaxAttempts = int.Parse(_configuration["MaxLoginAttempts"])
                                }

                            ).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | VerifyAccountAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<VerifyAccountResponse>().FirstOrDefault();
    }

    public async Task<List<UserModulePermissionResponse>> GetUserModulePermissionAsync(int userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | GetUserModulePermissionAsync - [userId: {userId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<UserModulePermissionResponse>
                            (
                                DatabaseFactories.UserManagementDb,
                                StoredProcedures.USP_GetUserModulePermission, new
                                {
                                    UserId = userId
                                }

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetUserModulePermissionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<UserModulePermissionResponse>().ToList();
    }

    public async Task<bool> UpdateUserStatusAsync(UpdateUserStatusRequest request)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | UpdateUserStatusAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<int>
                            (
                                DatabaseFactories.UserManagementDb,
                                StoredProcedures.USP_UpdateUserStatus, new
                                {
                                    UserId = request.UserId,
                                    UserStatusId = (int)request.UserStatus,
                                    UpdatedBy = request.UpdatedBy
                                }

                            ).ConfigureAwait(false);
            return result.FirstOrDefault() == 1;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | UpdateUserStatusAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> UpdateUserStatusByIdAsync(UpdateUserOnlineStatusRequest request)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | UpdateUserStatusByIdAsync | [Request] - {JsonConvert.SerializeObject(request)}");

            await _mainDbFactory.ExecuteQueryAsync<int>
                (
                    DatabaseFactories.UserManagementDb,
                    StoredProcedures.USP_UpdateUserStatusById, new
                    {
                        UserId = request.UserId,
                        IsOnline = request.IsOnline
                    }

                ).ConfigureAwait(false);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | UpdateUserStatusByIdAsync | [Exception] - {ex.Message} | [Request] - {JsonConvert.SerializeObject(request)}");
        }
        return false;
    }
    

    public async Task<List<RoleModel>> GetRolesFilterAsync()
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<RoleModel>
                (
                    DatabaseFactories.UserManagementDb,
                    StoredProcedures.Usp_GetRole,
                    null
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetRolesFilterAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<RoleModel>().ToList();
    }

    public async Task<List<TeamModel>> GetTeamsFilterAsync()
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<TeamModel>
                (
                    DatabaseFactories.UserManagementDb,
                    StoredProcedures.Usp_GetTeam,
                    null
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetTeamsFilterAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<TeamModel>().ToList();

    }

    public async Task<List<LookupModel>> GetAgentsForTagging()
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<LookupModel>
                (
                    DatabaseFactories.UserManagementDb,
                    StoredProcedures.USP_GetAgentsForTagging,
                    null
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetAgentsForTagging : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<LookupModel>> GetUserLookupsAsync(string filter)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | GetUserLookupsAsync - [filter: {filter}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LookupModel>
                            (
                                DatabaseFactories.UserManagementDb,
                                StoredProcedures.USP_GetUserLookups, new
                                {
                                    Filter = filter
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetUserLookupsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<UserListOptionModel>> GetUserListOptionAsync()
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<UserListOptionModel>
                (
                    DatabaseFactories.UserManagementDb,
                    StoredProcedures.Usp_GetUserByFilter,
                    null
                ).ConfigureAwait(false);

            return result.ToList(); 
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | ValidateEmailAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<UserListOptionModel>().ToList();
    }

    public async Task<List<LookupModel>> GetCommProviderUserListOptionAsync()
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<LookupModel>
                (
                    DatabaseFactories.UserManagementDb,
                    StoredProcedures.USP_GetUsersWithLivePersonCommProvider,
                    null
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetCommProviderUserListOptionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }
    public async Task<List<LookupModel>> GetTeamListByUserIdOptionAsync(long userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | GetTeamListByUserIdOptionAsync : Request: UserId - {userId}");
            var result = await _mainDbFactory
                .ExecuteQueryAsync<LookupModel>
                (
                    DatabaseFactories.UserManagementDb,
                    StoredProcedures.USP_GetTeamsByUserId, new
                    {
                        UserId = userId
                    }
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetTeamListByUserIdOptionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }
    public async Task<bool> ValidateEmailAsync(string email)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<object>
                            (
                                DatabaseFactories.UserManagementDb,
                                StoredProcedures.Usp_GetUserByFilter, new
                                {
                                    Email = email,
                                }

                            ).ConfigureAwait(false);
            return result.Any();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | ValidateEmailAsync : [Exception] - {ex.Message}");
            return false;
        }
    }

    public async Task<AdminAccessResponse> VerifyAdminAccountAccessAsync(LoginRequest request)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<AdminAccessResponse>
            (
                DatabaseFactories.UserManagementDb,
                StoredProcedures.USP_VerifyAdminAccountAccess, new
                {
                    Email = request.Email,
                    AccountPwd = request.Password
                }

            ).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | VerifyAdminAccountAccessAsync : [Exception] - {ex.Message}");
            return Enumerable.Empty<AdminAccessResponse>().FirstOrDefault();
        }
    }

    public async Task<List<UserOptionModel>> GetUserOptionsAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<UserOptionModel>(
                DatabaseFactories.UserManagementDb,
                StoredProcedures.USP_GetUserOptions, null
            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetUserOptions : [Exception] - {ex.Message}");

            return Enumerable.Empty<UserOptionModel>().ToList();
        }
    }

    public async Task<bool> VerifyUserChatByProviderAsync(VerifyUserChatProviderRequest request)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<int>
                            (
                                DatabaseFactories.UserManagementDb,
                                StoredProcedures.USP_ValidateUserChatByProvider, new
                                {
                                    UserId = request.UserId,
                                    ProviderAccount = request.ProviderAccount,
                                    ProviderId = request.ProviderId,
                                }
                            ).ConfigureAwait(false);
            return result.FirstOrDefault() == 1;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | VerifyUserChatByProviderAsync : [Exception] - {ex.Message}");
            return false;
        }
    }

    public async Task<List<CommunicationProviderAccountUdt>> GetCommunicationProviderAccountListByUserIdAsync(int userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | paramter: {userId} |GetCommunicationProviderAccountListByUserIdAsync");
            var result = await _mainDbFactory.ExecuteQueryAsync<CommunicationProviderAccountUdt>(
                DatabaseFactories.UserManagementDb,
                StoredProcedures.Usp_GetCommunicationProviderAccountListByUserId, new
                {
                    UserId = userId
                }
            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetCommunicationProviderAccountListByUserIdAsync : [Exception] - {ex.Message}");

            return Enumerable.Empty<CommunicationProviderAccountUdt>().ToList();
        }
       
    }

    public async Task<List<DataRestrictionAccessModel>> GetDataRestrictionDetailsByUserIdAsync(long userId)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<DataRestrictionAccessModel>(
                DatabaseFactories.UserManagementDb,
                StoredProcedures.USP_GetDataRestrictionDetailsPerUserId, new
                {
                    UserId = userId
                }
            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | GetDataRestrictionDetailsByUserIdAsync : [Exception] - {ex.Message}");

            return Enumerable.Empty<DataRestrictionAccessModel>().ToList();
        }

    }

    public async Task<bool> SetUserIdleAsync(string userId, bool isIdle)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | SetUserIdle - [userId: {userId} , isIdle: {isIdle}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<int>
                            (DatabaseFactories.UserManagementDb,
                                StoredProcedures.USP_SetUserIdleStatus, new
                                {
                                    UserId = userId,
                                    IsIdle = isIdle,
                                }
                            ).ConfigureAwait(false);
            _logger.LogInfo($"{Factories.UserFactor} | SetUserIdle - Result: {result} ]");

            return result.FirstOrDefault() == 1;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | SetUserIdle : [Exception] - {ex.Message}");
            return false;
        }
    }
}

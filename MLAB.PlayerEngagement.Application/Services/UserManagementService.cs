using MediatR;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Constants;
using Newtonsoft.Json;
using MLAB.PlayerEngagement.Core.Models.Users.Request;
using MLAB.PlayerEngagement.Core.Models.Users.Udt;

namespace MLAB.PlayerEngagement.Application.Services;

public  class UserManagementService : IUserManagementService
{

    private readonly ILogger<UserManagementService> _logger;
    private readonly IUserFactor _userFactor;
        private readonly IMainDbFactory _mainDbFactory;
        public UserManagementService(IMediator mediator, ILogger<UserManagementService> logger, IUserFactor userFactor, IMainDbFactory mainDbFactory)
    {
        _logger = logger;
        _userFactor = userFactor;
        _mainDbFactory = mainDbFactory;
    }

    public async Task<List<RoleModel>> GetRolesFilterAsync()
    {
        var result = await _userFactor.GetRolesFilterAsync();
        return result;
    }

    public async Task<List<TeamModel>> GetTeamsFilterAsync()
    {
        var result = await _userFactor.GetTeamsFilterAsync();
        return result;
    }

    public async Task<List<LookupModel>> GetAgentsForTagging()
    {
        var result = await _userFactor.GetAgentsForTagging();
        return result;
    }

    public async Task<List<LookupModel>> GetUserLookupsAsync(string filter)
    {
        var result = await _userFactor.GetUserLookupsAsync(filter);
        return result;
    }

    public async Task<List<UserListOptionModel>> GetUserListOptionAsync()
    {
        var result = await _userFactor.GetUserListOptionAsync();
        return result;
    }
    public async Task<List<LookupModel>> GetCommProviderUserListOptionAsync()
    {
        var result = await _userFactor.GetCommProviderUserListOptionAsync();
        return result;
    }
    public async Task<List<LookupModel>> GetTeamListByUserIdOptionAsync(long userId)
    {
        var result = await _userFactor.GetTeamListByUserIdOptionAsync(userId);
        return result;
    }
    public async Task<List<UserOptionModel>> GetUserOptionsAsync()
    {
        var result = await _userFactor.GetUserOptionsAsync();
        return result;
        }

    public async Task<bool> ValidateUserProviderNameAsync(UserProviderRequestModel request)
    {
        try
            {
                _logger.LogInfo($"{Factories.UserFactor} | ValidateUserProviderNameAsync - {JsonConvert.SerializeObject(request)}");

                var result = await _mainDbFactory
                            .ExecuteQuerySingleOrDefaultAsync<bool>
                                (   DatabaseFactories.UserManagementDb,
                                    StoredProcedures.Usp_ValidateUserProviderName, new
                                    {
                                        UserId = request.UserId,
                                        ProviderId = request.ProviderId,
                                        ProviderAccount = request.ProviderAccount
                                    }

                                ).ConfigureAwait(false);
                return result;
            }
         catch (Exception ex)
            {
                _logger.LogError($"{Factories.UserFactor} | ValidateUserProviderNameAsync : [Exception] - {ex.Message}");
            }
        return false;
    }

    public async Task<bool> ValidateCommunicationProviderAsync(CommunicationProviderRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | ValidateCommunicationProviderAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (   DatabaseFactories.UserManagementDb,
                                StoredProcedures.Usp_ValidateCommunicationProvider, new
                                {
                                    UserId = request.UserId,
                                    ProviderId = request.ProviderId,
                                    ProviderAccount = request.ProviderAccount,
                                    Action = request.Action
                                }

                            ).ConfigureAwait(false);
            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | ValidateCommunicationProviderAsyncValidateCommunicationProviderAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<List<CommunicationProviderAccountUdt>> GetCommunicationProviderAccountListByUserIdAsync(int userId)
    {
        var accountResult = await _userFactor.GetCommunicationProviderAccountListByUserIdAsync(userId);
        return accountResult;
    }

    public async Task<List<DataRestrictionAccessModel>> GetDataRestrictionDetailsByUserIdAsync(long userId)
    {
        var accountResult = await _userFactor.GetDataRestrictionDetailsByUserIdAsync(userId);
        return accountResult;
    }

    public async Task<bool> SetUserIdleAsync(string userId, bool isIdle)
    {
        var result = await _userFactor.SetUserIdleAsync(userId, isIdle);
        return result;
    }
}

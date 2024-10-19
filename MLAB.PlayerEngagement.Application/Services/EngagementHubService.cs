using System.Data;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using MLAB.PlayerEngagement.Core.Extensions;

namespace MLAB.PlayerEngagement.Application.Services;

public class EngagementHubService : IEngagementHubService
{
    private readonly ILogger<EngagementHubService> _logger;
    private readonly IMainDbFactory _mainDbFactory;
    private readonly IEngagementHubFactory _engagementHubFactory;

    public EngagementHubService(ILogger<EngagementHubService> logger, IMainDbFactory mainDbFactory, IEngagementHubFactory engagementHubFactory)
    {
        _logger = logger;
        _mainDbFactory = mainDbFactory;
        _engagementHubFactory = engagementHubFactory;  
    }

    #region EngagementHubService
    public async Task<bool> CancelBroadcast(long broadcastConfigurationId, long userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.EngagementHubFactory} | CancelBroadcast - [broadcastConfigurationId: {broadcastConfigurationId},userId:{userId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_CancelBroadcast,
                                 new
                                 {
                                     BroadcastConfigurationId = broadcastConfigurationId,
                                     UserId = userId
                                 }

                            );
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.EngagementHubFactory} | CancelBroadcast : [Exception] - {ex}, [Param]- broadcastConfigurationId: {broadcastConfigurationId},userId:{userId}");
        }
        return false;
    }

    public async Task<bool> DeleteAutoReplyAsync(long telegramBotAutoReplyTriggerId)
    {
        return await _engagementHubFactory.DeleteAutoReplyAsync(telegramBotAutoReplyTriggerId);
    }

    public async Task<long> TelegramCustomAutoReplyCountAsync(long botDetailId )
    {
        return await _engagementHubFactory.TelegramCustomAutoReplyCountAsync(botDetailId);
    }

    public async Task<bool> ValidateBotIdAsync(long botId)
    {
        return await _engagementHubFactory.ValidateBotIdAsync(botId);
    }

    #endregion

}

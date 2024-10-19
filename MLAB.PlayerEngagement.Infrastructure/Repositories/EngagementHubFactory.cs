using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.Request;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class EngagementHubFactory : IEngagementHubFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<EngagementHubFactory> _logger;
    public EngagementHubFactory(IMainDbFactory mainDbFactory, ILogger<EngagementHubFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }

    public async Task<bool> ValidateBotIdAsync(long botId)
    {
        try
        {
            _logger.LogInfo($"{Factories.EngagementHubFactory} | ValidateBotIdAsync - [BotId: {botId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_ValidateTelegramBotId,
                                 new
                                 {
                                     BotId = botId
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.EngagementHubFactory} | ValidateBotIdAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<bool> DeleteAutoReplyAsync(long telegramBotAutoReplyTriggerId)
    {
        try
        {
            _logger.LogInfo($"{Factories.EngagementHubFactory} | DeleteAutoReplyAsync - [telegramBotAutoReplyTriggerId: {telegramBotAutoReplyTriggerId}]");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<bool>
                (DatabaseFactories.IntegrationDb,
                    StoredProcedures.USP_DeleteTelegramBotDetailsAutoReply,
                    new
                    {
                        @BotAutoReplyTriggerId = telegramBotAutoReplyTriggerId,
                    }

                );
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.EngagementHubFactory} | DeleteAutoReplyAsync : [Exception - {ex}, Param- telegramBotAutoReplyTriggerId: {telegramBotAutoReplyTriggerId}");
        }
        return false;
    }

    public async Task<int> TelegramCustomAutoReplyCountAsync(long botDetailId)
    {
        try
        {
            _logger.LogInfo($"{Factories.EngagementHubFactory} | TelegramCustomAutoReplyCountAsync - [botDetailId: {botDetailId}]");

            var results = await _mainDbFactory.ExecuteQueryMultipleAsync<BotDetailsAutoReplyRequestModel, int>
                          (
                              DatabaseFactories.IntegrationDb,
                              StoredProcedures.USP_GetBotDetailsAutoReplyByFilter
                              , new
                              {
                                  BotDetailId = botDetailId,
                              }
                          );
            return results.Item1.Count(a => a.Type == AutoReplyType.Custom);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.EngagementHubFactory} | TelegramCustomAutoReplyCount : [Exception - {ex}, Param- botDetailId: {botDetailId}");
        }
        return 0;
    }
}

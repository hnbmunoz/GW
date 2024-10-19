using MLAB.PlayerEngagement.Core.Models.RelationshipManagement;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Response;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IEngagementHubService
{
    Task<bool> ValidateBotIdAsync(long botId);
    Task<bool> CancelBroadcast(long broadcastConfigurationId,long userId);
    Task<bool> DeleteAutoReplyAsync(long telegramBotAutoReplyTriggerId);
    Task<long> TelegramCustomAutoReplyCountAsync(long botDetailId);

}

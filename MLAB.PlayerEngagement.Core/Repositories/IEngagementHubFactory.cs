
namespace MLAB.PlayerEngagement.Core.Repositories;

public interface IEngagementHubFactory
{
    Task<bool> ValidateBotIdAsync(long botId);
    Task<bool> DeleteAutoReplyAsync(long telegramBotAutoReplyTriggerId);
    Task<int> TelegramCustomAutoReplyCountAsync(long botDetailId);
}

using MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ICampaignGoalSettingService
{
    Task<bool> CheckCampaignGoalSettingByNameExistAsync(CampaignGoalSettingNameRequestModel request);
}

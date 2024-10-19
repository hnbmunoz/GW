using MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ICampaignGoalSettingFactory
{
    Task<bool> CheckCampaignGoalSettingByNameExistAsync(CampaignGoalSettingNameRequestModel request);
}

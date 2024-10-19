using MLAB.PlayerEngagement.Core.Models.CampaignManagement;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ICampaignCustomEventSettingService
{
    Task<bool> CheckExistingCampaignCustomEventSettingByFilterAsync(CampaignCustomEventSettingRequestModel request);
}

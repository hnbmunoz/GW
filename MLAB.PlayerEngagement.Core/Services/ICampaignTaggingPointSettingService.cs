using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ICampaignTaggingPointSettingService
{
    Task<List<CampaignSettingMasterReference>> GetCampaignSettingTypeSelectionAsync(int masterReferenceId, int masterReferenceIsParent);
    Task<bool> GetCampaignSettingListAsync(AutoTaggingPointIncentiveFilterRequestModel request);
    Task<bool> GetAutoTaggingDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request);
    Task<bool> GetPointIncentiveDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request);
    Task<bool> AddAutoTaggingListAsync(AutoTaggingDetailsRequestModel request);

    Task<List<SegmentSelectionModel>> GetTaggingSegmentAsync();
    Task<List<UsersSelectionModel>> GetUsersByModuleAsync(int subMainModuleDetailId);

    Task<bool> AddPointIncentiveSettingAsync(AddPointIncentiveDetailsRequestModel request);

    Task<bool> CheckCampaignSettingByNameIfExistAsync(CampaignSettingNameRequestModel request);

}

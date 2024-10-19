using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;
using MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting.Response;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ICampaignTaggingPointSettingFactory 
{
    Task<List<CampaignSettingMasterReference>> GetCampaignSettingTypeSelectionAsync(int masterReferenceId, int masterReferenceIsParent);
    Task<Tuple<Int64, List<AutoTaggingPointIncentiveResponseModel>>> GetCampaignSettingListAsync(AutoTaggingPointIncentiveFilterRequestModel request);
    Task<AutoTaggingDetailsResponseModel> GetAutoTaggingDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request);
    Task<PointIncentiveDetailsByIdResponseModel> GetPointIncentiveDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request); //RESUE AUTOTAGGING REQUEST FILTER
    Task<List<SegmentSelectionModel>> GetTaggingSegmentAsync();
    Task<List<UsersSelectionModel>> GetUsersByModuleAsync(int subMainModuleDetailId);
    Task<bool> CheckCampaignSettingByNameIfExistAsync(CampaignSettingNameRequestModel request);


}

using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ICampaignManagementFactory
{  
    Task<List<LookupModel>> GetAllCampaignAsync();
    Task<List<LookupModel>> GetCampaignByFilterAsync(CampaignLookupByFilterRequestModel filter);
    Task<int> GetMasterReferenceId(string masterReferenceName, bool isParent);
    Task<List<MasterReferenceModel>> GetMasterReferenceList(int masterReferenceId, bool masterReferenceIsParent);
    Task<List<LookupModel>> GetAllSurveyTemplateAsync();
    Task<List<LookupModel>> GetAllSegmentAsync();
    Task<List<CustomLookModel>> GetSegmentWithSegmentTypeNameAsync();
    Task<List<SegmentListModel>> GetSegmentationByIdAsync(int segmentId);
    Task<List<CampaignGoalSettingListModel>> GetCampaignGoalSettingListAsync();    
    Task<CampaignGoalSettingListModel> GetCampaignGoalSettingByIdAsync(int campaignGoalId);
    Task<List<LookupModel>> GetAllLeaderValidation();
    Task<Tuple<List<CampaignSettingListModel>, List<CampaignSettingListModel>,List<CampaignSettingListModel>>> GetCampaignLookUpAsync();
    Task<List<CampaignAutoTaggingListModel>> GetAutoTaggingDetailsByIdAsync(int campaignSettingId);
    Task<List<CampaignUploadPlayerList>> GetExportCampaignUploadPlayerListAsync(UploadPlayerFilterModel request);
    Task<List<LookupModel>> GetAllCampaignBySearchFilterAsync(int searchFilterType, string searchFilterField, int campaignType);
    Task<List<LookupModel>> GetAllCampaignCustomEventSettingNameAsync();
    Task<bool> ValidateHasPlayerInCampaignAsync(long campaignId, string campaignGuid);
    Task<List<CampaignConfigurationSegmentModel>> GetCampaignConfigurationSegmentByIdAsync(long segmentId, long? varianceId);

}

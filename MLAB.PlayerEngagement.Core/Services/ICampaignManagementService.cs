using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ICampaignManagementService
{
    Task<List<LookupModel>> GetAllCampaignAsync();
    Task<List<LookupModel>> GetCampaignByFilterAsync(CampaignLookupByFilterRequestModel request);
    Task<List<LookupModel>> GetAllCampaignType();
    Task<List<LookupModel>> GetAllCampaignStatusAsync();
    Task<List<LookupModel>> GetAllSurveyTemplateAsync();
    Task<List<LookupModel>> GetAllSegmentAsync();
    Task<List<CustomLookModel>> GetSegmentWithSegmentTypeNameAsync();
    Task<List<SegmentListModel>> GetSegmentationByIdAsync(int segmentId);
    Task<List<CampaignGoalSettingListModel>> GetCampaignGoalSettingListAsync();
    Task<CampaignGoalSettingListModel> GetCampaignGoalSettingByIdAsync(int campaignGoalId);
    Task<List<CampaignConfigurationSegmentModel>> GetCampaignConfigurationSegmentByIdAsync(long segmentId, long? varianceId);
    Task<List<LookupModel>> GetAllLeaderValidation();
    Task<CampaignLookUpsResponseModel> GetCampaignGoalParametersAsync();
    Task<List<CampaignAutoTaggingListModel>> GetAutoTaggingDetailsByIdAsync(int campaignSettingId);
    Task<Tuple<int, string>> ValidateCampaign(CampaignModel request);
    Task<List<LookupModel>> GetEligibilityTypeAsync();
    Task<List<LookupModel>> GetSearchFilterAsync();
    Task<List<LookupModel>> GetAllCampaignBySearchFilterAsync(int searchFilterType, string searchFilterField, int campaignType);
    Task<List<CampaignUploadPlayerList>> GetExportCampaignUploadPlayerListAsync(UploadPlayerFilterModel request);
    Task<List<LookupModel>> GetAllCampaignCustomEventSettingNameAsync();
    Task<bool> ValidateHasPlayerInCampaignAsync(long campaignId, string campaignGuid);
    Task<List<CustomLookModel>> GetCampaignPeriodBySourceId(long sourceID);
}

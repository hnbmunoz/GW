using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Segmentation;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ISegmentationService
{
    Task<bool> DeactivateSegmentAsync(int segmentId, int userId);
    Task<List<SegmentationFilterFieldModel>> GetSegmentConditionFieldsAsync();
    Task<List<SegmentationFilterOperatorModel>> GetSegmentConditionOperatorsAsync();
    Task<SegmentationModel> GetSegmentByIdAsync(int segmentId);
    Task<bool> ValidateSegmentAsync(int? segmentId, string segmentName);
    Task<List<SegmentPlayer>> TestSegmentAsync(SegmentationTestModel request);
    Task<SegmentationTestResponseModel> TestStaticSegmentAsync(SegmentationTestModel request);
    Task<SegmentationToStaticResponseModel> ToStaticSegmentation(SegmentationToStaticModel request);
    Task<List<SegmentConditionSetResponseModel>> GetSegmentConditionSetByParentIdAsync(int ParentSegmentConditionFieldId);
    Task<List<LookupModel>> GetCampaignGoalNamesByCampaignIdAsync(int CampaignId);
    Task<List<LookupModel>> GetVariancesBySegmentIdAsync(int SegmentId);
    Task<SegmentLookupsResponseModel> GetSegmentLookupsAsync();
    Task<InFileSegmentPlayerResponseModel> ValidateInFilePlayersAsync(ValidateInFileRequestModel request);
    Task<bool> TriggerVarianceDistributionAsync(TriggerVarianceDistributionRequestModel request);
    Task<SegmentDistributionByFilterResponseModel> GetVarianceDistributionForCSVAsync(SegmentDistributionByFilterRequestModel request);
    Task<List<SegmentationConditionModel>> GetSegmentConditionsBySegmentIdAsync(int SegmentId);
    Task<List<LookupModel>> GetMessageStatusByCaseTypeIdAsync(string CaseTypeId);
    Task<List<LookupModel>> GetMessageResponseByMultipleIdAsync(string MessageStatusId);

    Task<ValidateCustomQueryResponseModel> ValidateCustomQueryAsync(string CustomQuery);
}

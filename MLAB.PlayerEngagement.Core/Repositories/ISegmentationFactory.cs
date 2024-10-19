using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Segmentation;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ISegmentationFactory
{
    Task<bool> DeactivateSegmentAsync(int SegmentId, int userId);
    Task<List<SegmentationFilterFieldModel>> GetSegmentConditionFieldsAsync();
    Task<List<SegmentationFilterOperatorModel>> GetSegmentConditionOperatorsAsync();
    Task<Tuple<SegmentationModel, List<SegmentationConditionModel>, List<SegmentVarianceModel>>> GetSegmentByIdAsync(int segmentId);
    Task<bool> ValidateSegmentAsync(int? segmentId, string segmentName);
    Task<List<SegmentPlayer>> TestSegmentAsync(SegmentationTestModel request);
    Task<SegmentationTestResponseModel> TestStaticSegmentAsync(SegmentationTestModel request);
    Task<SegmentationToStaticResponseModel> ToStaticSegmentation(SegmentationToStaticModel request);
    Task<List<SegmentConditionSetResponseModel>> GetSegmentConditionSetByParentIdAsync(int ParentSegmentConditionFieldId);
    Task<List<LookupModel>> GetCampaignGoalNamesByCampaignIdAsync(int CampaignId);
    Task<List<LookupModel>> GetVariancesBySegmentIdAsync(int SegmentId);
    Task<Tuple<List<SegmentationFilterFieldModel>, List<SegmentationFilterOperatorModel>, List<SegmentFieldLookupModel>>> GetSegmentLookupsAsync();
    Task<Tuple<InFilePlayersValidationCount, List<InFilePlayersInvalidRemarks>>> ValidateInFilePlayersAsync(ValidateInFileRequestModel request);
    Task<bool> TriggerVarianceDistributionAsync(TriggerVarianceDistributionRequestModel request);
    Task<SegmentDistributionByFilterResponseModel> GetVarianceDistributionForCSVAsync(SegmentDistributionByFilterRequestModel request);
    Task<List<SegmentationConditionModel>> GetSegmentConditionsBySegmentIdAsync(int SegmentId);
    Task<List<LookupModel>> GetMessageStatusByCaseTypeIdAsync(string CaseTypeId);
    Task<List<LookupModel>> GetMessageResponseByMultipleIdAsync(string MessageStatusId);
    Task<string> GetSegmentCustomQueryProhibitedKeywordsAsync();
}

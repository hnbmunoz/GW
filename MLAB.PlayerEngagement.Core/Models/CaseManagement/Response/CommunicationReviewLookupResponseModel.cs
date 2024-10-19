
namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class CommunicationReviewLookupResponseModel
    {
       public List<QualityReviewMeasurementResponseModel> QualityReviewMeasurements { get; set; }
       public List<QualityReviewBenchmarkResponseModel> QualityReviewBenchmarks { get; set; }
       public List<QualityReviewPeriodResponseModel> QualityReviewPeriods { get; set; }
       public List<CommunicationReviewFieldLookupsResponseModel> CommunicationReviewFieldLookups { get; set; }    
    }
}

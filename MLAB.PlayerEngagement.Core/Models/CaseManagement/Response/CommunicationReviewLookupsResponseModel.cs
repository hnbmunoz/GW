namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class CommunicationReviewLookupsResponseModel
    {
        public List<QualityReviewMeasurementResponseModel> QualityReviewMeasurementList { get; set; }
        public List<QualityReviewBenchmarkResponseModel> QualityReviewBenchmarkList { get; set; }
        public List<QualityReviewPeriodResponseModel> QualityReviewPeriodList { get; set; }
        public List<LookupModel> QualityReviewPeriodOptions { get; set; }
        public List<LookupModel> QualityReviewRankingOptions { get; set; }
        public List<LookupModel> CommunicationReviewStatus { get; set; }
        public List<LookupModel> CommunicationReviewEvent { get; set; }
        public List<LookupModel> QualityReviewMeasurementType { get; set; }
        public int QualityReviewLimit { get; set; }
    }
}

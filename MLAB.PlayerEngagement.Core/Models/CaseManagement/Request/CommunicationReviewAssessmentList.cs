namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class CommunicationReviewAssessmentList
    {
        public int CommunicationReviewAssessmentId { get; set; }
        public int QualityReviewMeasurementId { get; set; }
        public int QualityReviewCriteriaId { get; set; }
        public decimal AssessmentScore { get; set; }
        public string Remarks { get; set; }
        public string Suggestions { get; set; }

    }
}

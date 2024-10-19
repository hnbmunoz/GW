namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class QualityReviewPeriodResponseModel
    {
        public int QualityReviewPeriodId { get; set; }
        public string QualityReviewPeriodName { get; set; }
        public DateTime QualityReviewPeriodStart { get; set; }
        public DateTime QualityReviewPeriodEnd { get; set; }  
        public DateTime QualityReviewPeriodDeadline { get; set; }
    }
}

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class CommunicationReviewLimitRequestModel
    {
        public int CaseCommunicationId { get; set; }
        public int QualityReviewPeriodId { get; set; }
        public int RevieweeId { get; set; }
        public int ReviewerId { get; set; }
        public int UserId { get; set; }
    }
}

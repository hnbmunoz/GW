namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class SaveCommunicationReviewRequestModel : BaseModel
    {
        public int CommunicationReviewId { get; set; }
        public int CaseCommunicationId { get; set; }
        public int QualityReviewPeriodId { get; set; }
        public int CommunicationReviewStatusId { get; set; }
        public int CommunicationRevieweeId { get; set; }
        public int CommunicationReviewerId { get; set; }
        public string CommunicationReviewSummary { get; set; } 
        public decimal CommunicationReviewScore { get; set; }
        public List<CommunicationReviewAssessmentList> CommunicationReviewAssessments { get; set; }
    }
}

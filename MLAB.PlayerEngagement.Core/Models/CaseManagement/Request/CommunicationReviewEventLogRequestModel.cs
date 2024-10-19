namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class CommunicationReviewEventLogRequestModel
    {
        public int CaseCommunicationId { get; set; }
        public int EventTypeId { get; set; }
        public int UserId { get; set; }
    }
}

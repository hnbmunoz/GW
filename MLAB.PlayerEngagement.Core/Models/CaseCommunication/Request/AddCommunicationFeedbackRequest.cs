namespace MLAB.PlayerEngagement.Core.Models;

public class AddCommunicationFeedbackRequest
{
    public int CommunicationFeedbackId { get; set; }
    public int CaseCommunicationId { get; set; }
    public int CommunicationFeedbackNo { get; set; }
    public int FeedbackTypeId { get; set; }
    public int FeedbackCategoryId { get; set; }
    public int FeedbackAnswerId { get; set; }
    public string FeedbackAnswer { get; set; }
    public string CommunicationFeedbackDetails { get; set; }
    public string CommunicationSolutionProvided { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models;

public  class FeedbackAnswerRequestModel
{
    public int FeedbackAnswerId { get; set; }
    public string FeedbackAnswerName { get; set; }
    public int FeedbackAnswerStatus { get; set; }
    public int FeedbackCategoryId { get; set; }
    public int Position { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

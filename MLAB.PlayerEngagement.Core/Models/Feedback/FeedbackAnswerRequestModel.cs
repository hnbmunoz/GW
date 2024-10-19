namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class FeedbackAnswerRequestModel : BaseModel
{
    public int FeedbackAnswerId { get; set; }
    public string FeedbackAnswerName { get; set; }
    public bool FeedbackAnswerStatus { get; set; }
    public int FeedbackCategoryId { get; set; }
    public int Position { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public List<FeedbackAnswerCategoryModel> FeedbackAnswerCategories { get; set; }
}

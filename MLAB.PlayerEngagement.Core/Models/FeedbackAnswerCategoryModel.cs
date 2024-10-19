namespace MLAB.PlayerEngagement.Core.Models;

public class FeedbackAnswerCategoryModel
{
    public int FeedbackAnswerCategoriesId { get; set; }
    public int FeedbackAnswerId { get; set; }
    public string FeedbackAnswerName { get; set; }
    public int FeedbackCategoryId { get; set; }
    public string FeedbackCategoryName { get; set; }
}

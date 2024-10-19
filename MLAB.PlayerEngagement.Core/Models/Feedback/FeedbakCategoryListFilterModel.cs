namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class FeedbakCategoryListFilterModel: BaseModel
{
    public string FeedbackCategoryName { get; set; }
    public string FeedbackCategoryStatus { get; set; }
    public string FeedbackTypeIds { get; set; }
}

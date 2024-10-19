namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class FeedbackAnswerListFilterModel : BaseModel
{
    public string FeedbackAnswerName { get; set; }
    public string FeedbackAnswerStatuses { get; set; }
    public string FeedbackCategoryIds { get; set; }
}

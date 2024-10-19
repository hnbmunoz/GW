namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class AddFeedbackAnswerModel : BaseModel
{
    public int CodeListId { get; set; }
    public string CodeListStatus { get; set; }
    public List<FeedbackAnswerRequestModel> FeedbackAnswers { get; set; }
}

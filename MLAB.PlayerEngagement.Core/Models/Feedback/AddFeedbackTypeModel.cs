namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class AddFeedbackTypeModel : BaseModel
{
    public int CodeListId { get; set; }
    public string CodeListStatus { get; set; }
    public List<FeedbackTypeRequestModel> FeedbackType { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class AddFeedbackCategoryModel : BaseModel
{
    public int CodeListId { get; set; }
    public string CodeListStatus { get; set; }
    public List<FeedbackCategoryRequestModel> FeedbackCategories { get; set; }
}

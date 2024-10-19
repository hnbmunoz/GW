namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class FeedbackCategoryRequestModel
{
    public int FeedbackCategoryId { get; set; }
    public string FeedbackCategoryName { get; set; }
    public bool FeedbackCategoryStatus { get; set; }
    public int FeedbackTypeId { get; set; }
    public int Position { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public List<FeedbackCategoryTypeModel> FeedbackCategoryTypes { get; set; }
}

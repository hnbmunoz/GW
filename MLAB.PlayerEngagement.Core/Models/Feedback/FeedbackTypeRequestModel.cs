namespace MLAB.PlayerEngagement.Core.Models.Feedback;

public class FeedbackTypeRequestModel
{
    public string FeedbackTypeName { get; set; }
    public string FeedbackTypeStatus { get; set; }
    public int FeedbackTypeId { get; set; }
    public int Position { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

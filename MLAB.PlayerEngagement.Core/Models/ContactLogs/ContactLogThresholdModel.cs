namespace MLAB.PlayerEngagement.Core.Models;

public class ContactLogThresholdModel
{
    public long UserId { get; set; }
    public string FullName { get; set; }
    public int ThresholdCount { get; set; }
    public string EmailRecipient { get; set; }
    public string EmailContent { get; set; }
    public string ThresholdAction { get; set; }
    public int CurrentCount { get; set; }
}

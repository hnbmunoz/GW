namespace MLAB.PlayerEngagement.Core.Models;

public class ManageThresholdRequest
{
    public int ManageThresholdId { get; set; }
    public int ThresholdCount { get; set; }
    public int ThresholdAction { get; set; }
    public string EmailRecipient { get; set; }
}

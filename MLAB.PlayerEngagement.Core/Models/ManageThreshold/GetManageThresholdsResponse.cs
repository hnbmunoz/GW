namespace MLAB.PlayerEngagement.Core.Models;

public class GetManageThresholdsResponse
{
    public int ManageThresholdId { get; set; }
    public int ThresholdCount { get; set; }
    public int ThresholdAction { get; set; }
    public string EmailRecipient { get; set; }
    public int CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public string UpdatedDate { get; set; }
}

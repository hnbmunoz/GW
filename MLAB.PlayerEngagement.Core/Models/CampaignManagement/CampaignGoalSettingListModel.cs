namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignGoalSettingListModel
{
    public int CampaignSettingId { get; set; }
    public string CampaignSettingName { get; set; }
    public string CampaignSettingDescription { get; set; }
    public string SegmentDescription { get; set; }
    public int GoalParameterAmountId { get; set; }
    public string GoalParameterAmountName { get; set; }
    public int GoalParameterCountId { get; set; }
    public string GoalParameterCountName { get; set; }
}

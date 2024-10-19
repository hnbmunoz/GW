namespace MLAB.PlayerEngagement.Core.Models.CampaignPerformance;

public class CampaignGoalResponseModel
{
    public int CampaignId { get; set; }
    public string CampaignGoalName { get; set; }
    public int CampaignGoalId { get; set; }
    public bool IsPrimary { get; set; }
    public string CampaignGoalDesc { get; set; }
    public int CampaignTypeId { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.CampaignPerformance;

public class CampaignPerformanceFilterResponseModel
{
    public List<CampaignActiveAndEndedResponseModel> Campaigns { get; set; }
    public List<CampaignGoalResponseModel> CampaignGoals { get; set; }
}

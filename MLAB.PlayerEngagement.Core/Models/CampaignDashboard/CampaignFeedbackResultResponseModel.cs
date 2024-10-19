namespace MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

public class CampaignFeedbackResultResponseModel
{
    public string FeedbackCategory { get; set; }
    public string FeedbackAnswer { get; set; }
    public int Count { get; set; }
    public int Deposited { get; set; }
    public decimal DepositedPercentage { get; set; }

}

namespace MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

public class SurveyAndFeedbackReportSummary
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int CampaignReportPeriod { get; set; }
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public string CampaignStatus { get; set; }
    public string Brand { get; set; }
    public string Currency { get; set; }
    public int CampaignTypeId { get; set; }
    public string CampaignType { get; set; }
}

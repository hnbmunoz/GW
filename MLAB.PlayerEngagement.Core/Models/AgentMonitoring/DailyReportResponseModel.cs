namespace MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

public class DailyReportResponseModel
{
    public int DailyReportId { get; set; }
    public int CampaignAgentId { get; set; }
    public int CampaignId { get; set; }
    public string ReportDate { get; set; }
    public string Shift { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}

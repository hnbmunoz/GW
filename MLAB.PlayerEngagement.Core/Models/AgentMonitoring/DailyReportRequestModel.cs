namespace MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

public class DailyReportRequestModel : BaseModel
{
    public string ReportDate { get; set; }
    public string Shift { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }
    public int CampaignAgentId { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}

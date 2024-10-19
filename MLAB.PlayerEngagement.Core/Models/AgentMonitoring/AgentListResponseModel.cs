namespace MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

public class AgentListResponseModel
{
    public List<AgentResponseModel> Agents { get; set; }
    public List<DailyReportResponseModel> DailyReports { get; set; }
    public int RecordCount { get; set; }
}

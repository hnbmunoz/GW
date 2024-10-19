namespace MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

public class AgentListRequestModel
{
    public int? CampaignId { get; set; }
    public int? AutoTaggedId { get; set; }
    public string AgentName { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string AgentId { get; set; }
}

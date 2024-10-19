namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace;

public class ServiceCommunicationHistoryFilterRequestModel
{
    public long CampaignId { get; set; }
    public string PlayerId { get; set; }
    public string BrandName { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

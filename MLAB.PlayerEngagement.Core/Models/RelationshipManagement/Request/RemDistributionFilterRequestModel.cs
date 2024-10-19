namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class RemDistributionFilterRequestModel : BaseModel
{
    public long RemProfileId { get; set; }
    public string AgentIds { get; set; }
    public string PseudoNames { get; set; }
    public string PlayerId { get; set; }
    public string UserName { get; set; }
    public long StatusId { get; set; }
    public string CurrencyIds { get; set; }
    public long BrandId { get; set; }
    public string VipLevelIds { get; set; }
    public bool? AssignStatus { get; set; }
    public string DistributionDateStart { get; set; }
    public string DistributionDateEnd { get; set; }
    public string AssignedByIds { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

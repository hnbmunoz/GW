namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class RemHistoryFilterRequestModel : BaseModel
{
    public string AssignmentDateStart { get; set; }
    public string AssignmentDateEnd { get; set; }
    public string ActionTypeIds { get; set; }
    public string RemProfileIds { get; set; }
    public string AgentIds { get; set; }
    public string PseudoName { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public long MlabPlayerId { get; set; }
}

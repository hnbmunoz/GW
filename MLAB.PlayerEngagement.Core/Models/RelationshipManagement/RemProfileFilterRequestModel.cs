namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement;

public class RemProfileFilterRequestModel: BaseModel
{
    public int? RemProfileID { get; set; }
    public string RemProfileName { get; set; }
    public string AgentNameIds { get; set; }
    public string PseudoNamePP { get; set; }
    public int? OnlineStatusId { get; set; }
    public int? AgentConfigStatusId { get; set; }
    public int? ScheduleTemplateSettingId { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string PlayerId { get; set; }
    public long MlabPlayerId { get; set; }
}

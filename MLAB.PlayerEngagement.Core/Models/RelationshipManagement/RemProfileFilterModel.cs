namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement;

public class RemProfileFilterModel
{
    public int? RemProfileID { get; set; }
    public string RemProfileName { get; set; }
    public int? AgentId { get; set; }
    public string AgentName { get; set; }
    public string PseudoNamePP { get; set; }
    public int? OnlineStatusId { get; set; }
    public string OnlineStatus { get; set; }
    public int? AgentConfigStatusId { get; set; }
    public string AgentConfigStatus { get; set; }
    public int? ScheduleTemplateSettingId { get; set; }
    public string ScheduleTemplateSettingName { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

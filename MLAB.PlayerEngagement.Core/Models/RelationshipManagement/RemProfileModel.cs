namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement;

public class RemProfileModel
{

    public int RemProfileId { get; set; }
    public string RemProfileName { get; set; }
    public int AgentId { get; set; }
    public string PseudoNamePP { get; set; }
    public int ScheduleTemplateSettingId { get; set; }
    public int OnlineStatusId { get; set; }
    public int AgentConfigStatusId { get; set; }
    public long CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public long UpdatedBy { get; set; }
    public string UpdatedDate { get; set; }
}

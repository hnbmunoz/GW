namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Udt;

public class RemLivePersonUDTModel
{
    public int LivePersonId { get; set; }
    public int RemProfileId { get; set; }
    public string EngagementID { get; set; }
    public string AgentID { get; set; }
    public string SkillID { get; set; }
    public string SkillName { get; set; }
    public string Section { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

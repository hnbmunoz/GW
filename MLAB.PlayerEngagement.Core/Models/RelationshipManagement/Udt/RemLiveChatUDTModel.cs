namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Udt;

public class RemLiveChatUDTModel
{


    public int LiveChatId { get; set; }
    public int RemProfileId { get; set; }
    public string AgentId { get; set; }
    public string GroupId { get; set; }
    public string GroupName { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

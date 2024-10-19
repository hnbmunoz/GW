namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement;

public  class UpdateRemProfileRequestModel
{
    public int? RemProfileID { get; set; }
    public int? AgentNameId { get; set; }
    public int? OnlineStatusId { get; set; }
    public int? AgentConfigStatusId { get; set; }
    public int? UserId { get; set; }
    public bool OnlineStatus { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request
{
    public class UpdateMaxPlayerCountConfigRequestModel
    {
        public long AgentNameId { get; set; }
        public long MaxPlayerCount { get; set; }
        public int UserId { get; set; }
    }
}

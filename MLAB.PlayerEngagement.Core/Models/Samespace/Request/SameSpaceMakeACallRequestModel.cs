namespace MLAB.PlayerEngagement.Core.Models.Samespace.Request
{
    public class SamespaceMakeACallRequestModel
    {
        public string AgentId { get; set; }
        public long MlabPlayerId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
    }
}

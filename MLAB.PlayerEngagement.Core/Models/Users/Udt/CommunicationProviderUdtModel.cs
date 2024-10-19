namespace MLAB.PlayerEngagement.Core.Models.Users.Udt
{
    public class CommunicationProviderUdtModel
    {
        public int ChatUserAccountId { get; set; }
        public string MessageTypeId { get; set; }
        public string AccountID { get; set; }
        public string ChatUserAccountStatus { get; set; }
        public int? SubscriptionId { get; set; }

    }
}

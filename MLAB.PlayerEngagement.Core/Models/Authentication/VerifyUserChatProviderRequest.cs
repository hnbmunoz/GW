namespace MLAB.PlayerEngagement.Core.Models.Authentication
{
    public class VerifyUserChatProviderRequest 
    {
        public long UserId { get; set; }
        public string ProviderAccount { get; set; }
        public long ProviderId { get; set; }
    }
}

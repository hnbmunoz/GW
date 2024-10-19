namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class PlayerInfoCaseCommunicationResponse
    {
        public string PlayerId { get; set; }
        public string Username { get; set; }
        public string CurrencyName { get; set; }    
        public long CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string PaymentGroupName { get; set; }
        public long PaymentGroupId { get; set; }
        public long VIPLevelId { get; set; }
        public string VipLevelName { get; set; }
        public int MlabPlayerId { get; set; }
    }
}

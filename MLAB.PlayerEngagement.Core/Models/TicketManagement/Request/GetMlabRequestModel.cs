

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class GetMlabRequestModel
    {
        public string TransactionId { get; set; }
        public long MlabPlayerId { get; set; }
        public string ProviderTransactionId { get; set; }
    }
}


namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class FmboTransactionDataRequestModel
    {
        public int Source { get; set; }
        public string TransactionId { get; set; } 
        public string CheckSum { get; set; }
        public int UserId { get; set; }
    }
}

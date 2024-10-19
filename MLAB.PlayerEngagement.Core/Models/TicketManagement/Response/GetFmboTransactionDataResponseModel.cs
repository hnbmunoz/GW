
namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class GetFmboTransactionDataResponseModel
    {
        public string PaymentMethodName { get; set; }
        public string PaymentMethodExt { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CryptoCurrency { get; set; }
        public decimal CryptoAmount { get; set; }
        public string PgTransactionId { get; set; }
        public int PaymentSystemTransactionStatusId { get; set; } 
        public string TransactionHash { get; set; }
        public string MethodCurrency { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remarks { get; set; } 
        public decimal RecievedAmount { get; set; }
        public string WalletAddress { get; set; }
        public string ProviderTransactionId { get; set; }
        public string TransactionId { get; set; }
        public string PaymentProcessor { get; set; }
    }
}

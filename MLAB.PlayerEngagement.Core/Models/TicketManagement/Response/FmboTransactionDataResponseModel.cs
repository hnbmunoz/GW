
using System.Text.Json.Serialization;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class FmboTransactionDataResponseModel
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonPropertyName("transactionInfo")]
        public TransactionInfo TransactionInfo { get; set; }
    }
    public class TransactionInfo
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
        [JsonPropertyName("reasonCode")]
        public string ReasonCode { get; set; }
        [JsonPropertyName("requestedAmount")]
        public decimal RequestedAmount { get; set; }
        [JsonPropertyName("creditedAmount")]
        public decimal CreditedAmount { get; set; }
        [JsonPropertyName("reimbursementFee")]
        public decimal ReimbursementFee { get; set; }
        [JsonPropertyName("bankFee")]
        public decimal BankFee { get; set; }
        [JsonPropertyName("cryptoRequestedAmount")]
        public string CryptoRequestedAmount { get; set; }
        [JsonPropertyName("cryptoCurrency")]
        public string CryptoCurrency { get; set; }
        [JsonPropertyName("cryptoCreditedAmount")]
        public string CryptoCreditedAmount { get; set; }
        [JsonPropertyName("cryptoFee")]
        public string CryptoFee { get; set; }
        [JsonPropertyName("cryptoWalletAddress")]
        public string CryptoWalletAddress { get; set; }
        [JsonPropertyName("cryptoTransactionHash")]
        public string CryptoTransactionHash { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }
        [JsonPropertyName("operatorTransactionId")]
        public string OperatorTransactionId { get; set; }
        [JsonPropertyName("providerTransactionId")]
        public string ProviderTransactionId { get; set; }
        [JsonPropertyName("pgTransactionId")]
        public string PgTransactionId { get; set; }
        [JsonPropertyName("transactionTimestamp")]
        public string TransactionTimestamp { get; set; }
        [JsonPropertyName("referencenumber")]
        public string ReferenceNumber { get; set; }
        [JsonPropertyName("txnRemark")]
        public string Remarks { get; set; }
        [JsonPropertyName("walletAddress")]
        public string WalletAddress { get; set; }
        [JsonPropertyName("paymentProcessor")]
        public string PaymentProcessor { get; set; }
    }

}

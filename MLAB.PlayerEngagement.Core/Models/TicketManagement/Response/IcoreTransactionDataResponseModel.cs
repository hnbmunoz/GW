using System.Text.Json.Serialization;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class IcoreTransactionDataResponseModel
    {
        [JsonPropertyName("transactionid")]
        public string TransactionId { get; set; }
        [JsonPropertyName("playerid")]
        public int PlayerId { get; set; }
        [JsonPropertyName("paymentmethodname")]
        public string PaymentMethodName { get; set; }
        [JsonPropertyName("paymentmethodext")]
        public string PaymentMethodExt { get; set; }
        [JsonPropertyName("transactiondate")]
        public DateTime TransactionDate { get; set; }
        [JsonPropertyName("balancebefore")]
        public decimal BalanceBefore { get; set; }
        [JsonPropertyName("transactiontypeid")]
        public int TransactionTypeId { get; set; }
        [JsonPropertyName("transactionstatusid")]
        public int TransactionStatusId { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("providertransactionid")]
        public string ProviderTransactionId { get; set; }
        [JsonPropertyName("accountnumber")]
        public string AccountNumber { get; set; }
        [JsonPropertyName("providerid")]
        public int ProviderId { get; set; }
        [JsonPropertyName("paymentinstrumentid")]
        public int PaymentInstrumentId { get; set; }
        [JsonPropertyName("customparameters")]
        public CustomParameters CustomParameters { get; set; }

    }

    public class CustomParameters
    {
        [JsonPropertyName("accountholder")]
        public string AccountHolder { get; set; }
        [JsonPropertyName("bankname")]
        public string BankName { get; set; }
    }
}

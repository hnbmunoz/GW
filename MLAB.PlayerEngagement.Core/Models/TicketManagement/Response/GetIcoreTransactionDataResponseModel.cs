namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class GetIcoreTransactionDataResponseModel
    {
        public string TransactionId { get; set; }
        public int PlayerId { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentMethodExt { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal BalanceBefore { get; set; }
        public int TransactionTypeId { get; set; }
        public int TransactionStatusId { get; set; }
        public decimal Amount { get; set; }
        public string ProviderTransactionId { get; set; }
        public int ProviderId { get; set; }
        public int PaymentInstrumentId { get; set; }
        public bool IsSuccessInsert { get; set; }
        public string MessageValidation { get; set; }
        public string AccountHolder { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class UpsertTransactionDataFromApiRequestModel
    {
        public string TransactionId { get; set; }
        public int PlayerId { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentMethodExt { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal BalanceBefore { get; set; }
        public long TransactionTypeId { get; set; }
        public long? TransactionStatusId { get; set; }
        public decimal Amount { get; set; }
        public string ProviderTransactionId { get; set; }
        public long ProviderId { get; set; }
        public long PaymentInstrumentId { get; set; }
        public string CustomParameters { get; set; }
        public long UserId { get; set; }
        public string PgTransactionId { get; set; }
        public long PaymentSystemTransactionStatusId { get; set; }
        public string TransactionHash { get; set; }
        public string MethodCurrency { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remarks { get; set; }
        public decimal ReceivedAmount { get; set; }
        public string WalletAddress { get; set; }
        public long TicketId { get; set; }
        public long TicketTypeId { get; set; }
    }
}

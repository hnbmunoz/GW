using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class MlabTransactionResponseModel
    {
        public string TransactionId { get; set; }
        public string ProviderTransactionId { get; set; }
        public int PaymentMethodExtId { get; set; }
        public string PaymentMethodName { get; set; }       
        public decimal Amount { get; set; }
        public string PgTransactionId { get; set; }
        public long TransactionStatusId { get; set; }
        public long PaymentSystemTransactionStatusId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionHash { get; set; }
        public string Remarks { get; set; }
        public string WalletAddress { get; set; }
        public decimal CryptoAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public string MethodCurrency { get; set; }
        public string ReferenceNumber { get; set; }

    }
}

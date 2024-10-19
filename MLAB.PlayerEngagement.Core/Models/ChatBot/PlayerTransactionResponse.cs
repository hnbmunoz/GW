using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class PlayerTransactionResponse : BaseResponse
    {
        public int PlayerID { get; set; }
        public string BrandName { get; set; }
        public string PaymentGroup { get; set; }
        public string Currency { get; set; }
        public string VIPLevel { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        public string ID { get; set; }
        public string ProviderTrxnID { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public string PaymentMethodName { get; set; }
        public string DeclineCode { get; set; }
        public DateTime TransactionSubmissionTime { get; set; }
        public int CaseModifiedUserId { get; set; }

    }
}

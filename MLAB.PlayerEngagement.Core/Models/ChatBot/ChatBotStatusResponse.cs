using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class ChatbotStatusResponse 
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string CaseId { get; set; }
        public string CommunicationId { get; set; }
        public string ExternalCommunicationId { get; set; }
        public DateTime? CaseDateCreated { get; set; }
        public DateTime? CaseDateModified { get; set; }
        public string CaseStatus { get; set; }
        public int? CaseCreatorUserId { get; set; }
        public string CaseCreatorUserName { get; set; }
        public int? CaseModifiedUserId { get; set; }
        public string CaseModifiedUserName { get; set; }

    }
}

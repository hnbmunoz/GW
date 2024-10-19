using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class CaseAndPlayerInformationResponse : BaseResponse
    {
        
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public string VIPLevel { get; set; }
        public string Currency { get; set; }
        public string BrandName { get; set; }
        public int CaseID { get; set; }
        public string CaseStatus { get; set; }
        public int CommunicationID { get; set; }
        public DateTime CaseDateCreated { get; set; }
        public DateTime CaseDateModified { get; set; }
        public int CaseCreatorUserId { get; set; }
        public string CaseCreatorUserName { get; set; }
        public int CaseModifiedUserId { get; set; }
        public string CaseModifiedUserName { get; set; }
        public int TopicId { get; set; }
        public int SubTopicId { get; set; }
        public int CurrencyId { get; set; }
        public int BrandID { get; set; }

    }
}

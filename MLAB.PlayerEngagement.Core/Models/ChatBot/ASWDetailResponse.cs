using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class ASWDetailResponse: BaseResponse
    {
        public int CaseID { get; set; }
        public int CommunicationID { get; set; }
        public string ExternalCommunicationID { get; set; }
        public DateTime CaseDateCreated { get; set; }
        public DateTime CaseDateModified { get; set; }
        public string CaseStatus { get; set; }
        public int CaseCreatorUserID { get; set; }
        public string CaseCreatorUserName { get; set; }
        public int CaseModifiedUserID { get; set; }
        public string CaseModifiedUserName { get; set; }

    }
}

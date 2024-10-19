using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CustomerCaseCommunicationModel
    {
        public long CaseCommunicationId { get; set; }
        public string Purpose { get; set; }
        public string ExternalCommunicationId { get; set; }
        public string MessageType { get; set; }
        public string MessageStatus { get; set; }
        public string CommunicationOwner { get; set; }
        public string CreatedDate { get; set; }
        public string ReportedDate { get; set; }
    }
}

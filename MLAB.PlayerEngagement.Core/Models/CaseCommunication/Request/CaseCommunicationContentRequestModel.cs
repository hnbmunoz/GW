using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request
{
    public class CaseCommunicationContentRequestModel
    {
#nullable disable
        public string CommunicationContent { get; set; }
        public long CommunicationId { get; set; }
        public DateTime StartCommunicationDate { get; set; }
        public DateTime EndCommunicationDate { get; set; }
    }
}

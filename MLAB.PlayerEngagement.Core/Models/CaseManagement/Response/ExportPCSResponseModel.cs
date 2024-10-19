using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class ExportPcsResponseModel
    {
        public string Id { get; set; }
        public long CaseCommunicationId { get; set; }
        public string Username { get; set; }
        public string Agent { get; set; }
        public string TopicName { get; set; }
        public string SubtopicName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Summary { get; set; }
        public string Action { get; set; }
        public string ExternalId { get; set; }
        public string CommunicationOwner { get; set; }
        public DateTime CommunicationStartDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public  class CaseManagementPcsCommunicationResponseModel
    {
        public int CaseCommunicationId { get; set; }
        public string ExternalId { get; set; }
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Agent { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int SubtopicId { get; set; }
        public string SubtopicName { get; set; }
        public int ChatSurveyId { get; set; }
        public string CreatedDate { get; set; }
        public string Summary { get; set; }
        public string Action { get; set; }
        public string CommunicationOwner { get; set; }
        public string CommunicationStartDate { get; set; }
        public int CaseId { get; set; }
    }
}

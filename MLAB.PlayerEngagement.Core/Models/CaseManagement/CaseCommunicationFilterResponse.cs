using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CaseCommunicationFilterResponse
    {
        public string CaseType { get; set; }
        public string Brand { get; set; }
        public string CaseCommunicationId { get; set; }
        public string CaseInformatIonId { get; set; }
        public string ExternalCommunicationId { get; set; }
        public string UserName { get; set; }
        public string VIPLevel { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        public string MessageType { get; set; }
        public string CaseStatus { get; set; }
        public string CommunicationOwner { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TopicId { get; set; }
        public string SubTopicId { get; set; }
        public int Duration { get; set; }
        public string CampaignName { get; set; }
        public string CommunicationOwnerTeamName { get; set; }
        public string Currencies { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public DateTime CommunicationStartDate { get; set; }
        public DateTime CommunicationEndDate { get; set; }
        public DateTime ReportedDate { get; set; }
        public string IsLastSkillAbandonedQueue { get; set; }
        public string IsLastAgentAbandonedAssigned { get; set; }
        public string LatestSkillTeamName { get; set; }

    }
}

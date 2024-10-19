using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget
{
    public class AgentSurveyCaseCommResponseModel
    {
        public long AgentSurveyId { get; set; }
        public string CaseBrandName { get; set; } = string.Empty;
        public string CaseStatusName { get; set; } = string.Empty;
        public string CaseTopic { get; set; } = string.Empty;
        public long? CaseStatusId { get; set; }
        public string CaseSubtopic { get; set; } = string.Empty;
        public string CommunicationCreatedBy { get; set; } = string.Empty;
        public long CommunicationId { get; set; }
        public string ConversationId { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public long LanguageId { get; set; } = new();
        public long SubTopicId { get; set; } = new();
        public string SubTopicName { get; set; } = string.Empty;
        public long? SubtopicLanguageId { get; set; }
        public string SubmittedByName { get; set; }
        public string SubmittedDate { get; set; }
        public long TopicId { get; set; } = new();
        public string TopicName { get; set; } = string.Empty;
        public long? TopicLanguageId { get; set; }
        public string UserName { get; set; } = string.Empty; // For Agent info
        public string Username { get; set; } = string.Empty; // For Case Comm info

    }
}

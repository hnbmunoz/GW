using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class ChatStatisticsChatInformationModel
    {
        public string ConversationId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ConversationEndTime { get; set; }
        public string LatestSkillId { get; set; }
        public string LatestSkillName { get; set; }
        public string LatestSkillTeamId { get; set; }
        public string LatestSkillTeamName { get; set; }
        public string LatestAgentId { get; set; }
        public string LatestAgentName { get; set; }
        public string LatestQueueState { get; set; }
        public string Status { get; set; }
        public string ConversationAvgResponseTime { get; set; }
        public string ConversationDuration { get; set; }

    }
}

using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class ChatStatisticsAgentSegmentModel
    {
        public string ID { get; set; }
        public string Index { get; set; }
        public string ConversationId { get; set; }
        public string AgentID { get; set; }
        public string AgentLoginName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string IsFirstSegment { get; set; }
        public string IsLastSegment { get; set; }
        public string WaitTime { get; set; }
        public string ContactDuration { get; set; }
        public string ResponseCount { get; set; }
        public string TotalResponseTime { get; set; }
        public string ResponseCycleCount { get; set; }
        public string AvgResponseTime { get; set; }

    }
}

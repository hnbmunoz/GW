using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class ChatStatisticsAbandonmentModel
    {
        public string IsLastAgentAbandonedAssigned { get; set; }
        public string IsLastSkillAbandonedQueue { get; set; }
        public string LastAgentAvgDuration { get; set; }
        public string LastSkillAvgDuration { get; set; }
        public string LastAgentTotDuration { get; set; }
        public string LastAgentAveResponseTime { get; set; }
        public string LastAgentAvgWaitingTime { get; set; }
        public string LastAgentTotWaitingTime {  get; set; }
        public string LastAgentAvgContactDuration { get; set; }
        public string LastAgentTotContactDuration { get; set; }
        public string LastSkillTotDuration { get; set; }


    }
}

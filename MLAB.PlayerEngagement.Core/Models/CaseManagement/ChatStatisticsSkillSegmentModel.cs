using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class ChatStatisticsSkillSegmentModel
    {
        public string Index { get; set; }
        public string ConversationId { get; set; }
        public string SkillId { get; set; }
        public string SkillName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string IsFirstSegment { get; set; }
        public string IsLastSegment { get; set; }


    }
}

using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CustomerCaseChatStatisticsModel
    {
        public ChatStatisticsCaseCommDetailsModel chatStatisticsCaseCommDetailsModel { get; set; }
        public ChatStatisticsChatInformationModel chatInformationModel { get; set; }
        public ChatStatisticsAbandonmentModel chatAbandonmentModel { get; set; }
        public List<ChatStatisticsAgentSegmentModel> chatAgentSegmentModel { get; set; }
        public ChatStatisticsAgentSegmentRecordCountModel chatStatisticsAgentSegmentRecordCountModel { get; set; }
        public List<ChatStatisticsSkillSegmentModel> chatStatisticsSkillSegmentModel { get; set; }
        public ChatStatisticsSkillSegmentRecordCountModel chatStatisticsSkillSegmentRecordCountModel { get; set; }
    }
}

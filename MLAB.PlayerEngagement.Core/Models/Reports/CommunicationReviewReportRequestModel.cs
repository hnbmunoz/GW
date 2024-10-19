namespace MLAB.PlayerEngagement.Core.Models.Reports
{
    public class CommunicationReviewReportRequestModel: BaseModel
    {
        public string DisplayResultsGrouping { get; set; }
        public string RevieweeTeamIds { get; set; }
        public string RevieweeIds { get; set; }
        public string ReviewerIds { get; set; }
        public string CommunicationRangeStart { get; set; }
        public string CommunicationRangeEnd { get; set; }
        public int? ReviewPeriod { get; set; }
        public int? HasLineComments { get; set; }
        public int SectedIds { get; set; }
    }
}

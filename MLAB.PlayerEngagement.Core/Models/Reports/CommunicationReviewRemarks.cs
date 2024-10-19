namespace MLAB.PlayerEngagement.Core.Models.Reports
{
    public class CommunicationReviewRemarks
    {
        public string ReviewPeriodName { get; set; }
        public string Reviewer { get; set; }
        public string Reviewee { get; set; }
        public string RevieweeTeamName { get; set; }
        public long ReviewID { get; set; }
        public long CommunicationID { get; set; }
        public string ExternalID { get; set; }
        public string Topic { get; set; }
        public string Ranking { get; set; }
        public string MeasurementName { get; set; }
        public string Code { get; set; }
        public string Score { get; set; }
        public string Criteria { get; set; }
        public string AdditionalRemark { get; set; }
        public string Suggestion { get; set; }
        public string ReviewDate { get; set; }
    }
}

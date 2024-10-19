namespace MLAB.PlayerEngagement.Core.Models.Reports
{
    public class CommunicationReviewScoreData
    {
        public string PeriodName { get; set; }
        public string Reviewer { get; set; }
        public string Reviewee { get; set; }
        public string RevieweeTeamName { get; set; }
        public long ReviewID { get; set; }
        public long CommunicationID{ get; set; }
        public string ExternalID { get; set; }
        public string ReviewScore{ get; set; }
        public string ReviewBenchmark{ get; set; }
        public string ReviewDate{ get; set; }

    }
}

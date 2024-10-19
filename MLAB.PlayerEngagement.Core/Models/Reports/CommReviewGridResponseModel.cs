using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.Reports
{
    public class CommReviewGridResponseModel
    {
        public int ReviewId { get; set; }
        public string ReviewPeriodName { get; set; }
        public string ReviewDate { get; set; }
        public string CommCreateDate { get; set; }
        public int CommunicationId { get; set; }
        public string ExternalId { get; set; }
        public string TopicName { get; set; }
        public string SubTopicName { get; set; }
        public string ReviewScore { get; set; }
        public string Summary { get; set; }
        public string Reviewer { get; set; }
        public string CaseID { get; set; }
    }
}

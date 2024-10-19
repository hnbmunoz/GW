using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class UpdateCommReviewTaggingModel
    {
        public int CommunicationReviewId { get; set; }
        public int CommunicationId { get; set; }
        public int RevieweeId { get; set; }
        public int ReviewerId { get; set; }
        public int ReviewPeriodId { get; set; }
    }
}

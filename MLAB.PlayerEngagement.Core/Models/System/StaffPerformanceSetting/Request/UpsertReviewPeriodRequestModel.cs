using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request
{
    public class UpsertReviewPeriodRequestModel
    {
        public int CommunicationReviewPeriodId { get; set; }
        public string CommunicationReviewPeriodName { get; set; }
        public DateTime RangeStart { get; set; }
        public DateTime RangeEnd { get; set; }
        public DateTime ValidationPeriod { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }


    }
}

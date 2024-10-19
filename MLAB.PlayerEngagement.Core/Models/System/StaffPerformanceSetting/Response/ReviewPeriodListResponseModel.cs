using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Response
{
    public class ReviewPeriodListResponseModel
    {
        public List<ReviewPeriodListModel> ReviewPeriodList { get; set; }
        public int TotalRecords { get; set; }
    }

    public class ReviewPeriodListModel
    {
        public int CommunicationReviewPeriodId { get; set; }
        public string CommunicationReviewPeriodName { get; set; }
        public string RangeStart { get; set; }
        public string RangeEnd { get; set; }
        public string ValidationPeriod { get; set; }
        public bool Status { get; set; }
    }
}

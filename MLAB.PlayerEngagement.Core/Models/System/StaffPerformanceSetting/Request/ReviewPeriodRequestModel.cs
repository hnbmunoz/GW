using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request
{
    public class ReviewPeriodRequestModel : BaseModel
    {
        public string CommunicationReviewPeriodName { get; set; }
        public int? Status { get; set; }
        public int OffsetValue { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string UserId { get; set; }
    }
}

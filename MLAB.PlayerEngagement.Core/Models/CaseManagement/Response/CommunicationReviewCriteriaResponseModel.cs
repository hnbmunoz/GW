using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class CommunicationReviewCriteriaResponseModel
    {
        public int QualityReviewMeasurementId { get; set; }
        public int QualityReviewCriteriaId { get; set; }
        public string CriteriaName { get; set; }
        public string Code { get; set; }
        public float Score { get; set; }
        public int QualityReviewRankingId { get; set; }
        public string RankingName { get; set; }
        public bool IsAutoFailed { get; set; }
        public int Order { get; set; }
    }
}

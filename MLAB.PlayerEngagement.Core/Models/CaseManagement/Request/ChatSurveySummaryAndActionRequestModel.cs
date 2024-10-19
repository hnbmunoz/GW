using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class ChatSurveySummaryAndActionRequestModel : BaseModel
    {
        public string Action { get; set; }
        public string Summary { get; set; }
        public int ChatSurveyId { get; set; }
    }
}

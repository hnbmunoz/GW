using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CustomerCaseCommunicationTabsModel
    {
       public CustomerCaseCommunicationInfoModel CustomerCaseCommunicationInfo { get; set; }
       public List<PCSDataModel> PCSData { get; set; }
        public List<CustomerCaseCommunicationFeedbackResponseModel> CustomerCaseCommunicationFeedback { get; set; }
        public List<CustomerCaseCommunicationSurveyResponseModel> CustomerCaseCommunicationSurvey { get; set; }
    }
}

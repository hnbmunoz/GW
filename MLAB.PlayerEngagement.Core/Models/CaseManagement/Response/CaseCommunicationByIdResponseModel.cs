using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class CaseCommunicationByIdResponseModel
    {
            public CustomerCaseCommunicationInfoModel CustomerCaseCommunicationInfo { get; set; }
            public List<PCSDataModel> PcsData{ get; set; }
            public List<CustomerCaseCommunicationFeedbackResponseModel> CustomerCaseCommunicationFeedbacks { get; set; }
            public List<CustomerCaseCommunicationSurveyResponseModel> CustomerCaseCommunicationSurveys { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class CustomerCaseCommunicationSurveyResponseModel
    {
        public int CommunicationSurveyQuestionId { get; set; }
        public int CaseCommunicationId { get; set; }
        public int SurveyTemplateId { get; set; }
        public string SurveyTemplateName { get; set; }
        public int SurveyQuestionId { get; set; }
        public string SurveyQuestionName { get; set; }
        public int SurveyQuestionAnswersId { get; set; }
        public string SurveyAnswerName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}

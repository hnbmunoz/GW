using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Response;

namespace MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;

public class SurveyTemplateResponse
{
    public SurveyTemplateInfoResponse SurveyTemplate { get; set; }
    public List<SurveyQuestionResponse> SurveyQuestions { get; set; }
    public List<SurveyQuestionAnswerResponse> SurveyQuestionAnswers { get; set; }
}

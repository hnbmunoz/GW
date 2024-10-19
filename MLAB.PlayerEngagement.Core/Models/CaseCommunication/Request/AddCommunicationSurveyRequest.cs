namespace MLAB.PlayerEngagement.Core.Models;

public class AddCommunicationSurveyRequest
{
    public int CommunicationSurveyQuestionId { get; set; }
    public int CaseCommunicationId { get; set; }
    public int SurveyTemplateId { get; set; }
    public int SurveyQuestionId { get; set; }
    public int SurveyQuestionAnswersId { get; set; }
    public string SurveyAnswer { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

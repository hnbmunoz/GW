namespace MLAB.PlayerEngagement.Core.Models.Survey;

public class SaveSurveyTemplateModel : BaseModel
{
    public int SurveyTemplateId { get; set; }
    public string SurveyTemplateName { get; set; }
    public bool SurveyTemplateStatus { get; set; }
    public string SurveyTemplateDescription { get; set; }
    public int CaseTypeId { get; set; }
    public int MessageTypeId { get; set; }
    public List<SurveyTemplateQuestionModel> SurveyTemplateQuestions { get; set; }
}

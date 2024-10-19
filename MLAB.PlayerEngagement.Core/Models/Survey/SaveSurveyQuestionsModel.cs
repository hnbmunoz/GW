namespace MLAB.PlayerEngagement.Core.Models.Survey;

public class SaveSurveyQuestionsModel : BaseModel
{
    public int SurveyQuestionId { get; set; }
    public string SurveyQuestionName { get; set; }
    public long FieldTypeId { get; set; }
    public bool IsActive { get; set; }
    public List<SurveyAnswerModel> SurveyQuestionAnswers { get; set; }
}

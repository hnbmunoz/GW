namespace MLAB.PlayerEngagement.Core.Models.Survey;

public class SurveyTemplateQuestionModel
{
    public long Id { get; set; }
    public int SurveyTemplateId { get; set; }
    public int SurveyQuestionId { get; set; }
    public int OrderNo { get; set; }
    public bool Status { get; set; }
    public bool IsMandatory { get; set; }

}

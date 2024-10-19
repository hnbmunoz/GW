namespace MLAB.PlayerEngagement.Core.Models.CaseCommunication.Response;

public class SurveyQuestionResponse
{
    public int SurveyQuestionId { get; set; }
    public string SurveyQuestionName { get; set; }
    public int FieldTypeId { get; set; }
    public string FieldTypeName { get; set; }
    public Boolean IsMandatory { get; set; }
}

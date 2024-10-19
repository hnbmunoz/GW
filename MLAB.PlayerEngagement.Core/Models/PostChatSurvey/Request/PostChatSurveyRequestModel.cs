namespace MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;

public class PostChatSurveyRequestModel : BaseModel
{
    public long PostChatSurveyId { get; set; }
    public long BrandId { get; set; }
    public long MessageTypeId { get; set; }
    public string LicenseId { get; set; }
    public string QuestionId { get; set; }
    public string QuestionMessage { get; set; }
    public string QuestionMessageEN { get; set; }
    public bool FreeText { get; set; }
    public bool Status { get; set; }
    public string SurveyId  { get; set; }
    public long CsatTypeId { get; set; }
    public List<SkillsUdtModel> SkillsList { get; set; }
}

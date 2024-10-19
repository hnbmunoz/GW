namespace MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Response;

public class PostChatSurveyResponseModel
{
    public long PostChatSurveyId { get; set; }
    public string BrandId { get; set; }
    public long MessageTypeId { get; set; }
    public string MessageType { get; set; }
    public string License { get; set; }
    public string QuestionId { get; set; }
    public string QuestionMessage { get; set; }
    public string QuestionMessageEN { get; set; }
    public bool FreeText { get; set; }
    public bool Status { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string SurveyId { get; set; }
    public long CsatTypeId { get; set; }
    public List<SkillsUdtModel> SkillsList { get; set; }
}

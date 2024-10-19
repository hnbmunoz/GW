namespace MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;

public class ValidatePostChatSurveyQuestionIDModel
{
    public string QuestionId { get; set; }
    public long PostChatSurveyId { get; set; }
    public string SkillId { get; set; }
}

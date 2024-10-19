using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ISurveyAgentWidgetFactory
{
    Task<List<LanguageResponse>> GetLanguageOptionAsync(string platform);
    Task<List<SubtopicLanguageOptionModel>> GetSubtopicnameByIdAsync(long topicLanguageId, string currencyCode, long languageId, string platform);
    Task<List<TopicLanguageOptionModel>> GetTopicNameByCodeAsync(string languageCode, string currencyCode, string platform);
    Task<UserValidationResponse> UserValidationAsync(UserValidationRequest request, string platform);
    Task<LivePersonAgentSurveyResponse> GetAgentSurveyByConversationIdAsync(string conversationId, string platform);
    Task<UserBrandBySkillNameModel> GetBrandBySkillNameAsync(string skillName, string licenseId, string platform);
    Task<List<FeedbackTypeOptionModel>> GetASWFeedbackTypeOptionList(string platform);

    Task<List<FeedbackCategoryOptionModel>> GetASWFeedbackCategoryOptionById(int feedbackTypeId, string platform);

    Task<List<FeedbackAnswerOptionModel>> GetASWFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform);
    Task<AgentSkillDetailsModel> GetSkillDetailsBySkillIDAsync(string skillId, string licenseId, string platform);
    Task<string> GetLiveChatLicenseIDAsync(string platform);
    Task<List<CampaignOptionModel>> GetAllActiveCampaignByUsername(string username, string platform);
}

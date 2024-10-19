using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Repositories;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ISurveyAgentWidgetService
{
    Task<UserValidationResponse> UserValidationAsync(UserValidationRequest request, string platform);
    Task<LivePersonAgentSurveyResponse> GetAgentSurveyByConversationIdAsync(string conversationId, string platform);
    Task<UserBrandBySkillNameModel> GetBrandBySkillNameAsync(string skillName, string licenseId, string platform);
    Task<List<LanguageResponse>> GetLanguageOptionAsync(string platform);
    Task<List<TopicLanguageOptionModel>> GetTopicNameByCodeAsync(string languageCode, string currencyCode, string platform);
    Task<List<SubtopicLanguageOptionModel>> GetSubtopicnameByIdAsync(long topicLanguageId, string currencyCode, long languageId, string platform);
    Task<List<FeedbackTypeOptionModel>> GetASWFeedbackTypeOptionList(string platform);

    Task<List<FeedbackCategoryOptionModel>> GetASWFeedbackCategoryOptionById(int feedbackTypeId, string platform);

    Task<List<FeedbackAnswerOptionModel>> GetASWFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform);
    Task<AgentSkillDetailsModel> GetSkillDetailsBySkillIDAsync(string skillId, string licenseId, string platform);
    Task<string> GetLiveChatLicenseIDAsync(string platform);
    Task<List<CampaignOptionModel>> GetAllActiveCampaignByUsername(string username, string platform);
}

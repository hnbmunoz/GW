using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Repositories;

namespace MLAB.PlayerEngagement.Application.Services;

public class SurveyAgentWidgetService : ISurveyAgentWidgetService
{
    private readonly ISurveyAgentWidgetFactory _surveyAgentWidgetFactory;
    public SurveyAgentWidgetService(ISurveyAgentWidgetFactory surveyAgentWidgetFactory)
    {
        _surveyAgentWidgetFactory= surveyAgentWidgetFactory;
    }
    public async Task<LivePersonAgentSurveyResponse> GetAgentSurveyByConversationIdAsync(string conversationId, string platform)
    {
        var asw = await _surveyAgentWidgetFactory.GetAgentSurveyByConversationIdAsync(conversationId, platform);
        return asw;
    }
    public async Task<UserBrandBySkillNameModel> GetBrandBySkillNameAsync(string skillName, string licenseId, string platform)
    {
        var asw_brand = await _surveyAgentWidgetFactory.GetBrandBySkillNameAsync(skillName, licenseId, platform);
        return asw_brand;
    }

    public async Task<List<LanguageResponse>> GetLanguageOptionAsync(string platform)
    {
       var lookup = await _surveyAgentWidgetFactory.GetLanguageOptionAsync(platform);
        return lookup;
    }

    public async Task<List<SubtopicLanguageOptionModel>> GetSubtopicnameByIdAsync(long topicLanguageId, string currencyCode, long languageId, string platform)
    {
        var lookup = await _surveyAgentWidgetFactory.GetSubtopicnameByIdAsync(topicLanguageId, currencyCode, languageId, platform);
        return lookup;
    }

    public async Task<List<TopicLanguageOptionModel>> GetTopicNameByCodeAsync(string languageCode, string currencyCode, string platform)
    {
        var lookup = await _surveyAgentWidgetFactory.GetTopicNameByCodeAsync(languageCode, currencyCode, platform);
        return lookup;
    }

    public async Task<UserValidationResponse> UserValidationAsync(UserValidationRequest request, string platform)
    {
        var response = await _surveyAgentWidgetFactory.UserValidationAsync(request, platform);
        return response;
    }

    public async Task<List<FeedbackTypeOptionModel>> GetASWFeedbackTypeOptionList(string platform)
    {
        return await _surveyAgentWidgetFactory.GetASWFeedbackTypeOptionList(platform);
    }

    public async Task<List<FeedbackCategoryOptionModel>> GetASWFeedbackCategoryOptionById(int feedbackTypeId, string platform)
    {
        return await _surveyAgentWidgetFactory.GetASWFeedbackCategoryOptionById(feedbackTypeId, platform);
    }

    public async Task<List<FeedbackAnswerOptionModel>> GetASWFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform)
    {
        return await _surveyAgentWidgetFactory.GetASWFeedbackAnswerOptionById(request, platform);
    }
    public async Task<AgentSkillDetailsModel> GetSkillDetailsBySkillIDAsync(string skillId, string licenseId, string platform)
    {
        var asw_skillDetails = await _surveyAgentWidgetFactory.GetSkillDetailsBySkillIDAsync(skillId, licenseId, platform);
        return asw_skillDetails;
    }

    public async Task<string> GetLiveChatLicenseIDAsync(string platform)
    {
        return await _surveyAgentWidgetFactory.GetLiveChatLicenseIDAsync(platform);
    }

    public async Task<List<CampaignOptionModel>> GetAllActiveCampaignByUsername(string username, string platform)
    {
        return await _surveyAgentWidgetFactory.GetAllActiveCampaignByUsername(username, platform);
    }
}

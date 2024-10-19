using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class SurveyAgentWidgetFactory : ISurveyAgentWidgetFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ISystemFactory _systemFactory;
    private readonly ILogger _logger;
    public SurveyAgentWidgetFactory(IMainDbFactory mainDbFactory,ISystemFactory systemFactory, ILogger logger)
    {
        _mainDbFactory = mainDbFactory;
        _systemFactory = systemFactory;
        _logger = logger;
    }

     public async Task<LivePersonAgentSurveyResponse> GetAgentSurveyByConversationIdAsync(string conversationId, string platform)
    {
        try
        {

            _logger.LogInfo($" {Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetAgentSurveyByConversationIdAsync");
            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<AgentSurveyCaseCommResponseModel, AgentSurveyFeedback, CampaignOptionModel>
                            (   DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_ASW_GetAgentSurveyByConversationId, new { 
                                    ConversationId = conversationId
                                }
                            ).ConfigureAwait(false);


            var feedbackList = new List<AgentSurveyFeedback>();

            foreach ( var feedback in result.Item2)
            {
                feedbackList.Add(feedback);
            }

            var campaignList = new List<CampaignOptionModel>();

            foreach (var campaign in result.Item3)
            {
                campaignList.Add(campaign);
            }

            var aswresponse = (from x in result.Item1
                             select new LivePersonAgentSurveyResponse()
                             {
                                 AgentSurveyId = x.AgentSurveyId,
                                 UserName = x.UserName,
                                 CurrencyCode = x.CurrencyCode,
                                 TopicId = x.TopicId,
                                 TopicName = x.TopicName,
                                 SubTopicId = x.SubTopicId,
                                 SubTopicName = x.SubTopicName,
                                 LanguageId = x.LanguageId,

                                 CaseStatusId = x.CaseStatusId,
                                 TopicLanguageId = x.TopicLanguageId,
                                 SubtopicLanguageId = x.SubtopicLanguageId,

                                 SubmittedByName = x.SubmittedByName,
                                 SubmittedDate = x.SubmittedDate,

                                 CaseCommunicationDetails = new CaseCommunicationModel
                                 {
                                    CommunicationId = x.CommunicationId,
                                    ConversationId = x.ConversationId,
                                    Username = x.Username,
                                    CaseBrandName = x.CaseBrandName,
                                    CaseStatusName = x.CaseStatusName,
                                    CommunicationCreatedBy = x.CommunicationCreatedBy,
                                    CaseTopic = x.CaseTopic,
                                    CaseSubtopic = x.CaseSubtopic
                                 },

                                 ASWFeedbackDetailsType = feedbackList,
                                 CampaignList = campaignList

                             }).FirstOrDefault();

            return aswresponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetAgentSurveyByConversationIdAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LivePersonAgentSurveyResponse>().FirstOrDefault();
    }

    public async Task<List<LanguageResponse>> GetLanguageOptionAsync(string platform)
    {
        try
        {
            _logger.LogInfo ($" {Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetLanguageOptionAsync");
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LanguageResponse>
                            (   DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_ASW_GetLanguageList, null

                            ).ConfigureAwait(false);
           
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetLanguageOptionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LanguageResponse>().ToList();
    }

    public async Task<List<SubtopicLanguageOptionModel>> GetSubtopicnameByIdAsync(long topicLanguageId, string currencyCode, long languageId, string platform)
    {
        try
        {
            _logger.LogInfo($" {Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetSubtopicnameByIdAsync : [topicLanguageId] -  {topicLanguageId} ");
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<SubtopicLanguageOptionModel>
                            (   DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_ASW_GetSubtopicnameById, new
                                {
                                    TopicLanguageId = topicLanguageId,
                                    CurrencyCode = currencyCode,
                                    LanguageId = languageId,
                                }

                            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetSubtopicnameByIdAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<SubtopicLanguageOptionModel>().ToList();
    }

    public async Task<List<TopicLanguageOptionModel>> GetTopicNameByCodeAsync(string languageCode, string currencyCode, string platform)
    {
         try
        {
            _logger.LogInfo($" {Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetTopicNameByCodeAsync : [languageCode] -  {languageCode} [currencyCode] - {currencyCode} ");
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<TopicLanguageOptionModel>
                            (
                                DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_ASW_GetTopicNameByCode, new
                                {
                                    LanguageCode = languageCode,
                                    CurrencyCode = currencyCode
                                }

                            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetTopicNameByCodeAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<TopicLanguageOptionModel>().ToList();  
    }

    public async Task<UserValidationResponse> UserValidationAsync(UserValidationRequest request, string platform)
    {
        try
        {
            var userValidationResponse  = new UserValidationResponse();
            _logger.LogInfo($" {Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | UserValidationAsync : [UserValidationRequest] -  {request} ");
            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<AgentSurveyValidationModel, PlayerInformationModel, PlayerTransactionModel>
                            (   DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_ASW_ValidatePlayerAndSkillMappingBrand, new
                                {
                                    BrandName = request.BrandName,
                                    SkillName = request.SkillName,
                                    UserName = request.UserName,
                                    LicenseId = request.LicenseId,
                                    UserId = request.UserId
                                }

                            ).ConfigureAwait(false);

            var userASW = result.Item1.FirstOrDefault();

            userValidationResponse.IsDataAccessible = userASW?.IsDataAccessible ?? false;
            userValidationResponse.IsValidUser = userASW?.IsValidPlayer ?? false;
            userValidationResponse.IsValidBrand = userASW?.IsValidBrand ?? false;
            userValidationResponse.PlayerCurrency = userASW?.CurrencyCode ?? "";

            userValidationResponse.PlayerInfoDetails = result.Item2.FirstOrDefault();
            userValidationResponse.PlayerTransactionDetails = result.Item3.ToList();



            return userValidationResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | UserValidationAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<UserValidationResponse>().FirstOrDefault();
    }

    public async Task<UserBrandBySkillNameModel> GetBrandBySkillNameAsync(string skillName, string licenseId, string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetBrandBySkillNameAsync - [skillName] -  {skillName} ");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<UserBrandBySkillNameModel>
                            (
                                DatabaseFactories.MLabDB,
                                StoredProcedures.USP_ASW_GetBrandBySkillName, new
                                    {
                                        SkillName = skillName,
                                        LicenseId = licenseId
                                }
                            ).ConfigureAwait(false);

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetBrandBySkillNameAsync : [Exception] - {ex.Message}");
            return Enumerable.Empty<UserBrandBySkillNameModel>().FirstOrDefault();
        }
    }

    public async Task<List<FeedbackTypeOptionModel>> GetASWFeedbackTypeOptionList(string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetASWFeedbackTypeOptionList");

            var result = await _mainDbFactory.ExecuteQueryAsync<FeedbackTypeOptionModel>(DatabaseFactories.MLabDB,
                    StoredProcedures.USP_GetFeedbackTypeOptionList, null
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetASWFeedbackTypeOptionList : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<FeedbackTypeOptionModel>().ToList();
    }

    public async Task<List<FeedbackCategoryOptionModel>> GetASWFeedbackCategoryOptionById(int feedbackTypeId, string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetASWFeedbackCategoryOptionById - [feedbackTypeId: {feedbackTypeId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<FeedbackCategoryOptionModel>(DatabaseFactories.MLabDB,
                    StoredProcedures.USP_GetFeedbackCategoryOptionById, new { feedbackTypeId = feedbackTypeId }
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetASWFeedbackCategoryOptionById : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<FeedbackCategoryOptionModel>().ToList();
    }

    public async Task<List<FeedbackAnswerOptionModel>> GetASWFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetASWFeedbackAnswerOptionById - [feedbackAnswerOptionById: {JsonConvert.SerializeObject(request)}]");
            var result = await _systemFactory.GetFeedbackAnswerOptionById(request, platform);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetASWFeedbackAnswerOptionById : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<FeedbackAnswerOptionModel>().ToList();
    }

    public async Task<AgentSkillDetailsModel> GetSkillDetailsBySkillIDAsync(string skillId, string licenseId, string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetSkillDetailsBySkillIDAsync - [skillId] -  {skillId} ");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<AgentSkillDetailsModel>
                            (   DatabaseFactories.MLabDB,
                                StoredProcedures.USP_ASW_GetSkillDetailsBySkillID, new
                                {
                                    SkillID = skillId,
                                    LicenseID = licenseId
                                }
                            ).ConfigureAwait(false);

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetSkillDetailsBySkillIDAsync : [Exception] - {ex.Message}");
            return Enumerable.Empty<AgentSkillDetailsModel>().FirstOrDefault();
        }
    }


    public async Task<string> GetLiveChatLicenseIDAsync(string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetLiveChatLicenseIDAsync");

            var result = await _mainDbFactory.ExecuteQueryAsync<AppConfigSettingsModel>( DatabaseFactories.MLabDB,
                     StoredProcedures.USP_GetAppConfigSetting,
                     new
                     {
                         ApplicationId = 284
                     }).ConfigureAwait(false);

           var license = result.FirstOrDefault();

           if (license != null)
            {
                return license.License;
            } else
            {
                return null;
            }
        }
        catch ( Exception ex ) 
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetLiveChatLicenseIDAsync : [Exception] - {ex.Message}");
            return Enumerable.Empty<string>().FirstOrDefault();
        }
    }

    public async Task<List<CampaignOptionModel>> GetAllActiveCampaignByUsername(string username, string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetAllActiveCampaignByUsername - [username] -  {username} ");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignOptionModel>
                            (   DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_ASW_GetAllActiveCampaignByUsername, new
                                {
                                    Username = username
                                }
                            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch(Exception ex)
        {
            _logger.LogError($"{Factories.SurveyAgentWidgetFactory} {(platform != null ? "| " + platform : "")} | GetAllActiveCampaignByUsername : [Exception] - {ex.Message}");
            return Enumerable.Empty<CampaignOptionModel>().ToList();
        }
    }

}

using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Response;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Response;
using MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Udt;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Response;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request;
using MLAB.PlayerEngagement.Core.Models.System.Response;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ISystemFactory
{
    Task<bool> AddOperatorAsync(int operatorId, string operatorName, int status, int createdBy, List<BrandTypeModel> brandType, List<BrandCurrencyTypeModel> currencyType);
    Task<List<BrandInfoModel>> GetAllBrandAsync(long? userId, long? platformId);
    Task<List<VIPLevelModel>> GetAllVIPLevelAsync(long? userId);
    Task<List<VIPLevelModel>> GetAllVIPLevelByBrandAsync(long? userId, string brandId);
    Task<List<OperatorInfoModel>> GetAllOperatorAsync();
    Task<List<CaseTypeModel>> GetCaseTypeListAsync();
    Task<Tuple<List<OperatorBrandModel>, List<BrandCurrencyModel>>> GetOperatorBrandCurrencyByOperatorIdAsync(string operatorIds);
    Task<Tuple<List<OperatorModel>, List<BrandModel>, List<BrandCurrencyModel>>> GetOperatorByIdAsync(int operatorId);
    Task<List<OperatorBrandModel>> GetOperatorListByFilterAsync(int operatorId, string operatorName, int brandId, string brandName);
    Task<bool> UpdateOperatorAsync(int operatorId, string operatorName, int status, int updatedBy, List<BrandTypeModel> brandType, List<BrandCurrencyTypeModel> brandCurrencyType);
    Task<List<CurrencyModel>> GetAllCurrencyAsync(long? userId);
    Task<Tuple<List<Int64>,List<string>>> GetBrandExistingLists(string brandIds, string brandNames);
    Task<List<FieldTypeModel>> GetAllFieldTypeAsync();
    Task<List<TopicModel>> GetAllTopicAsync();
    Task<List<MessageTypeModel>> GetAllMessageTypeAsync();
    Task<bool> ValidateSubtopicName(string subtopicName, long subtopicId);
    Task<bool> GetTopicByNameAsync(string topicName, int caseTypeId);
    Task<int> ValidateCodelistName(string codelistName);
    Task<int> ValidateCodelistTypeName(string codelistTypeName);
    Task<List<CodeListTypeModel>> GetAllCodeListTypeAsync();
    Task<bool> ValidateSurveyQuestion(ValidateSurveyQuestionModel surveyQuestion);
    Task<bool> ValidateSurveyQuestionAnswer(ValidateSurveyQuestionAnswerModel surveyQuestionAnswer);
    Task<bool> ValidateSurveyTemplateName(ValidateSurveyTemplateNameModel surveyTemplateName);
    Task<bool> ValidateSurveyTemplateQuestion(ValidateSurveyTemplateQuestionModel surveyTemplateQuestion);
    Task<int> ValidateMessageResponseNameAsync(string messageResponseName);
    Task<int> ValidateMessageTypeNameAsync(string messageTypeName);
    Task<int> ValidateMessageStatusNameAsync(string messageStatusName);
    Task<int> ValidateFeedbackTypeNameAsync(string feedbackTypeName);
    Task<int> ValidateFeedbackCategoryNameAsync(string feedbackCategory);
    Task<int> ValidateFeedbackAnswerNameAsync(string feedbackResponseName);
    Task<bool> DeactivateSurveyQuestion(int SurveyQuestionId);
    Task<bool> DeactivateSurveyTemplate(int SurveyTemplateId);
    Task<SystemLookupsResponseModel> GetSystemLookupsAsync();//NOSONAR
    #region Option
    Task<List<CaseTypeOptionModel>> GetCaseTypeOptionList();//NOSONAR
    Task<List<TopicOptionModel>> GetTopicOptionList();//NOSONAR
    Task<List<LookupModel>> GetTopicOptionByBrandId(long brandId);//NOSONAR
    Task<List<SubtopicOptionModel>> GetSubtopicOptionById(int topicId);//NOSONAR
    Task<List<MessageTypeOptionModel>> GetMessageTypeOptionList(string channelTypeId);//NOSONAR
    Task<List<MessageStatusOptionModel>> GetMessageStatusOptionById(int messageTypeId);//NOSONAR
    Task<List<MessageResponseOptionModel>> GetMessageResponseOptionById(int messageStatusId);//NOSONAR option list
    Task<List<FeedbackTypeOptionModel>> GetFeedbackTypeOptionList(string platform);//NOSONAR
    Task<List<FeedbackCategoryOptionModel>> GetFeedbackCategoryOptionById(int feedbackTypeId, string platform);//NOSONAR
    Task<List<FeedbackAnswerOptionModel>> GetFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform);//NOSONAR
    Task<List<MasterReferenceModel>> GetMasterReferenceList(string masterReferenceId);//NOSONAR
    #endregion
    Task<SurveyTemplateResponse> GetCommunicationSurveyQuestionAnswers(int campaignId);//NOSONAR
    Task<List<MessageStatusResponseModel>> GetMessageStatusResponseListAsync(int campaignId);//NOSONAR
    Task<List<CurrencyModel>> GetCurrencyCodeAsync();//NOSONAR
    Task<List<UserGridCustomDisplayResponseModel>> LoadUserGridCustomDisplayAsync(long userId, string module);//NOSONAR
    Task<Tuple<List<CurrencyFilterModel>, Int64>> GetCurrencyByFilterAsync(PlayerConfigurationRequestModel filter);
    Task<List<LookupModel>> GetSkillsByLicenseIdAsync(string LicenseId);
    Task<PostChatSurveyLookupsResponseModel> GetPCSLookupsAsync();
    Task<List<GetTopicOrderResponseModel>> GetTopicOrderAsync();
    Task<List<GetSubtopicOrderUdtViewModel>> GetSubtopicOrderAsync(int topicId);
    Task<List<GetAppConfigSettingByApplicationIdResponseModel>> GetAppConfigSettingByApplicationIdAsync(int ApplicationId);

    Task<bool> TogglePostChatSurveyAsync(PostChatSurveyToggleRequestModel request);
    Task<bool> ToggleSkillAsync(SkillToggleRequestModel request);
    Task<bool> ValidateSkillAsync(ValidateSkillRequestModel request);
    Task<Tuple<PostChatSurveyResponseModel, List<SkillsUdtModel>>> GetPostChatSurveyByIdAsync(PostChatSurveyIdRequestModel request);
    Task<bool> UpdateSubtopicStatusAsync(long subTopicId, string userId, bool isActive);
    Task<bool> UpdateTopicStatusAsync(UpdateTopicStatusRequestModel request);
    Task<bool> ValidatePostChatSurveyQuestionID(ValidatePostChatSurveyQuestionIDModel questionModel);
    Task<List<GetTopicOptionsReponse>> GetTopicOptionsAsync();
    Task<List<TopicLanguageOptionModel>> GetTopicOptionsByCodeAsync(string languageCode, long caseTypeId);
    Task<List<SubtopicLanguageOptionModel>> GetSubtopicOptionsByIdAsync(long topicLanguageId);
    Task<List<PCSCommunicationProviderOptionResponseModel>> GetPCSCommunicationProviderOptionAsync();
    Task<List<PCSCommunicationSummaryActionResponseModel>> GetPCSCommunicationSummaryActionAsync();
    Task<List<CurrencyModel>> GetCurrencyWithNullableRestrictionAsync(long? userId);
    Task<List<LookupModel>> GetRemAgentsByUserAccessAsync(long? userId);
    Task<List<LookupModel>> GetDateByOptionList();
    Task<List<CountryListResponse>> GetCountryWithAccessRestrictionAsync(long? userId);
    Task<StaffPerformaneSettingResponseModel> GetStaffPermormanceSettingList(StaffPerformanceSettingRequestModel request);
    Task<StaffPerformanceInfoResponseModel> GetStaffPerformanceInfoAsync(int id);
    Task<bool> UpsertReviewPeriodAsync(UpsertReviewPeriodRequestModel request);
}

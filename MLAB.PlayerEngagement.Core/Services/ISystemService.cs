using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Request;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Response;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;
using MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Udt;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Response;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Response;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request;
using MLAB.PlayerEngagement.Core.Models.System.Response;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ISystemService
{
    Task<List<AllCurrencyResponse>> GetAllCurrenciesAsync(long? userId);
    Task<Tuple<bool, string>> AddOperatorAsync(OperatorRequest operatorDetail);
    Task<Tuple<bool, string>> UpdateOperatorAsync(OperatorRequest operatorDetail);
    Task<OperatorResponse> GetOperatorByIdAsync(int operatorId);
    Task<List<CaseTypeModel>> GetCaseTypeListAsync();
    Task<List<OperatorListResponse>> GetOperatorListByFilterAsync(int operatorId, string operatorName, int brandId, string brandName);
    Task<Tuple<bool, string>> GetBrandExistingListAsync(string brandIds, string brandNames);
    Task<List<OperatorResponse>> GetOperatorDetailsAsync(string operatorIds);
    Task<List<BrandInfoModel>> GetAllBrandAsync(long? userId, long? platformId);
    Task<List<VIPLevelModel>> GetAllVIPLevelAsync(long? userId);
    Task<List<VIPLevelModel>> GetAllVIPLevelByBrandAsync(long? userId, string brandId);
    Task<List<OperatorInfoModel>> GetAllOperatorAsync();
    Task<List<FieldTypeModel>> GetAllFieldTypeAsync();
    Task<List<TopicModel>> GetAllTopicAsync();
    Task<List<MessageTypeModel>> GetAllMessageTypeAsync();
    Task<Tuple<int, string>> ValidateCodelistTypeNameAsync(string codeListTypeName);
    Task<bool> GetTopicByNameAsync(string topicName, int caseTypeId);
    Task<Tuple<int, string>> ValidateCodelistNameAsync(string codeListName);
    Task<bool> ValidateSubtopicNameAsync(string subtopicName, long subtopicId);
    Task<List<CodeListTypeModel>> GetAllCodeListTypeAsync();
    Task<bool> DeactivateSurveyQuestion(int SurveyQuestionId);
    Task<bool> DeactivateSurveyTemplate(int SurveyTemplateId);
    Task<bool> UpdateSubtopicStatusAsync(long subTopicId, string userId, bool isActive);
    Task<bool> UpdateTopicStatusAsync(UpdateTopicStatusRequestModel request);

    #region Survey
    Task<bool> ValidateSurveyQuestionAsync(ValidateSurveyQuestionModel request);
    Task<bool> ValidateSurveyQuestionAnswerAsync(ValidateSurveyQuestionAnswerModel request);
    Task<bool> ValidateSurveyTemplateNameAsync(ValidateSurveyTemplateNameModel request);
    Task<bool> ValidateSurveyTemplateQuestionAsync(ValidateSurveyTemplateQuestionModel request);

    #endregion

    Task<Tuple<int, string>> ValidateMessageResponseNameAsync(string messageResponseName);
    Task<Tuple<int, string>> ValidateMessageTypeNameAsync(string messageTypeName);
    Task<Tuple<int, string>> ValidateMessageStatusNameAsync(string messageStatusName);

    Task<Tuple<int, string>> ValidateFeedbackTypeNameAsync(string feedbackTypeName);
    Task<Tuple<int, string>> ValidateFeedbackCategoryNameAsync(string feedbackCategory);
    Task<Tuple<int, string>> ValidateFeedbackAnswerNameAsync(string feedbackResponseName);


    Task<SystemLookupsResponseModel> GetSystemLookupsAsync();
    #region Option

    Task<List<CaseTypeOptionModel>> GetCaseTypeOptionList();
    Task<List<TopicOptionModel>> GetTopicOptionList();
    Task<List<LookupModel>> GetTopicOptionByBrandId(long brandId);
    Task<List<SubtopicOptionModel>> GetSubtopicOptionById(int topicId);
    Task<List<MessageTypeOptionModel>> GetMessageTypeOptionList(string channelTypeId);
    Task<List<MessageStatusOptionModel>> GetMessageStatusOptionById(int messageTypeId);
    Task<List<MessageResponseOptionModel>> GetMessageResponseOptionById(int messageStatusId);
    Task<List<FeedbackTypeOptionModel>> GetFeedbackTypeOptionList(string platform);
    Task<List<FeedbackCategoryOptionModel>> GetFeedbackCategoryOptionById(int feedbackTypeId, string platform);
    Task<List<FeedbackAnswerOptionModel>> GetFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform);
    Task<List<MasterReferenceModel>> GetMasterReferenceList(string masterReferenceId);

    #endregion
    Task<SurveyTemplateResponse> GetCommunicationSurveyQuestionAnswers(int campaignId);
    Task<List<MessageStatusResponseModel>> GetMessageStatusResponseListAsync(int campaignId);
    Task<List<CurrencyModel>> GetCurrencyCodeAsync();

    Task<List<UserGridCustomDisplayResponseModel>> LoadUserGridCustomDisplayAsync(long userId, string module);
    Task<CurrencyListResponseModel> GetCurrencyByFilterAsync(PlayerConfigurationRequestModel filter);
    Task<List<LookupModel>> GetSkillsByLicenseIdAsync(string LicenseId);
    Task<PostChatSurveyLookupsResponseModel> GetPCSLookupsAsync();
    Task<List<GetTopicOrderResponseModel>> GetTopicOrderAsync();
    Task<bool> TogglePostChatSurveyAsync(PostChatSurveyToggleRequestModel request);
    Task<bool> ToggleSkillAsync(SkillToggleRequestModel request);
    Task<bool> ValidateSkillAsync(ValidateSkillRequestModel request);
    Task<List<GetSubtopicOrderUdtViewModel>> GetSubtopicOrderAsync(int topicId);
    Task<PostChatSurveyResponseModel> GetPostChatSurveyByIdAsync(PostChatSurveyIdRequestModel request);
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
    Task<List<GetAppConfigSettingByApplicationIdResponseModel>> GetAppConfigSettingByApplicationIdAsync(int ApplicationId);
    Task<StaffPerformaneSettingResponseModel> GetStaffPermormanceSettingList(StaffPerformanceSettingRequestModel request);
    Task<StaffPerformanceInfoResponseModel> GetStaffPerformanceInfoAsync(int id);
    Task<bool> UpsertReviewPeriodAsync(UpsertReviewPeriodRequestModel request);
}
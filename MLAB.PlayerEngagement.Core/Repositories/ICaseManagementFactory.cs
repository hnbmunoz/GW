using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Udt;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Request;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Response;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Request;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Response;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Samespace.Request;
using MLAB.PlayerEngagement.Core.Models.Samespace.Response;

namespace MLAB.PlayerEngagement.Core.Repositories
{
    public interface ICaseManagementFactory
    {
        Task<CustomerCaseModel> GetCustomerCaseByIdAsync(int customerCaseId, long userId);
        Task<Tuple<List<int>, List<CustomerCaseCommunicationModel>>> GetCustomerCaseCommListAsync(CustomerCaseCommunicationListRequest request);
        Task<CaseCommunicationByIdResponseModel> GetCaseCommunicationByIdAsync(int communicationId, long userId);
        Task<List<CommunicationOwnerResponseList>> GetAllCommunicationOwnersAsync();
        Task<List<CaseCommunicationFilterResponse>> GetCaseCommunicationListCsvAsync(CaseCommunicationFilterRequest request);
        Task<PlayerInfoCaseCommunicationResponse> ValidatePlayerCaseCommunicationAsync(long mlabPlayerId);
        Task<CaseInformationResponse> GetCustomerServiceCaseInformationByIdAsync(long caseInformationId, long userId);
        Task<List<PlayerCaseCommunicationResponse>> GetPlayersByUsernameAsync(string username, int brandId, long userId);
        Task<List<PlayerCaseCommunicationResponse>> GetPlayersByPlayerIdAsync(string playerId, int brandId, long userId);
        Task<Tuple<List<ExportPcsResponseModel>, List<ExportQuestionAnswerUdtModel>>> GetPCSQuestionaireListCsvAsync(PCSQuestionaireListByFilterRequestModel request);
        Task<List<PCSCommunicationQuestionsByIdResponseModel>> GetPCSCommunicationQuestionsByIdAsync(long caseCommunicationId);
        Task<List<PCSQuestionaireListByFilterResponseModel>> GetCaseManagementPCSQuestionsByFilterAsync(CaseManagementPCSQuestionsByFilterRequestModel request);
        Task<CaseManagementPcsCommunicationByFilterResponseModel> GetCaseManagementPCSCommunicationByFilterAsync(CaseManagementPCSCommunicationByFilterRequestModel request);
        Task<SurveyTemplateResponse> GetCustomerCaseCommSurveyTemplateByIdAsync(int surveyTemplateId);
        Task<List<CustomerCaseCommunicationFeedbackResponseModel>> GetCaseCommunicationFeedbackByIdAsync(int communicationId);
        Task<List<CustomerCaseCommunicationSurveyResponseModel>> GetCaseCommunicationSurveyByIdAsync(int communicationId);
        Task<List<LookupModel>> GetCustomerCaseSurveyTemplateAsync(int caseTypeId);
        Task<FlyFoneRecordedParameterResponseModel> FlyFoneOutboundCallAsync(FlyFoneOutboundCallRequestModel request);
        Task<List<FlyFoneCallDetailRecordResponseModel>> GetFlyFoneCallDetailRecordsAsync();
        Task<bool> FlyFoneEndOutboundCallAsync(FlyFoneSaveCallDetailRecordRequestModel request);
        Task<bool> FlyFoneFetchDetailRecordsAsync(FlyFoneSaveFetchCallDetailRecordRequestModel request);
        Task<CloudTalkMakeACallResponseModel> CloudTalkMakeACallAsync (CloudTalkMakeACallRequestModel request);
        Task<SamespaceMakeACallResponseModel> SamespaceMakeACallAsync(SamespaceMakeACallRequestModel request);
        Task<int> AppendCaseCommunicationContent(CaseCommunicationContentRequestModel request);
        Task<bool> UpdateCloudTalkCdrByCallingCodeAsync(UpdateCloudTalkCdrByCallingCodeRequestModel request);
        Task<AppConfigSettingsModel> GetFlyFoneAppSettingsAsync();
        Task<UpsertCaseResponse> UpSertCustomerServiceCaseCommunicationAsync(AddCustomerServiceCaseCommunicationRequest request);
        #region Communication Review
        Task<CommunicationReviewLookupResponseModel> GetCommunicationReviewLookupsAsync();  
        Task<bool> ValidateCommunicationReviewLimitAsync(CommunicationReviewLimitRequestModel request);
        Task<bool> InsertCommunicationReviewEventLogAsync(CommunicationReviewEventLogRequestModel request);
        Task<List<CommunicationReviewCriteriaResponseModel>> GetCriteriaListByMeasurementId(int? measurementId);
        Task<List<CommunicationReviewEventLogRequestModel>> GetCommunicationReviewEventLogAsync(int caseCommunicationId);

        Task<bool> UpdateCommReviewPrimaryTaggingAsync(UpdateCommReviewTaggingModel request);
        Task<bool> RemoveCommReviewPrimaryTaggingAsync(RemoveCommReviewPrimaryTaggingModel request);

        #endregion

        #region Samespace VoIP Integration
        Task<bool> UpdateSamespaceCdrByCallingCodeAsync(UpdateSamespaceCdrByCallingCodeRequestModel request, int userId);
        #endregion

        #region Case and Communication
        Task<List<CaseCommunicationAnnotationModel>> GetCaseCommunicationAnnotationByCaseCommunicationIdAsync(long caseCommunicationId);
        Task<bool> UpsertCaseCommunicationAnnotationAsync(CaseCommAnnotationRequestModel request);
        Task<int> ValidateCaseCommunicationAnnotationAsync(long caseCommunicationId, string contentBefore, string contentAfter);
        Task<List<LookupModel>> GetCampaignByCaseTypeId(int caseTypeId);
        #endregion
        Task<List<CampaignOptionModel>> GetEditCustomerServiceCaseCampainNameByUsername(string username, long brandId);

        Task<CustomerCaseChatStatisticsModel> GetChatStatisticsByCommunicationIdAsync(long communicationId);
    }
}

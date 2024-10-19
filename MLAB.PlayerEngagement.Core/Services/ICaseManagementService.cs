using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Request;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Response;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Request;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Response;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Udt;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Samespace.Request;
using MLAB.PlayerEngagement.Core.Models.Samespace.Response;

namespace MLAB.PlayerEngagement.Core.Services
{
    public interface ICaseManagementService
    {
        Task<CustomerCaseModel> GetCustomerCaseByIdAsync(int customerCaseId, long userId);
        Task<CustomerCaseCommunicationListModel> GetCustomerCaseCommListAsync(CustomerCaseCommunicationListRequest request);
        Task<CustomerCaseCommunicationTabsModel> GetCaseCommunicationByIdAsync(int communicationId, long userId);
        Task<List<CommunicationOwnerResponseList>> GetAllCommunicationOwnerAsync();
        Task<List<CaseCommunicationFilterResponse>> GetCaseCommunicationListCsvAsync(CaseCommunicationFilterRequest request);
        Task<PlayerInfoCaseCommunicationResponse> ValidatePlayerCaseCommunicationAsync(long mlabPlayerId);
        Task<CaseInformationResponse>GetCustomerServiceCaseInformationByIdAsync(long caseInformationId, long userId);
        Task<List<PlayerCaseCommunicationResponse>> GetPlayersByUsernameAsync(string username, int brandId, long userId);
        Task<List<PlayerCaseCommunicationResponse>> GetPlayersByPlayerIdAsync(string playerId, int brandId, long userId);
        Task<PcsQuestionaireResponseModel> GetPCSQuestionaireListCsvAsync(PCSQuestionaireListByFilterRequestModel request);
        Task<List<PCSCommunicationQuestionsByIdResponseModel>> GetPCSCommunicationQuestionsByIdAsync(long caseCommunicationId);
        Task<List<PCSQuestionaireListByFilterResponseModel>> GetCaseManagementPCSQuestionsByFilterAsync(CaseManagementPCSQuestionsByFilterRequestModel request);
        Task<CaseManagementPcsCommunicationByFilterResponseModel> GetCaseManagementPCSCommunicationByFilterAsync(CaseManagementPCSCommunicationByFilterRequestModel request);
        Task<SurveyTemplateResponse> GetCustomerCaseCommSurveyTemplateByIdAsync(int surveyTemplateId);
        Task<List<CustomerCaseCommunicationFeedbackResponseModel>> GetCaseCommunicationFeedbackByIdAsync(int communicationId);
        Task<List<CustomerCaseCommunicationSurveyResponseModel>> GetCaseCommunicationSurveyByIdAsync(int communicationId);
        Task<List<LookupModel>> GetCustomerCaseSurveyTemplateAsync(int caseTypeId);
        Task<FlyFoneOutboundCallResponsetModel> FlyFoneOutboundCallAsync(FlyFoneOutboundCallRequestModel request);
        Task<List<FlyFoneCallDetailRecordResponseModel>> GetFlyFoneCallDetailRecordsAsync();
        Task<FormattedFlyFoneCdrUdt> FlyFoneEndOutboundCallAsync(FlyFoneCallDetailRecordRequestModel request);
        Task<bool> FlyFoneFetchDetailRecordsAsync(FlyFoneFetchCallDetailRecordRequestModel request);
        Task<CloudTalkMakeACallWithApiResponseModel> CloudTalkMakeACallAsync(CloudTalkMakeACallRequestModel request);
        Task<CloudTalkCdrResponseModel> CloudTalkGetCallAsync(CloudTalkGetCallRequestModel request);

        Task<UpsertCaseResponse> UpSertCustomerServiceCaseCommunicationAsync(AddCustomerServiceCaseCommunicationRequest request);

        Task<List<LookupModel>> GetCampaignByCaseTypeId(int caseTypeId);

        #region Communication Review
        Task<CommunicationReviewLookupsResponseModel> GetCommunicationReviewLookupsAsync();
        Task<bool> ValidateCommunicationReviewLimitAsync(CommunicationReviewLimitRequestModel request);
        Task<bool> InsertCommunicationReviewEventLogAsync(CommunicationReviewEventLogRequestModel request);
        Task<List<CommunicationReviewCriteriaResponseModel>> GetCriteriaListByMeasurementId (int? measurementId);
         Task<List<CommunicationReviewEventLogRequestModel>> GetCommunicationReviewEventLogAsync(int caseCommunicationId);
        Task<bool> UpdateCommReviewPrimaryTaggingAsync(UpdateCommReviewTaggingModel request);
        Task<bool> RemoveCommReviewPrimaryTaggingAsync(RemoveCommReviewPrimaryTaggingModel request);
        #endregion

        #region Samespace VoIP Integration
        Task<SamespaceGetCallResponseModel> SamespaceGetCallAsync(SamespaceGetCallRequestModel request);
        Task<SamespaceMakeACallWithApiResponseModel> SamespaceMakeACallAsync(SamespaceMakeACallRequestModel request);
        #endregion

        #region Case and Communication
        Task<List<CaseCommunicationAnnotationModel>> GetCaseCommunicationAnnotationByCaseCommunicationIdAsync(long caseCommunicationId);
        Task<bool> UpsertCaseCommunicationAnnotationAsync(CaseCommAnnotationRequestModel request);
        Task<int> ValidateCaseCommunicationAnnotationAsync(long caseCommunicationId, string contentBefore, string contentAfter);
        #endregion

        Task<List<CampaignOptionModel>> GetEditCustomerServiceCaseCampainNameByUsername(string username, long brandId);
        Task<CustomerCaseChatStatisticsModel> GetChatStatisticsByCommunicationIdAsync(long communicationId);
    }
}

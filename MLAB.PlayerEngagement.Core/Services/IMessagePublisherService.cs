using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Administrator;
using MLAB.PlayerEngagement.Core.Models.AgentMonitoring;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Models.Authentication;
using MLAB.PlayerEngagement.Core.Models.CallListValidation;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Models.CampaignPerformance;
using MLAB.PlayerEngagement.Core.Models.Feedback;
using MLAB.PlayerEngagement.Core.Models.Message;
using MLAB.PlayerEngagement.Core.Models.Player;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement;
using MLAB.PlayerEngagement.Core.Models.Segmentation;
using MLAB.PlayerEngagement.Core.Request;
using MLAB.PlayerEngagement.Core.Models.CampaignJourney;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;
using MLAB.PlayerEngagement.Core.Models.CodelistSubtopic.Request;
using MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;
using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.EngagementHub;
using MLAB.PlayerEngagement.Core.Models.Request;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;
using MLAB.PlayerEngagement.Core.Models.SearchLeads;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IMessagePublisherService
{
    Task<bool> GetRoleListAsync(RoleFilterModel roleFilter);
    Task<bool> AddRoleAsync(RoleRequestModel createRole);
    Task<bool> UpdateRoleAsync(RoleRequestModel updateRole);
    Task<bool> GetRoleByIdAsync(RoleIdRequestModel role);
    Task<bool> GetCloneRoleAsync(RoleIdRequestModel role);
    Task<bool> GetAllRoleAsync(BaseModel request);
    Task<bool> GetTeamListAsync(TeamFilterModel teamFilter);
    Task<bool> AddTeamAsync(TeamRequestModel createTeam);
    Task<bool> UpdateTeamAsync(TeamRequestModel updateTeam);
    Task<bool> GetTeamByIdAsync(TeamIdRequestModel team);
    Task<bool> GetUserListAsync(UserFilterModel userFilter);
    Task<bool> GetUserByIdAsync(UserIdRequestModel user);
    Task<bool> GetCommunicationProviderAccountListbyIdAsync(UserIdRequestModel user);
    Task<bool> CreatePasswordAsync(CreateNewPasswordRequest request);
    Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    Task<bool> AddUserAsync(UserRequestModel createUser);
    Task<bool> UpdateUserAsync(UserRequestModel updateUser);
    Task<bool> LockUserAsync(LockUserRequestModel lockUser);
    Task<bool> ImportPlayerAsync(ImportPlayersRequestModel playerImportModel);
    Task<bool> ValidateImportPlayerAsync(ImportPlayersRequestModel playerImportModel);
    Task<bool> GetAllCodelistAsync(BaseModel request);
    Task<bool> AddCodeListAsync(CodeListModel codelist);
    Task<bool> AddCodeListTypeAsync(CodeListTypeModel codelistType);
    Task<bool> GetAllCodelistTypeAsync(BaseModel request);
    Task<bool> AddTopicAsync(CodeListTopicModel request);
    Task<bool> UpSertTopicAsync(UpSertTopicRequestModel request);
    Task<bool> GetTopicByIdAsync(GetTopicByIdRequestModel request);
    Task<bool> GetTopicListByFilterAsync(TopicRequest request);
    Task<bool> UpdateTopicOrderAsync(UpdateTopicOrderRequestModel request);
    Task<bool> UpdateSubtopicOrderAsync(UpdateSubtopicOrderRequestModel request);
    Task<bool> UpdateTopicStatusAsync(UpdateTopicStatusRequestModel request);
    Task<bool> GetSubtopicByFilterAsync(SubTopicRequest request);
    Task<bool> SubmitSubtopicAsync(SubtopicRequestModel request);
    Task<bool> UpsertSubtopicAsync(SubtopicNewRequestModel request);
    Task<bool> GetSubTopicById(SubtopicIdRequestModel request);
    
    #region Survey

    Task<bool> SaveSurveyQuestionAsync(SaveSurveyQuestionsModel request);
    Task<bool> SaveSurveyTemplateAsync(SaveSurveyTemplateModel request);
    Task<bool> GetSurveyQuestionsByFilterAsync(SurveyQuestionsListFilterModel request);
    Task<bool> GetSurveyQuestionByIdAsync(SurveyQuestionByIdModel request);
    Task<bool> GetSurveyTemplateByFilterAsync(SurveyTemplateListFilterModel request);
    Task<bool> GetSurveyTemplateById(SurveyTemplateByIdModel request);

    #endregion
    Task<bool> GetCodeListByIdAsync(CodeListIdModel request);
    //Player Configuration
    Task<bool> GetVIPLevelById(VIPLevelIdRequestModel request);
    Task<bool> GetVIPLevelByFilterAsync(PlayerConfigurationRequestModel request);
    Task<bool> GetAllPlayerConfigurationAsync(BaseModel request);
    Task<bool> GetPlayerConfigurationByIdAsync(PlayerConfigurationIdRequestModel request);
    Task<bool> AddVIPLevelAsync(VipLevelRequestModel request);
    Task<bool> UpdateVIPLevelAsync(VipLevelRequestModel request);
    Task<bool> GetRiskLevelByFilterAsync(PlayerConfigurationRequestModel request);
    Task<bool> GetRiskLevelByIdAsync(RiskLevelIdModel request);
    Task<bool> AddRiskLeveldAsync(RiskLevelModel request);
    Task<bool> UpdateRiskLeveldAsync(RiskLevelModel request);
    //Message
    Task<bool> GetMessageTypeListAsync(MessageTypeListFilterModel request);
    Task<bool> AddMessageListAsync(CodeListMessageTypeModel request);
    Task<bool> GetMesssageTypeByIdAsync(MessageTypeIdModel request);
    Task<bool> GetMessageStatusListAsync(MessageStatusListFilterModel request);
    Task<bool> AddMessageStatusListAsync(MessageStatusRequestModel request);
    Task<bool> GetMesssageStatusByIdAsync(MessageStatusIdModel request);
    Task<bool> GetMessageResponseListAsync(MessageResponseListFilterModel request);
    Task<bool> AddMessageResponseListAsync(MessageResponseRequestModel request);
    Task<bool> GetMesssageResponseByIdAsync(MessageResponseIdModel request);
    //Feedback
    Task<bool> GetFeedbackTypeListAsync(FeedbackTypeListFilterModel request);
    Task<bool> AddFeedbackTypeListAsync(AddFeedbackTypeModel request);
    Task<bool> GetFeedbackTypeByIdAsync(FeedbackTypeIdModel request);
    Task<bool> GetFeedbackCategoryListAsync(FeedbakCategoryListFilterModel request);
    Task<bool> AddFeedbackCategoryListAsync(AddFeedbackCategoryModel request);
    Task<bool> GetFeedbackCategoryByIdAsync(FeedbackCategoryIdModel request);
    Task<bool> GetFeedbackAnswerListAsync(FeedbackAnswerListFilterModel request);
    Task<bool> AddFeedbackAnswerListAsync(AddFeedbackAnswerModel request);
    Task<bool> GetFeedbackAnswerByIdAsync(FeedbackAnswerIdModel request);
    Task<bool> GetSegmentationByFilterAsync(SegmentationRequestModel request);
    Task<bool> GetCampaignPlayerListByFilterAsync(CampaignPlayerFilterRequestModel request);
    Task<bool> SaveSegmentAsync(SegmentationModel request);
    Task<bool> UpsertSegmentAsync(SegmentationModel request);
    Task<bool> TestSegmentAsync(SegmentationTestModel request);
    Task<bool> ToStaticSegmentAsync(SegmentationToStaticModel request);
    Task<bool> GetSegmentDistributionByFilterAsync(SegmentDistributionByFilterRequestModel request);
    Task<bool> GetPlayerConfigCountryAsync(PlayerConfigurationRequestModel request);
    //Campaign Journey
    Task<bool> GetJourneyGridAsync(JourneyGridRequestModel request);
    Task<bool> GetJourneyDetailsAsync(JourneyDetailsRequestModel request);
    Task<bool> SaveJourneyAsync(JourneyRequestModel request);
    //Campaign Management
    Task<bool> GetCampaignListAsync(CampaignListRequestModel request);
    Task<bool> SaveCampaignAsync(CampaignModel request);
    Task<bool> GetCampaignByIdAsync(CampaignIdModel request);
    //Campaign Goal Setting
    Task<bool> GetCampaignGoalSettingByFilterAsync(CampaignGoalSettingByFilterRequestModel request);
    Task<bool> GetCampaignGoalSettingByIdAsync(CampaignGoalSettingIdRequestModel request);
    Task<bool> AddCampaignGoalSettingAsync(CampaignGoalSettingRequestModel request);
    Task<bool> UpsertCampaignGoalSettingAsync(CampaignGoalSettingRequestModel request);
    Task<bool> GetCampaignGoalSettingByIdDetailsAsync(CampaignGoalSettingIdRequestModel request);
    
    #region CaseCommunication
    Task<bool> AddCaseCommunicationAsync(AddCaseCommunicationRequest request);
    Task<bool> GetCaseInformationbyIdAsync(CaseInformationRequest request);
    Task<bool> GetCommunicationByIdAsync(CommunicationByIdRequest request);
    Task<bool> GetCommunicationListAsync(CommunicationListRequest request);
    Task<bool> ChangeCaseStatusAsync(ChangeCaseStatusRequest request);
    Task<bool> GetCommunicationSurveyAsync(CommunicationSurveyRequest request);
    Task<bool> GetCommunicationFeedbackListAsync(CommunicationFeedbackListRequest request);
    Task<bool> UpdateCaseInformationAsync(UpdateCaseInformationRequest request);
    Task<bool> GetCaseCampaignByIdAsync(CaseCampaignByIdRequest request);
    Task<bool> GetCaseContributorByIdAsync(CaseContributorListRequest request);

    #endregion
    //Call List Validation
    Task<bool> UpsertAgentValidationAsync(List<AgentValidationRequestModel> request);
    Task<bool> UpsertLeaderValidationAsync(List<LeaderValidationsRequestModel> request);
    Task<bool> UpsertCallEvaluationAsync(CallEvaluationRequestModel request);
    Task<bool> DeleteCallEvaluationAsync(DeleteCallEvaluationRequestModel request);
    Task<bool> UpsertLeaderJustificationAsync(List<LeaderJustificationRequestModel> request);
    //Agent Monitoring
    Task<bool> UpdateCampaignAgentStatusAsync(AgentStatusRequestModel request);
    Task<bool> UpsertDailyReportAsync(List<DailyReportRequestModel> request);
    Task<bool> DeleteDailyReportByIdAsync(List<DeleteDailyReportRequestModel> request);
    //Campaign Performance
    Task<bool> GetCampaignPerformanceListAsync(CampaignPerformanceRequestModel request);
    // User Grid Custom Display
    Task<bool> UpsertUserGridCustomDisplayAsync(UserGridCustomDisplayModel request);
    #region ManageThreshold
    Task<bool> SaveManageThresholdAsync(SaveManageThresholdRequest request);
    #endregion
    //Player Contact Logs
    Task<bool> GetViewContactLogListAsync(ContactLogListRequestModel request);
    Task<bool> GetViewContactLogTeamListAsync(ContactLogListRequestModel request);
    Task<bool> GetViewContactLogUserListAsync(ContactLogListRequestModel request);
    //Code List Player Configuration
    Task<bool> GetPlayerConfigLanguageAsync(PlayerConfigurationRequestModel request);
    Task<bool> GetPlayerConfigPortalAsync(PlayerConfigurationRequestModel request);
    Task<bool> GetPlayerConfigPlayerStatusAsync(PlayerConfigurationRequestModel request);
    Task<bool> SavePlayerConfigCodeDetailsAsync(PlayerConfigCodeListDetailsRequestModel request);
    
    //Campaign Dashboard
    Task<bool> GetCampaignSurveyAndFeedbackReportAsync(CampaignSurveyAndFeedbackReportRequestModel request);
    Task<bool> GetPaymentGroupByFilterAsync(PlayerConfigurationRequestModel request);
    Task<bool> GetMarketingChannelByFilterAsync(PlayerConfigurationRequestModel request);
    Task<bool> GetCurrencyByFilterAsync(PlayerConfigurationRequestModel request);
    Task<bool> GetPaymentMethodByFilterAsync(PaymentMethodRequestModel request);

    //Campang Retention 
    Task<bool> ValidateImportRetentionPlayerAsync(CampaignImportPlayerRequestModel request);
    Task<bool> ProcessCampaignImportPlayersAsync(CampaignImportPlayerRequestModel request);
    Task<bool> GetCampaignUploadPlayerListAsync(UploadPlayerFilterModel request);
    Task<bool> RemoveCampaignImportPlayersAsync(CampaignImportPlayerModel request);
    Task<bool> GetCampaignCustomEventSettingByFilterAsync(CampaignCustomEventSettingRequestModel request);
    Task<bool> AddCampaignCustomEventSettingAsync(CampaignCustomEventSettingModel request);

    //RemProfile
    Task<bool> GetRemProfileByFilterAsync(RemProfileFilterRequestModel request);
    Task<bool> GetRemProfileByIdAsync(RemProfileFilterRequestModel request);
    Task<bool> UpSertRemProfileAsync(RemProfileDetailsRequestModel request);
    //RemDistribution
    Task<bool> GetRemDistributionByFilterAsync(RemDistributionFilterRequestModel request);
    //RemSetting
    Task<bool> GetScheduleTemplateSettingListAsync(ScheduleTemplateListRequestModel request);
    Task<bool> GetScheduleTemplateSettingByIdAsync(ScheduleTemplateByIdRequestModel request);
    Task<bool> GetScheduleTemplateLanguageSettingListAsync(ScheduleTemplateLanguageRequestModel request);
    Task<bool> SaveScheduleTemplateSettingAsync(SaveScheduleTemplateRequestModel request);
    Task<bool> GetAutoDistributionSettingConfigsListByFilterAsync(AutoDistributionSettingFilterRequestModel request);
    Task<bool> GetAutoDistributionSettingAgentsListByFilterAsync(AutoDistributionSettingFilterRequestModel request);
    Task<bool> SaveAutoDistributionConfigurationAsync(AutoDistributionConfigurationRequestModel request);
    Task<bool> GetAutoDistributionConfigurationDetailsByIdAsync(AutoDistributionConfigurationByIdRequestModel request);
    Task<bool> GetAutoDistributionConfigurationListByAgentIdAsync(AutoDistributionConfigurationListByAgentIdRequestModel request);
    //RemHistory
    Task<bool> GetRemHistoryByFilterAsync(RemHistoryFilterRequestModel request);

    Task<bool> GetPostChatSurveyByFilterAsync(PostChatSurveyFilterRequestModel request);
    Task<bool> GetSkillByFilterAsync(SkillFilterRequestModel request);
    Task<bool> UpsertPostChatSurveyAsync(PostChatSurveyRequestModel request);
    Task<bool> GetPostChatSurveyByIdAsync(PostChatSurveyIdRequestModel request);
    Task<bool> UpsertSkillAsync(SkillRequestModel request);

    //ASW
    Task<bool> UpserSertAgentSurveyAsync(AgentSurveyRequest request, string platform);

    Task<bool> GetAppConfigSettingByFilterAsync(AppConfigSettingFilterRequestModel request);
    Task<bool> UpsertAppConfigSettingAsync(AppConfigSettingRequestModel request);

    //CASE COMMUNICATION MANAGEMENT
    Task<bool> GetCaseCommunicationListAsync(CaseCommunicationFilterRequest request); 
    Task<bool> UpSertCustomerServiceCaseCommunicationAsync(AddCustomerServiceCaseCommunicationRequest request);
    Task<bool> ChangeCustomerServiceCaseCommStatusAsync(ChangeStatusCustomerServiceRequest request);
    Task<bool> UpsertChatSurveyActionAndSummaryAsync(ChatSurveySummaryAndActionRequestModel request);
    Task<bool> GetChatSurveyByIdAsync(ChatSurveyByIdRequestModel request);
    Task<bool> GetCaseManagementPCSQuestionsByFilterAsync(PCSQuestionaireListByFilterRequestModel request);
    Task<bool> GetCaseManagementPCSCommunicationByFilterAsync(CaseManagementPCSCommunicationByFilterRequestModel request);

    #region CommunicationReview
    Task<bool> SaveCommunicationReviewAsync(SaveCommunicationReviewRequestModel request);
    Task<bool> GetCommunicationHistoryByReviewIdAsync(CommunicationReviewRequestModel request);
    Task<bool> GetCommunicationReviewHistoryListAsync(CommunicationReviewHistoryRequestModel request);
    #endregion

    #region EngagementHub
    Task<bool> GetBotAutoReplyListByFilterAsync(BotAutoReplyFilterRequestModel request);
    Task<bool> GetBotByIdAsync(BotFilterRequestModel request);
    Task<bool> UpSertBotDetailsAsync(BotDetailsRequestModel request);
    Task<bool> UpSertBotDetailsAutoReplyAsync(BotDetailsAutoReplyRequestModel request);
    Task<bool> GetBotDetailListResultByFilterAsync(BotDetailFilterRequestModel request);
    Task<bool> GetBroadcastListByFilter(BroadcastFilterRequestModel request);
    Task<bool> UpsertBroadcastConfigurationAsync(BroadcastConfigurationRequest request);
    Task<bool> GetBroadcastConfigurationByIdAsync(GetBroadcastConfigurationByIdRequest request);
    Task<bool> GetBroadcastConfigurationRecipientsStatusProgressById(GetBroadcastConfigurationByIdRequest request);
    
    #endregion

    #region Ticket Management
    Task<bool> GetTicketFieldMappingByTicketTypeAsync(FieldMappingRequestModel request);
    Task<bool> SavePaymentMethodAsync(SavePaymentMethodRequestModel request);
    Task<bool> SaveTicketFieldsAsync(SaveTicketFieldsRequestModel request);
    Task<bool> SaveTicketDetailsAsync(SaveTicketDetailsRequestModel request);
    Task<bool> DeleteTicketAttachmentByIdAsync(DeleteAttachmentRequestModel request);
    Task<bool> GetTicketCommentByTicketCommentId(GetTicketCommentRequestModel request);
    Task<bool> UpsertTicketComment([FromBody] UpsertTicketCommentRequestModel request);
    Task<bool> DeleteTicketCommentByTicketCommentId([FromBody] DeleteTicketCommentRequestModel request);
    Task<bool> UpsertPopupTicketDetailsAsync(UpsertPopupTicketDetailsRequestModel request);
    Task<bool> GetTicketStatusPopupMappingAsync(GetTicketStatusPopupMappingRequestModel request);
    Task<bool> GetSearchTicketByFilters(SearchTicketFilterRequestModel request);
    Task<bool> UpsertSearchTicketFilter(UpsertSearchTicketFilterRequestModel request);
    Task<bool> GetTicketHistory(HistoryCollaboratorGridRequestModel request);
    Task<bool> GetCollaboratorGridList(HistoryCollaboratorGridRequestModel request);
    #endregion

    #region Search Leads
    Task<bool> GetLeadsByFilterAsync(SearchLeadsRequestModel request);
    #endregion

    #region Staff Performance
    Task<bool> GetCommunicationReviewPeriodsByFilterAsync(ReviewPeriodRequestModel request);
    #endregion

    #region Administration
    Task<bool> GetEventSubscriptionAsync(EventSubscriptionFilterRequestModel request);
    Task<bool> UpdateEventSubscriptionAsync(EventSubscriptionRequestModel request); 
    #endregion
}

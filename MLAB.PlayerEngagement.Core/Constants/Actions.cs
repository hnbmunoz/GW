namespace MLAB.PlayerEngagement.Core.Constants;

public enum Actions
{
    GetRoleListAsync,
    AddRoleAsync,
    UpdateRoleAsync,
    GetRoleByIdAsync,
    GetCloneRoleAsync,
    GetAllRoleAsync,
    GetTeamListAsync,
    AddTeamAsync,
    UpdateTeamAsync,
    GetTeamByIdAsync,
    GetUserListAsync,
    GetUserByIdAsync,
    GetCommunicationProviderAccountListbyIdAsync,
    CreateNewPasswordAsync,
    ResetPasswordAsync,
    AddUserAsync,
    UpdateUserAsync,
    LockUserAsync,
    ImportPlayersAsync,
    ValidateImportPlayersAsync,
    GetAllCodeListAsync,
    AddCodeListAsync,
    AddCodeListTypeAsync,
    GetAllCodeListTypeAsync,
    GetAllFieldTypeAsync,
    AddTopicAsync,
    UpdateTopicOrderAsync,
    UpdateSubtopicOrderAsync,
    UpdateTopicStatusAsync,
    UpSertTopicAsync,
    GetTopicByIdAsync,
    GetTopicOrderAsync,
    GetTopicListByFilterAsync,
    GetSubtopicByFilterAsync,
    SubmitSubtopicAsync,
    UpsertSubtopicAsync,
    GetSubtopicByIdAsync,
    GetCodeListByIdAsync,
    //Player Configuration
    GetVIPLevelByIdAsync,
    GetVIPLevelByFilterAsync,
    GetAllPlayerConfigurationAsync,
    GetPlayerConfigurationByIdAsync,
    AddVIPLevelAsync,
    GetRiskLevelByFilterAsync,
    GetRiskLevelByIdAsync,
    AddRiskLeveldAsync,
    UpdateRiskLeveldAsync,
    UpdateVIPLevelAsync,
    SavePaymentMethodAsync,
    SaveTicketFieldsAsync,
    //Message
    GetMessageTypeListAsync,
    AddMessageListAsync,
    ValidateMessageTypeAsync,
    GetMesssageTypeByIdAsync,
    GetMessageStatusListAsync,
    AddMessageStatusListAsync,
    ValidateMessageStatusAsync,
    GetMesssageStatusByIdAsync,
    GetMessageResponseListAsync,
    AddMessageResponseListAsync,
    ValidateMessageResponseAsync,
    GetMesssageResponseByIdAsync,
    //Feedback
    GetFeedbackTypeListAsync,
    AddFeedbackTypeListAsync,
    GetFeedbackTypeByIdAsync,
    GetFeedbackCategoryListAsync,
    AddFeedbackCategoryListAsync,
    GetFeedbackCategoryByIdAsync,
    GetFeedbackAnswerListAsync,
    AddFeedbackAnswerListAsync,
    GetFeedbackAnswerByIdAsync,
    //Survey
    SaveSurveyQuestionAsync,
    SaveSurveyTemplateAsync,
    GetSurveyQuestionsByFilterAsync,
    GetSurveyQuestionByIdAsync,
    GetSurveyTemplateByFilterAsync,
    GetSurveyTemplateByIdAsync,
    //Segmentation
    GetSegmentationByFilterAsync,
    //Agent Workspace
    GetCampaignPlayerListByFilterAsync,
    SaveSegmentationAsync,
    UpsertSegmentAsync,
    TestSegmentationAsync,
    //Campaign
    GetCampaignListAsync,
    SaveCampaignAsync,
    GetCampaignByIdAsync,
    GetCampaignGoalSettingByFilterAsync,
    GetCampaignGoalSettingByIdAsync,
    AddCampaignGoalSettingAsync,
    ToStaticSegmentAsync,
    GetSegmentDistributionByFilterAsync,
    UpsertCampaignGoalSettingAsync,
    GetCampaignGoalSettingByIdDetailsAsync,
    //Case Communication
    AddCaseCommunicationAsync,
    GetCaseInformationbyIdAsync,
    //Campaign Journey
    GetJourneyGridAsync,
    GetJourneyDetailsAsync,
    GetJourneyCampaignDetailsAsync,
    SaveJourneyAsync,
    //Campaign Setting
    GetAutoTaggingSelectionAysnc,
    GetPointIncentiveDetailsByIdAsync,
    GetAutoTaggingDetailsByIdAsync,
    GetCampaignSettingListAsync,
    AddAutoTaggingListAsync,
    AddPointIncentiveSettingAsync,
    GetCommunicationByIdAsync,
    GetCommunicationListAsync,
    ChangeCaseStatusAsync,
    GetCommunicationSurveyAsync,
    GetCommunicationFeedbackListAsync,
    UpdateCaseInformationAsync,
    //Call List Validation
    UpsertAgentValidationAsync,
    GetCaseCampaignByIdAsync,
    GetCaseContributorByIdAsync,
    UpsertLeaderValidationAsync, 
    UpsertCallEvaluationAsync,
    DeleteCallEvaluationAsync, 
    UpsertLeaderJustificationAsync,
    //Agent Monitoring
    UpdateCampaignAgentStatusAsync,
    UpsertDailyReportAsync,
    DeleteDailyReportByIdAsync,
    // Campaign Performance
    GetCampaignPerformanceListAsync,
    //Code List Player Configuration
    GetPlayerConfigLanguageAsync,
    GetPlayerConfigPlayerStatusAsync,
    GetPlayerConfigPortalAsync,
    GetPlayerConfigCountryAsync,
    //User Grid Custom Display
    UpsertUserGridCustomDisplayAsync,
    // Manage Threshold
    SaveManageThresholdAsync,
    //Player Contact Logs
    GetViewContactLogUserListAsync,
    GetViewContactLogTeamListAsync,
    GetViewContactLogListAsync,
    SavePlayerConfigCodeDetailsAsync,
    //Campaign Dashboard
    GetCampaignSurveyAndFeedbackReportAsync,
    GetPaymentGroupByFilterAsync,
    GetMarketingChannelByFilterAsync,
    GetCurrencyByFilterAsync,
    GetPaymentMethodByFilterAsync,
    //Campaign Retention
    ValidateImportRetentionPlayerAsync,
    ProcessCampaignImportPlayersAsync,
    GetCampaignUploadPlayerListAsync,
    RemoveCampaignImportPlayersAsync,
    //Campaign Custom Event Setting
    GetCampaignCustomEventSettingByFilterAsync,
    AddCampaignCustomEventSettingAsync,
    //RemProfile
    GetRemProfileByFilterAsync,
    GetRemProfileByIdAsync,
    UpSertRemProfileAsync,

    //RemDistribution
    GetRemDistributionByFilterAsync,
    //RemSetting
    GetScheduleTemplateSettingListAsync,
    GetScheduleTemplateSettingByIdAsync,
    GetScheduleTemplateLanguageSettingListAsync,
    SaveScheduleTemplateSettingAsync,
    GetAutoDistributionSettingConfigsListByFilterAsync,
    GetAutoDistributionSettingAgentsListByFilterAsync,
    SaveAutoDistributionConfigurationAsync,
    GetAutoDistributionConfigurationDetailsByIdAsync,
    GetAutoDistributionConfigurationListByAgentIdAsync,
    //RemHistory
    GetRemHistoryByFilterAsync,
    UpsertPostChatSurveyAsync,
    GetPostChatSurveyByFilterAsync,
    GetSkillByFilterAsync,
    GetPostChatSurveyByIdAsync,
    UpsertSkillAsync,
    //ASW
    UpserSertAgentSurveyAsync,
    GetAppConfigSettingByFilterAsync,
    UpsertAppConfigSettingAsync,
    //Case Communication management
    GetCaseCommunicationListAsync,
    UpSertCustomerServiceCaseCommunicationAsync,
    ChangeCustomerServiceCaseCommStatusAsync,
    UpsertChatSurveyActionAndSummaryAsync,
    GetChatSurveyByIdAsync,
    GetCaseManagementPCSQuestionsByFilterAsync,
    GetCaseManagementPCSCommunicationByFilterAsync,

    #region Communication Review
    SaveCommunicationReviewAsync,
    GetCommunicationHistoryByReviewIdAsync,
    GetCommunicationReviewHistoryListAsync,
    #endregion
    #region Engagement Hub
    GetBotAutoReplyByFilterAsync,
    GetBotDetailByIdAsync,
    UpSertBotDetailsAsync,
    UpSertBotDetailAutoReplyAsync,
    GetBotDetailListResultByFilterAsync,
    GetBroadcastListByFilter,
    UpsertBroadcastConfiguration,
    GetBroadcastConfigurationById,
    GetBroadcastConfigurationRecipientsStatusProgressById,
    #endregion
    #region Ticket Management
    GetTicketFieldMappingByTicketTypeAsync,
    SaveTicketDetailsAsync,
    DeleteTicketAttachmentByIdAsync,
    GetTicketCommentByTicketCommentId,
    UpsertTicketComment,
    DeleteTicketComment,
    UpsertPopupTicketDetailsAsync,
    GetTicketStatusPopupMappingAsync,
    GetSearchTicketByFilters,
    UpsertSearchTicketFilter,
    GetTicketHistory,
    AddUserAsCollaborator,
    DeleteUserAsCollaborator,
    GetCollaboratorGridList,
    #endregion

    #region Search Leads
    GetLeadsByFilterAsync,
    #endregion

    #region StaffPerformance
    //ReviewPeriod
    GetCommunicationReviewPeriodsByFilterAsync,
    #endregion
    #region Administration
    GetEventSubscriptionAsync,
    UpdateEventSubscriptionAsync
    #endregion
}

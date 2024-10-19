namespace MLAB.PlayerEngagement.Core.Constants;

public static class StoredProcedures
{
    public const string Usp_GetCurrencies = @"Usp_GetCurrencies";
    public const string Usp_GetOperators = @"Usp_GetOperators";
    public const string Usp_InsertQueueRequest = @"Usp_InsertQueueRequest";
    public const string Usp_UpdateQueueRequest = @"Usp_UpdateQueueRequest";
    public const string Usp_GetAllCaseType = @"Usp_CaseType";
    public const string Usp_GetAllMessageType = @"USP_GetMessageTypeList";
    public const string Usp_GetCurrency = @"Usp_GetCurrency";
    public const string USP_GetRemAgentByAccess = @"USP_GetRemAgentByAccess";
    public const string Usp_GetCountry = @"Usp_GetCountry";

    // Operators
    public const string Usp_AddOperator = @"Usp_AddOperator";
    public const string Usp_GetBrand = @"Usp_GetBrand";
    public const string Usp_GetAllVIPLevel = @"Usp_GetAllVIPLevel";
    public const string Usp_GetAllCurrency = @"Usp_GetCurrency";
    public const string Usp_GetOperatorBrandCurrencyByOperatorId = @"Usp_GetOperatorBrandCurrencyByOperatorId";
    public const string Usp_GetOperatorById = @"Usp_GetOperatorById";
    public const string Usp_GetOperatorListByFilter = @"Usp_GetOperatorListByFilter";
    public const string Usp_UpdateOperator = @"Usp_UpdateOperator";
    public const string Usp_GetBrandExistingList = @"Usp_GetBrandExistingList";
    public const string Usp_GetOperator = @"Usp_GetOperator";

    //users
    public const string USP_VerifyAccount = @"USP_VerifyAccount";
    public const string USP_GetUserModulePermission = @"USP_GetUserModulePermission";
    public const string USP_UpdateUserStatus = @"USP_UpdateUserStatus";
    public const string USP_UpdateUserStatusById = @"Usp_UpdateUserStatusById";
    public const string Usp_GetTeam = @"Usp_GetTeam";
    public const string Usp_GetRole = @"Usp_GetRole";
    public const string USP_GetUserLookups = @"USP_GetUserLookups";
    public const string Usp_GetUserByFilter = @"Usp_GetUserByFilter";
    public const string USP_GetUsersWithLivePersonCommProvider = @"USP_GetUsersWithLivePersonCommProvider";
    public const string USP_GetTeamsByUserId = @"USP_GetTeamsByUserId";
    public const string Usp_ValidateUserProviderName = @"Usp_ValidateUserProviderName";
    public const string Usp_ValidateCommunicationProvider = @"USP_ValidateCommunicationProvider";
    public const string USP_GetDataRestrictionDetailsPerUserId = @"USP_GetDataRestrictionDetailsPerUserId";
    public const string USP_SetUserIdleStatus = @"USP_SetUserIdleStatus";

    //System
    public const string USP_GetAllFieldType = @"USP_GetAllFieldType";
    public const string USP_GetAllTopic = @"USP_GetTopic";
    public const string Usp_GetTopicByName = @"Usp_GetTopicByName";
    public const string USP_GetSubtopicId = @"USP_GetSubtopicId";
    public const string USP_GetAllCodeList = @"USP_GetAllCodeList";   
    public const string USP_GetCodeListId = @"USP_GetCodeListId";
    public const string USP_GetCodeListTypeId = @"USP_GetCodeListTypeId";
    public const string USP_GetAllCodeTypeList = @"USP_GetAllCodeListType";
    public const string USP_GetVIPLevelId = @"USP_GetVIPLevelId";
    public const string USP_ValidateSubtopicName = @"USP_ValidateSubtopicName";
    public const string USP_UpdateSubtopicStatus = @"USP_UpdateSubtopicStatus";
    public const string USP_UpdateTopicStatus = @"USP_UpdateTopicStatus";
    public const string USP_GetTopicOptionsByCode = @"USP_GetTopicOptionsByCode";
    public const string USP_GetSubtopicOptionsById = @"USP_GetSubtopicOptionsById";
    public const string USP_GetStaffPermormanceSettingList = @"USP_GetStaffPermormanceSettingList";

    //System-Survey
    public const string USP_ValidateSurveyQuestion = @"USP_ValidateSurveyQuestion";
    public const string USP_ValidateSurveyQuestionAnswers = @"USP_ValidateSurveyQuestionAnswers";
    public const string USP_ValidateSurveyTemplate = @"USP_ValidateSurveyTemplate";
    public const string USP_ValidateSurveyTemplateQuestions = @"USP_ValidateSurveyTemplateQuestions";
    public const string USP_DeactivateSurveyQuestion = @"USP_DeactivateSurveyQuestion";
    public const string USP_DeactivateSurveyTemplate = @"USP_DeactivateSurveyTemplate";
    public const string USP_GetAllSurveyTemplate = @"USP_GetSurveyTemplateByFilter";

    //Message
    public const string USP_ValidateMessageType = @"USP_ValidateMessageType";
    public const string USP_ValidateMessageResponse = @"USP_ValidateMessageResponse";
    public const string USP_ValidateMessageStatus = @"USP_ValidateMessageStatus";

    //Feedback
    public const string USP_ValidateFeedbackType = @"USP_ValidateFeedbackType";
    public const string USP_ValidateFeedbackCategory = @"USP_ValidateFeedbackCategory";
    public const string USP_ValidateFeedbackAnswer = @"USP_ValidateFeedbackAnswer";

    //System-Lookups
    public const string USP_GetSystemLookups = @"USP_GetSystemLookups";
    public const string USP_GetMasterReferenceName = @"USP_GetMasterReferenceName";
    public const string USP_GetMasterReferenceId = @"USP_GetMasterReferenceId";
    public const string USP_GetPlayerConfigCodeListDetails = @"USP_GetPlayerConfigCodeListDetails";

    //Players
    public const string USP_GetCampaign = @"USP_GetCampaign";
    public const string USP_GetPlayerList = @"USP_GetPlayerList";
    public const string USP_GetPlayerById = @"USP_GetPlayerById";
    public const string USP_AddPlayerContact = @"USP_AddPlayerContact";
    public const string USP_GetPlayerCaseList = @"USP_GetPlayerCaseList";
    public const string USP_GetPlayerSensitiveData = @"USP_GetPlayerSensitiveData";


    // Options
    public const string USP_GetCaseTypeOptionList = @"USP_GetCaseTypeOptionList";
    public const string USP_GetTopicOptionList = @"USP_GetTopicOptionList";
    public const string USP_GetTopicByBrand = @"USP_GetTopicByBrand";
    public const string USP_GetSubtopicOptionById = @"USP_GetSubtopicOptionById";
    public const string USP_GetMessageTypeOptionList = @"USP_GetMessageTypeOptionList";
    public const string USP_GetMessageStatusOptionById = @"USP_GetMessageStatusOptionById";
    public const string USP_GetMessageResponseOptionById = @"USP_GetMessageResponseOptionById";
    public const string USP_GetMasterReferenceList = @"USP_GetMasterReferenceList";
    public const string USP_GetTopicOptions = @"USP_GetTopicOptions";


    //Agent Workspace
    public const string USP_SaveCallListNote = @"USP_SaveCallListNote";
    public const string USP_GetCallListNote = @"USP_GetCallListNote";
    public const string USP_TagPlayer = @"USP_TagPlayer";
    public const string USP_DiscardPlayer = @"USP_DiscardPlayer";
    public const string USP_GetCampaignPlayerTaggingInfoById = @"USP_GetCampaignPlayerTaggingInfoById";
    public const string USP_GetCampaignPlayerListByFilter = @"USP_GetCampaignPlayerListByFilter";
    public const string USP_ValidateTaggingById = @"USP_ValidateTaggingById";
    public const string USP_GetAppConfigSetting = @"USP_GetAppConfigSetting";
    public const string USP_GetCommunicationHistoryByFilter = @"USP_GetCommunicationHistoryByFilter";
    
    //Campaign Setting - auto tagging, point incentive
    public const string USP_GetSegment = @"USP_GetSegment";
    public const string USP_GetSegmentWithType = @"USP_GetSegmentWithType";
    public const string USP_GetSegmentationById = @"USP_GetSegmentationById";
    public const string USP_GetCampaignGoalSettingById = @"USP_GetCampaignGoalSettingById";
    public const string USP_GetCampaignGoalSetting = @"USP_GetCampaignGoalSetting";
    public const string USP_GetLeaderValidation = @"USP_GetLeaderValidation";
    public const string USP_GetGoalParameterAmount = @"USP_GetGoalParameterAmount";
    public const string USP_GetGoalParameterCount = @"USP_GetGoalParameterCount";
    public const string USP_GetCampaignSettingList = @"USP_GetCampaignSettingList";
    public const string USP_GetPointIncentiveDetails = @"USP_GetPointIncentiveDetailsById";
    public const string USP_GetAutoTaggingDetailsById = @"USP_GetAutoTaggingDetailsById";
    public const string USP_GetPointIncentiveDetailsById = @"USP_GetPointIncentiveDetailsById";
    public const string USP_GetCampaignGoalSettingName = @"USP_GetCampaignGoalSettingName";
    public const string USP_GetGoalParameterValueSetting = @"USP_GetGoalParameterValueSetting";
    public const string USP_GetGoalParameterPointSetting = @"USP_GetGoalParameterPointSetting";
    public const string USP_GetAutoTaggingSetting = @"USP_GetAutoTaggingSetting";
    public const string USP_GetCampaignSettingByName = @"USP_GetCampaignSettingByName";
    public const string USP_GetCampaignLookupByFilter = @"USP_GetCampaignLookupByFilter";
    public const string USP_GetCampaignConfigurationSegmentById = @"USP_GetCampaignConfigurationSegmentById";

    //Segmentation
    public const string USP_DeactivateSegment = @"USP_DeactivateSegment";
    public const string USP_GetRelationalOperators = @"USP_GetRelationalOperators";
    public const string AT_USP_GetSegment = @"USP_GetSegment";
    public const string USP_GetUserByModule = @"Usp_GetUserByModule";
    public const string USP_TestSegment = @"USP_TestSegment";
    public const string USP_GetSegmentConditionFields = @"USP_GetSegmentConditionFields";
    public const string USP_GetSegmentById = @"USP_GetSegmentById";
    public const string USP_ValidateSegment = @"USP_ValidateSegment";
    public const string USP_GetFeedbackTypeOptionList = @"USP_GetFeedbackTypeOptionList";
    public const string USP_GetFeedbackCategoryOptionById = @"USP_GetFeedbackCategoryOptionById";
    public const string USP_GetFeedbackAnswerOptionById = @"USP_GetFeedbackAnswerOptionById";
    public const string USP_GetStaticSegment = @"USP_GetStaticSegment";
    public const string USP_ValidateInFileSegmentPlayers = @"USP_ValidateInFileSegmentPlayers";
    public const string USP_GetMessageStatusByCaseTypeId = @"USP_GetMessageStatusByCaseTypeId";
    public const string USP_GetMessageResponseByMultipleId = @"USP_GetMessageResponseByMultipleId";


    //Call List Validations
    public const string Usp_GetCallListValidationFilter = @"Usp_GetCallListValidationFilter";
    public const string Usp_GetCallValidationList = @"Usp_GetCallValidationList";
    public const string Usp_GetLeaderJustificationList = @"Usp_GetLeaderJustificationList";

    //Agent Monitoring
    public const string Usp_GetAutoTaggingNameList = @"Usp_GetAutoTaggingNameList";
    public const string Usp_GetCampaignAgentList = @"Usp_GetCampaignAgentList";
    //Case Communication
    public const string USP_GetCommunicationSurveyQuestionAnswers = @"USP_GetCommunicationSurveyQuestionAnswers";
    public const string USP_ValidateCaseCampaignId = @"USP_ValidateCaseCampaignId";

    //Campaign
    public const string USP_GetAllCampaigns = @"USP_GetAllCampaigns";
    public const string USP_GetCampaignAgents = @"USP_GetCampaignAgents";
    public const string USP_GetMessageStatusMessageResponseList = @"USP_GetMessageStatusMessageResponseList";
    public const string USP_GetAgentsForTagging = @"USP_GetAgentsForTagging";
    public const string USP_GetCampaignMessageStatusResponseList = @"USP_GetCampaignMessageStatusResponseList";
    public const string USP_GetCampaignPeriodBySourceId = @"USP_GetCampaignPeriodBySourceId";
    //Campaign Goal Setting
    public const string USP_CheckCampaignGoalSettingByNameExist = @"USP_CheckCampaignGoalSettingByNameExist";

    //Administrator
    public const string USP_GetQueueRqstByFiltr = "USP_GetQueueRequestByFilter";
    public const string USP_GetQueueHstryByFiltr = "USP_GetQueueHistoryByFilter";
    public const string USP_GetDstinctQueueStatus = "USP_GetDistinctQueueStatus";
    public const string USP_GetDstinctQueueActions = "USP_GetDistinctQueueActions";
    public const string USP_DeleteQueueByCreatedDateRange = "USP_DeleteQueueByCreatedDateRange";

    //Campaign Performance
    public const string Usp_GetCampaignActiveEnded = "Usp_GetCampaignActiveEnded";

    //User Grid Display
    public const string USP_LoadUserGridCustomDisplay = @"USP_LoadUserGridCustomDisplay";

    //CampaignDashboard
    public const string USP_GetCampaignSurveyAndFeedbackReport = "USP_ReportSurveyAndFeedback";

    //Manage Threshold
    public const string USP_GetManageThresholds = "USP_GetManageThresholds";

    //Player Log
    public const string USP_ViewContactLogs = "USP_ViewContactLogs";

    //Player Configuration - Code List
    public const string USP_CheckExistingIDNameCodeList = "USP_CheckExistingIDNameCodeList";
    public const string USP_GetLanguageDetails = "USP_GetLanguageDetails";
    public const string USP_GetTicketFields = "USP_GetTicketFields";
    public const string USP_ValidatePaymentMethodCommunicationProvider = "USP_ValidatePaymentMethodCommunicationProvider";
    public const string USP_ValidatePaymentMethodName = "USP_ValidatePaymentMethodName";

    //Admin Permission and access
    public const string USP_VerifyAdminAccountAccess = "USP_VerifyAdminAccountAccess";
    public const string USP_GetUserOptions = @"USP_GetUserOptions";
    public const string USP_ValidateUserChatByProvider = @"USP_ValidateUserChatByProvider";
    public const string Usp_GetCommunicationProviderAccountListByUserId = @"Usp_GetCommunicationProviderAccountListByUserId";

    //Campaign Retention
    public const string USP_SearchActivity = "Usp_SearchActivity";
    public const string USP_GetRetentionCampaignPlayerList = @"USP_GetRetentionCampaignPlayerList";
    public const string USP_GetPlayerDepositAttempts = "USP_GetPlayerDepositAttempts";
    public const string USP_GetCampaignCustomEventSettingByFilter = @"USP_GetCampaignCustomEventSettingByFilter";
    public const string USP_GetCampaignCustomEventSettingName = @"USP_GetCampaignCustomEventSettingName";

    //Campaign Journey
    public const string USP_GetCampaignJourney = @"USP_GetCampaignJourney";
    public const string USP_GetJourneyCampaignNames = @"USP_GetJourneyCampaignNames";

    //RemProfile
    public const string USP_UpdateRemProfileStatus = @"USP_UpdateRemProfileStatus";
    public const string USP_UpSertRemProfile = @"USP_UpSertRemProfile";
    public const string USP_ValidateRemProfileIfHasPlayer = @"USP_ValidateRemProfileIfHasPlayer";
    public const string USP_GetRemProfileByFilter = @"USP_GetRemProfileByFilter";

    //RemDistribution
    public const string USP_GetRemDistributionByPlayerId = @"USP_GetRemDistributionByPlayerId";
    public const string USP_DeleteRemDistribution = @"USP_DeleteRemDistribution";
    public const string USP_UpsertRemDistribution = @"USP_UpsertRemDistribution";
    public const string USP_GetRemLookups = @"USP_GetRemLookups";
    public const string USP_ValidateRemProfileName = @"USP_ValidateRemProfileName";
    public const string USP_GetReusableRemProfileDetails = @"USP_GetReusableRemProfileDetails";
    public const string USP_GetRemDistributionByFilter = @"USP_GetRemDistributionByFilter";
    public const string Usp_GetAllVIPLevelByBrand = @"Usp_GetAllVIPLevelByBrand";

    //RemHistory
    public const string USP_GetRemHistoryByFilter = @"USP_GetRemHistoryByFilter";

    //RemSetting
    public const string USP_ValidateTemplateSetting = @"USP_ValidateTemplateSetting";
    public const string USP_GetScheduleTemplateSettingList = @"USP_GetScheduleTemplateSettingList"; 
    public const string Usp_UpdateMaxPlayerCountConfig = @"Usp_UpdateMaxPlayerCountConfig";
    public const string Usp_RemoveDistributionbyVipLevel = @"Usp_RemoveDistributionbyVipLevel";
    public const string Usp_UpdateAutoDistributionSettingPriority = @"Usp_UpdateAutoDistributionSettingPriority";
    public const string Usp_UpdateAutoDistributionConfigurationStatus = @"Usp_UpdateAutoDistributionConfigurationStatus";
    public const string USP_DeleteAutoDistributionConfigurationById = @"USP_DeleteAutoDistributionConfigurationById";
    public const string USP_ValidateAutoDistributionConfigurationName = @"USP_ValidateAutoDistributionConfigurationName";
    public const string USP_GetAutoDistributionConfigurationCount = @"USP_GetAutoDistributionConfigurationCount";
    public const string USP_GetRemovedVipLevels = @"USP_GetRemovedVipLevels";
    public const string Usp_GetRemAutoDistributionSettingConfigsListOrder = @"Usp_GetRemAutoDistributionSettingConfigsListOrder";

    //Shared
    public const string USP_GetAllScheduleTemplateList = @"USP_GetAllScheduleTemplateList";
    public const string USP_GetMessageTypeChannelList = @"USP_GetMessageTypeChannelList";

    //TO BE REMOBVED
    public const string USP_GetScheduleTemplateLanguageSettingList = @"USP_GetScheduleTemplateLanguageSettingList";

    public const string USP_GetSegmentConditionSetByParentId = @"USP_GetSegmentConditionSetByParentId";
    public const string USP_GetSegmentLookups = @"USP_GetSegmentLookups";
    public const string USP_GetCampaignGoalNamesByCampaignId = @"USP_GetCampaignGoalNamesByCampaignId";
    public const string USP_GetVariancesBySegmentId = @"USP_GetVariancesBySegmentId";
    public const string USP_SegmentVarianceDistribution = @"USP_SegmentVarianceDistribution";
    public const string USP_GetSegmentDistributionByFilter = @"USP_GetSegmentDistributionByFilter";
    public const string USP_GetSegmentConditionsBySegmentId = @"USP_GetSegmentConditionsBySegmentId";
    public const string USP_GetSkillsByLicenseId = @"USP_GetSkillsByLicenseId";
    public const string USP_GetPCSLookups = @"USP_GetPCSLookups";
    public const string USP_GetTopicOrder = @"USP_GetTopicOrder";
    public const string USP_TogglePostChatSurvey = @"USP_TogglePostChatSurvey";
    public const string USP_ToggleSkill = @"USP_ToggleSkill";
    public const string USP_ValidateSkill = @"USP_ValidateSkill";
    public const string USP_GetSubtopicOrder = @"USP_GetSubtopicOrder";
    public const string USP_GetPostChatSurveyById = @"USP_GetPostChatSurveyById";
    public const string USP_ValidatePostChatSurveyQuestionID = @"USP_ValidatePostChatSurveyQuestionID";
    public const string USP_GetDateByOptionList = @"USP_GetDateByOptionList";

    //ASW
    public const string USP_ASW_GetLanguageList = @"USP_ASW_GetLanguageList";
    public const string USP_ASW_GetSubtopicnameById = @"USP_ASW_GetSubtopicnameById";
    public const string USP_ASW_ValidatePlayerAndSkillMappingBrand = @"USP_ASW_ValidatePlayerAndSkillMappingBrand";
    public const string USP_ASW_GetTopicNameByCode = @"USP_ASW_GetTopicNameByCode";
    public const string USP_ASW_GetAgentSurveyByConversationId = @"USP_ASW_GetAgentSurveyByConversationId";
    public const string USP_ASW_GetBrandBySkillName = @"USP_ASW_GetBrandBySkillName";
    public const string USP_ASW_GetSkillDetailsBySkillID = @"USP_ASW_GetSkillDetailsBySkillID";
    public const string USP_ASW_GetAllActiveCampaignByUsername = @"USP_ASW_GetAllActiveCampaignByUsername";

    //CaseManagement
    public const string USP_GetCustomerCaseInfo = @"USP_GetCustomerCaseInfo";
    public const string USP_GetCustomerCaseCommInfo = @"USP_GetCustomerCaseCommInfo";
    public const string USP_GetCustomerCaseCommList = @"USP_GetCustomerCaseCommList";
    public const string USP_GetAllCommunicationOwner = "USP_GetAllCommunicationOwner";
    public const string USP_GetCaseAndCommunicationFilter = "USP_GetCaseAndCommunicationFilter";
    public const string USP_ValidatePlayerCaseCommunication = "USP_ValidatePlayerCaseCommunication";
    public const string USP_GetCustomerServiceCaseInformationById = "USP_GetCustomerServiceCaseInformationById";
    public const string USP_GetPlayersByPlayerId = "USP_GetPlayersByPlayerId";
    public const string USP_GetPlayersByUsername = "USP_GetPlayersByUsername";
    public const string USP_GetPCSQuestionAndAnswerListCsv = "USP_GetPCSQuestionAndAnswerListCsv";
    public const string USP_GetPCSCommunicationQuestionsById = "USP_GetPCSCommunicationQuestionsById";
    public const string USP_GetPCSCommunicationProviderOption = "USP_GetPCSCommunicationProviderOption";
    public const string USP_GetPCSCommunicationSummaryAction = "USP_GetPCSCommunicationSummaryAction";
    public const string USP_GetCaseManagementPCSQuestionsByFilter = "USP_GetCaseManagementPCSQuestionsByFilter";
    public const string USP_GetCustomerCaseCommSurveyTemplateById = @"USP_GetCustomerCaseCommSurveyTemplateById";
    public const string USP_GetCustomerCaseCommFeedback = @"USP_GetCustomerCaseCommFeedback";
    public const string USP_GetCustomerCaseCommSurvey = @"USP_GetCustomerCaseCommSurvey";
    public const string USP_GetCaseManagementPCSCommunicationByFilter = "USP_GetCaseManagementPCSCommunicationByFilter";
    public const string USP_InsertRequestFlyFoneRecordDetail = "USP_InsertRequestFlyFoneRecordDetail";
    public const string USP_GetFlyFoneCallDetailRecords = "USP_GetFlyFoneCallDetailRecords";
    public const string USP_UpdateFlyFoneCallDetailRecordByCallingCode = "USP_UpdateFlyFoneCallDetailRecordByCallingCode";
    public const string USP_UpdateFlyFoneCallDetailRecord = "USP_UpdateFlyFoneCallDetailRecord";
    public const string USP_InsertRequestCloudTalkRecordDetail = "USP_InsertRequestCloudTalkRecordDetail";
    public const string USP_AppendCaseCommunicationContent = @"[dbo].[USP_AppendCaseCommunicationContent]";
    public const string USP_UpdateCloudTalkCdrByCallingCode = @"USP_UpdateCloudTalkCdrByCallingCode";
    public const string USP_UpsertCustomerServiceCaseCommunication = @"USP_UpsertCustomerServiceCaseCommunication";
    public const string USP_GetCustomerCaseCampaignNamesByUsername = @"USP_GetCustomerCaseCampaignNamesByUsername";
    public const string USP_GetChatStatisticsByCommunicationId = @"USP_GetChatStatisticsByCommunicationId";

    #region Communication Review
    public const string USP_GetCommunicationReviewLookups = "USP_GetCommunicationReviewLookups";
    public const string USP_ValidateCommunicationReviewLimit = "USP_ValidateCommunicationReviewLimit";
    public const string USP_InsertCommunicationReviewEventLog = "USP_InsertCommunicationReviewEventLog";
    public const string USP_GetQualityReviewCriteriaByQualityReviewMeasurementId = "USP_GetQualityReviewCriteriaByQualityReviewMeasurementId";
    public const string USP_GetCommunicationReviewEventLogByCaseCommunicationId = "USP_GetCommunicationReviewEventLogByCaseCommunicationId";
    public const string USP_UpdateCommunicationReviewIsPrimary = "USP_UpdateCommunicationReviewIsPrimary";
    public const string USP_RemoveCommunicationReviewIsPrimary = "USP_RemoveCommunicationReviewIsPrimary";
    #endregion
    #region Samespace VoIP Integration
    public const string USP_InsertRequestSamespaceRecordDetail = "USP_InsertRequestSamespaceRecordDetail";
    public const string USP_UpdateSamespaceCdrByCallingCode = @"USP_UpdateSamespaceCdrByCallingCode";
    #endregion
    #region Case and Communication
    public const string USP_GetCaseCommunicationAnnotationByCaseCommunicationId = @"USP_GetCaseCommunicationAnnotationByCaseCommunicationId";
    public const string USP_UpsertCaseCommunicationAnnotation = @"USP_UpsertCaseCommunicationAnnotation";
    public const string USP_ValidateCaseCommunicationAnnotation = @"USP_ValidateCaseCommunicationAnnotation";
    #endregion

    // Chatbot
    public const string USP_ChatBotGetCaseAndPlayerInformationByParam = @"USP_ChatBotGetCaseAndPlayerInformationByParam";
    public const string USP_ChatBotGetPlayerTransactionDataByParam = @"USP_ChatBotGetPlayerTransactionDataByParam";
    public const string USP_ChatBotGetTopic = @"USP_ChatBotGetTopic";
    public const string USP_ChatBotGetSubTopic = @"USP_ChatBotGetSubTopic";
    public const string USP_ChatBotChangeStatus = @"USP_ChatBotChangeStatus";
    public const string USP_ChatBotUpsertAgentSurvey = @"USP_ChatBotUpsertAgentSurvey";

    public const string USP_GetSegmentCustomQueryProhibitedKeywords = "USP_GetSegmentCustomQueryProhibitedKeywords";

    //Reports
    public const string USP_GetCommunicationReviewReportByFilter = @"USP_GetCommunicationReviewReportByFilter";
    public const string USP_GetCommunicationReviewData = @"USP_GetCommunicationReviewData";


    // Engagement Hub
    public const string USP_ValidateTelegramBotId = @"USP_ValidateTelegramBotId";
    public const string USP_CancelBroadcast = @"USP_CancelBroadcast";
    public const string USP_DeleteTelegramBotDetailsAutoReply = @"USP_DeleteTelegramBotDetailsAutoReply";
    public const string USP_GetBotDetailsAutoReplyByFilter = @"USP_GetBotAutoReplyByFilter";

    //Ticket Management
    public const string USP_GetActiveTicketTypes = @"USP_GetActiveTicketTypes";
    public const string USP_GetActiveTicketSections = @"USP_GetActiveTicketSections";
    public const string USP_GetActivePageLists = @"USP_GetActivePageLists";    
    public const string USP_GetLookUpsByTicketFieldId = @"USP_GetLookUpsByTicketFieldId";
    public const string USP_GetTicketFieldMappingByTicketType = @"USP_GetTicketFieldMappingByTicketType";
    public const string USP_GetTicketCustomGroupByTicketType = @"USP_GetTicketCustomGroupByTicketType";
    public const string USP_GetCustomGroupPlayerDetails = @"USP_GetCustomGroupPlayerDetails";
    public const string USP_GetDepositTransactionDetailsById = @"USP_GetDepositTransactionDetailsById";
    public const string USP_GetTicketStatusHierarchyByTicketType = @"USP_GetTicketStatusHierarchyByTicketType";
    public const string USP_GetTicketDetails = @"USP_GetTicketDetails";
    public const string USP_ValidateTransactionID = @"USP_ValidateTransactionID";
    public const string USP_InsertTicketAttachment = @"USP_InsertTicketAttachment";
    public const string USP_GetTeamAssignments = @"USP_GetTeamAssignments";
    public const string USP_GetTicketStatusPopupMapping = @"USP_GetTicketStatusPopupMapping";
    public const string USP_GetFilterIDByUserId = @"USP_GetFilterIDByUserId";
    public const string USP_GetAssigneesByIds = @"USP_GetAssigneesByIds";
    public const string USP_GetAutoAssignedById = @"USP_GetAutoAssignedById";
    public const string USP_GetSavedFilterByFilterId = @"USP_GetSavedFilterByFilterId";
    public const string USP_TicketManagementLookups = @"USP_TicketManagementLookups";
    public const string USP_GetTicketThreshold = @"USP_GetTicketThreshold";
    public const string USP_GetSearchTicketByFilters = @"USP_GetSearchTicketByFilters";
    public const string USP_GetTransactionFieldMapping = @"USP_GetTransactionFieldMapping";
    public const string USP_UpsertTransactionDataFromApi = @"USP_UpsertTransactionDataFromApi";
    public const string USP_GetHiddenPaymentMethodTickets = @"USP_GetHiddenPaymentMethodTickets";
    public const string USP_GetAdjustmentBusinessTypeList = @"USP_GetAdjustmentBusinessTypeList";
    public const string USP_GetTransactionStatusReference = @"USP_GetTransactionStatusReference";
    public const string USP_ValidateAddUserAsCollaborator = @"USP_ValidateAddUserAsCollaborator";
    public const string USP_DeleteUserAsCollaborator = @"USP_DeleteUserAsCollaborator";
    public const string USP_GetUserCollaboratorList = @"USP_GetUserCollaboratorList";
    public const string USP_GetEmailDetails = @"USP_GetEmailDetails";
    public const string USP_ValidateUserTier = @"USP_ValidateUserTier";
    public const string USP_GetAllPaymentProcessor = @"USP_GetAllPaymentProcessor";

    // Search Leads
    public const string Usp_LinkUnlinkPlayer = @"Usp_LinkUnlinkPlayer";
    public const string Usp_RemoveLead = @"Usp_RemoveLead";
    public const string Usp_GetLeadLinkDetailsById = @"Usp_GetLeadLinkDetailsById";
    public const string USP_GetAllSourceBOT = @"USP_GetAllSourceBOT";
    public const string USP_GetLeadPlayersByUsername = "USP_GetLeadPlayersByUsername";
    public const string Usp_GetLeadSelectionByFilter = "Usp_GetLeadSelectionByFilter";


    // AppConfig
    public const string Usp_GetAppConfigSettingByFilter = @"Usp_GetAppConfigSettingByFilter";
    public const string Usp_TestSwitchServer = @"Usp_TestSwitchServer";
    public const string USP_GetAppConfigSettingByApplicationId = @"USP_GetAppConfigSettingByApplicationId";

    // StaffPerformance
    public const string USP_GetStaffPerformanceInfo = @"USP_GetStaffPerformanceInfo";
    public const string USP_UpsertCommunicationReviewPeriod = @"USP_UpsertCommunicationReviewPeriod";
    
}

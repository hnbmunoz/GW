using MediatR;
using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Administrator;
using MLAB.PlayerEngagement.Core.Models.AgentMonitoring;
using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Models.Authentication;
using MLAB.PlayerEngagement.Core.Models.CallListValidation;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;
using MLAB.PlayerEngagement.Core.Models.CampaignJourney;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Models.CampaignPerformance;
using MLAB.PlayerEngagement.Core.Models.CodelistSubtopic.Request;
using MLAB.PlayerEngagement.Core.Models.Feedback;
using MLAB.PlayerEngagement.Core.Models.Message;
using MLAB.PlayerEngagement.Core.Models.Player;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement;
using MLAB.PlayerEngagement.Core.Models.Segmentation;
using MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;
using MLAB.PlayerEngagement.Core.Request;
using MLAB.PlayerEngagement.Core.Services;
using Constants = MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.EngagementHub;
using MLAB.PlayerEngagement.Core.Models.Request;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;
using MLAB.PlayerEngagement.Core.Models.SearchLeads;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request;

namespace MLAB.PlayerEngagement.Application.Services;

public class MessagePublisherService : IMessagePublisherService
{

    private readonly IMediator _mediator;
    private readonly ILogger<MessagePublisherService> _logger;
    private readonly string _exchangeBinding = "?bind=true&";
    private readonly string _userManagementExchangeUri;
    private readonly string _playerManagementExchangeUri;
    private readonly string _systemExchangeUri;
    private readonly string _campaignWorkspaceExchangeUri;
    private readonly string _campaignDashboardExchangeUri;
    private readonly string _campaignManagementExchangeUri;
    private readonly string _campaignSettingExchangeUri;
    private readonly string _remManagementExchangeUri;
    private readonly string _campaignJourneyExchangeUri;
    private readonly string _aswIntegrationExchangeUri;
    private readonly string _caseManagementExchangeUri;
    private readonly string _engagementHubExchageUri;
    private readonly string _ticketManagementExchangeUri;
    public MessagePublisherService(IMediator mediator, IConfiguration configuration, ILogger<MessagePublisherService> logger)
    {
        _mediator = mediator;
        Configuration = configuration;
        _logger = logger;
        string rabbitEnvironment = Configuration.GetConnectionString("RabbitEnvironment");

        _userManagementExchangeUri = Exchanges.UserManagement + rabbitEnvironment + _exchangeBinding + QueueNames.userManagementQueue + rabbitEnvironment;
        _playerManagementExchangeUri = Exchanges.PlayerManagement + rabbitEnvironment + _exchangeBinding + QueueNames.playerManagementQueue + rabbitEnvironment;
        _systemExchangeUri = Exchanges.SystemConfiguration + rabbitEnvironment + _exchangeBinding + QueueNames.systemConfigurationQueue + rabbitEnvironment;
        _campaignWorkspaceExchangeUri = Exchanges.CampaignWorkspace + rabbitEnvironment + _exchangeBinding + QueueNames.campaignWorkspaceQueue + rabbitEnvironment;
        _campaignDashboardExchangeUri = Exchanges.CampaignDashboard + rabbitEnvironment + _exchangeBinding + QueueNames.campaignDashboardQueue + rabbitEnvironment;
        _campaignManagementExchangeUri = Exchanges.CampaignManagement + rabbitEnvironment + _exchangeBinding + QueueNames.campaignManagementQueue + rabbitEnvironment;
        _campaignSettingExchangeUri = Exchanges.CampaignSetting + rabbitEnvironment + _exchangeBinding + QueueNames.campaignSettingQueue + rabbitEnvironment;
        _remManagementExchangeUri = Exchanges.RemManagement + rabbitEnvironment + _exchangeBinding + QueueNames.remManagementQueue + rabbitEnvironment;
        _campaignJourneyExchangeUri = Exchanges.CampaignJourney + rabbitEnvironment + _exchangeBinding + QueueNames.campaignJourneyQueue + rabbitEnvironment;
        _aswIntegrationExchangeUri = Exchanges.AswIntegration + rabbitEnvironment + _exchangeBinding + QueueNames.aswIntegrationQueue + rabbitEnvironment;
        _caseManagementExchangeUri = Exchanges.CaseManagement + rabbitEnvironment + _exchangeBinding + QueueNames.casemanagementQueue + rabbitEnvironment;
        _engagementHubExchageUri = Exchanges.EngagementHub + rabbitEnvironment + _exchangeBinding + QueueNames.engagementHubQueue + rabbitEnvironment;
        _ticketManagementExchangeUri = Exchanges.TicketManagement + rabbitEnvironment + _exchangeBinding + QueueNames.ticketManagementQueue + rabbitEnvironment;
    }

    public IConfiguration Configuration { get; }

    public async Task<bool> GetRoleListAsync(RoleFilterModel roleFilter)
    {
        string remarks = "Getting Role list";
        string eventName = Convert.ToString(Actions.GetRoleListAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(roleFilter, roleFilter.QueueId, roleFilter.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddRoleAsync(RoleRequestModel createRole)
    {

        string remarks = "Adding Role";
        string eventName = Convert.ToString(Actions.AddRoleAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(createRole, createRole.QueueId, createRole.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateRoleAsync(RoleRequestModel updateRole)
    {
        string remarks = "Updating Role";
        string eventName = Convert.ToString(Actions.UpdateRoleAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(updateRole, updateRole.QueueId, updateRole.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetRoleByIdAsync(RoleIdRequestModel role)
    {
        string remarks = "Getting Role";
        string eventName = Convert.ToString(Actions.GetRoleByIdAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(role, role.QueueId, role.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCloneRoleAsync(RoleIdRequestModel role)
    {
        string remarks = "Getting Clone";
        string eventName = Convert.ToString(Actions.GetCloneRoleAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(role, role.QueueId, role.UserId, remarks, eventName, exchangeUri);

    }

    public async Task<bool> GetAllRoleAsync(BaseModel request)
    {
        string remarks = "Getting All roles";
        string eventName = Convert.ToString(Actions.GetAllRoleAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetTeamListAsync(TeamFilterModel teamFilter)
    {
        string remarks = "Getting Team List";
        string eventName = Convert.ToString(Actions.GetTeamListAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(teamFilter, teamFilter.QueueId, teamFilter.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddTeamAsync(TeamRequestModel createTeam)
    {
        string remarks = "Adding Team";
        string eventName = Convert.ToString(Actions.AddTeamAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(createTeam, createTeam.QueueId, createTeam.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateTeamAsync(TeamRequestModel updateTeam)
    {
        string remarks = "Updating Team";
        string eventName = Convert.ToString(Actions.UpdateTeamAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(updateTeam, updateTeam.QueueId, updateTeam.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetTeamByIdAsync(TeamIdRequestModel team)
    {
        string remarks = "Getting Team by Id";
        string eventName = Convert.ToString(Actions.GetTeamByIdAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(team, team.QueueId, team.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetUserListAsync(UserFilterModel userFilter)
    {
        string remarks = "Getting user list";
        string eventName = Convert.ToString(Actions.GetUserListAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(userFilter, userFilter.QueueId, userFilter.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetUserByIdAsync(UserIdRequestModel user)
    {
        string remarks = "Getting user by id";
        string eventName = Convert.ToString(Actions.GetUserByIdAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(user, user.QueueId, user.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCommunicationProviderAccountListbyIdAsync(UserIdRequestModel user)
    {
        string remarks = "Getting communication provider list by user id";
        string eventName = Convert.ToString(Actions.GetCommunicationProviderAccountListbyIdAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(user, user.QueueId, user.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddUserAsync(UserRequestModel createUser)
    {
        string remarks = "Adding user";
        string eventName = Convert.ToString(Actions.AddUserAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(createUser, createUser.QueueId, createUser.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateUserAsync(UserRequestModel updateUser)
    {
        string remarks = "Updating user";
        string eventName = Convert.ToString(Actions.UpdateUserAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(updateUser, updateUser.QueueId, updateUser.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> LockUserAsync(LockUserRequestModel lockUser)
    {
        string remarks = "Locking user";
        string eventName = Convert.ToString(Actions.LockUserAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(lockUser, lockUser.QueueId, lockUser.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> CreatePasswordAsync(CreateNewPasswordRequest request)
    {
        string remarks = "Creating New Password";
        string eventName = Convert.ToString(Actions.CreateNewPasswordAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {
        string remarks = "Resetting Password";
        string eventName = Convert.ToString(Actions.ResetPasswordAsync);
        string exchangeUri = _userManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ImportPlayerAsync(ImportPlayersRequestModel playerImportModel)
    {
        string remarks = "Import ICore Players";
        string eventName = Convert.ToString(Actions.ImportPlayersAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(playerImportModel, playerImportModel.QueueId, playerImportModel.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ValidateImportPlayerAsync(ImportPlayersRequestModel playerImportModel)
    {
        string remarks = "Validate Import ICore Players";
        string eventName = Convert.ToString(Actions.ValidateImportPlayersAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(playerImportModel, playerImportModel.QueueId, playerImportModel.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetAllCodelistAsync(BaseModel request)
    {
        string remarks = "Get All Code list";
        string eventName = Convert.ToString(Actions.GetAllCodeListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddCodeListAsync(CodeListModel codelist)
    {
        string remarks = "Add Code list";
        string eventName = Convert.ToString(Actions.AddCodeListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(codelist, codelist.QueueId, codelist.UserId, remarks, eventName, exchangeUri, Constants.Services.SystemService.ToString());
    }

    private async Task<bool> BuildPublishMessage<T>(T userRequest, string queueId, string userId, string remarks, string eventName, string exchangeUri, string moduleService = null)
    {

        string redisCacheRequestId = Guid.NewGuid().ToString();
        string _moduleService = moduleService ?? (Constants.Services.CampaignTaggingPointSettingService).ToString();

        var createMemoryCacheCommand = new CreateMemoryCacheCommand
        {
            Id = redisCacheRequestId,
            Data = userRequest

        };

        var resultCreateMemoryCacheCommand = await _mediator.Send(createMemoryCacheCommand);

        if (resultCreateMemoryCacheCommand.result)
        {
            var createQueueCommand = new CreateQueueCommand
            {
                QueueId = Guid.Parse(queueId),
                QueueName = eventName,
                CreatedBy = userId,
                RedisCacheRequestId = Guid.Parse(redisCacheRequestId),
                QueueStatus = Convert.ToString(QueueStatus.PUBLISHED),
                ServiceTypeId = Guid.Parse(Configuration.GetConnectionString("ServiceTypeId")),
                Remarks = remarks,
                Action = eventName,
                UserId = userId
            };

            var resultCreateQueueCommand = await _mediator.Send(createQueueCommand);

            _logger.LogInfo($"{_moduleService} | BuildPublishMessage | {eventName} : [Received {queueId}] - {resultCreateQueueCommand.result} | [Request: {userRequest}]");

            var createQueuePublishCommand = new CreateQueuePublishCommand
            {
                Id = Guid.Parse(queueId),
                Name = eventName,
                Action = eventName,
                CacheId = Guid.Parse(redisCacheRequestId),
                ExchangeUri = exchangeUri,
                CallbackUri = Configuration.GetConnectionString("CallbackUrl"),
                QueueStatus = Convert.ToString(QueueStatus.PUBLISHED),
                UserId = userId,
                Remarks = remarks
            };
            var resultCreateQueuePublishCommand = await _mediator.Send(createQueuePublishCommand);

            _logger.LogInfo($"{_moduleService} | BuildPublishMessage | {eventName} : [Response {queueId}] - {resultCreateQueuePublishCommand.result}");

            return true;
        }

        else
        {
            _logger.LogError($"{_moduleService} | BuildPublishMessage | {eventName} : [Exception {queueId}] - Problem saving data in redis cache | [Request: {userRequest}]");
            return false;
        }

    }

    public async Task<bool> AddCodeListTypeAsync(CodeListTypeModel codelistType)
    {
        string remarks = "Add Code list Type";
        string eventName = Convert.ToString(Actions.AddCodeListTypeAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(codelistType, codelistType.QueueId, codelistType.UserId, remarks, eventName, exchangeUri, Constants.Services.SystemService.ToString());
    }

    public async Task<bool> GetAllCodelistTypeAsync(BaseModel request)
    {
        string remarks = "Get All Code list Type";
        string eventName = Convert.ToString(Actions.GetAllCodeListTypeAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, Constants.Services.SystemService.ToString());
    }

    public async Task<bool> GetAllFieldTypeAsync(BaseModel request)
    {
        string remarks = "Get All Field Type";
        string eventName = Convert.ToString(Actions.GetAllFieldTypeAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddTopicAsync(CodeListTopicModel request)
    {
        string remarks = "Add Topic";
        string eventName = Convert.ToString(Actions.AddTopicAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpSertTopicAsync(UpSertTopicRequestModel request)
    {
        string remarks = "UpSert Topic";
        string eventName = Convert.ToString(Actions.UpSertTopicAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetTopicByIdAsync(GetTopicByIdRequestModel request)
    {
        string remarks = "Get Topic By Id";
        string eventName = Convert.ToString(Actions.GetTopicByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetTopicListByFilterAsync(TopicRequest request)
    {
        string remarks = "Get Topic List By Filter";
        string eventName = Convert.ToString(Actions.GetTopicListByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetSubtopicByFilterAsync(SubTopicRequest request)
    {
        string remarks = "Get Subtopic List By Filter";
        string eventName = Convert.ToString(Actions.GetSubtopicByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SubmitSubtopicAsync(SubtopicRequestModel request)
    {
        string remarks = "Submit Subtopic";
        string eventName = Convert.ToString(Actions.SubmitSubtopicAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertSubtopicAsync(SubtopicNewRequestModel request)
    {
        string remarks = "Upsert Subtopic";
        string eventName = Convert.ToString(Actions.UpsertSubtopicAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetSubTopicById(SubtopicIdRequestModel request)
    {
        string remarks = "Get Subtopic By Id";
        string eventName = Convert.ToString(Actions.GetSubtopicByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCodeListByIdAsync(CodeListIdModel request)
    {
        string remarks = "Get Codelist By Id";
        string eventName = Convert.ToString(Actions.GetCodeListByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, Constants.Services.SystemService.ToString());
    }

    public async Task<bool> GetVIPLevelById(VIPLevelIdRequestModel request)
    {
        string remarks = "Get Level By Id";
        string eventName = Convert.ToString(Actions.GetVIPLevelByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetVIPLevelByFilterAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Level By Id";
        string eventName = Convert.ToString(Actions.GetVIPLevelByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetAllPlayerConfigurationAsync(BaseModel request)
    {
        string remarks = "Get All Player Configuration";
        string eventName = Convert.ToString(Actions.GetAllPlayerConfigurationAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetPlayerConfigurationByIdAsync(PlayerConfigurationIdRequestModel request)
    {
        string remarks = "Get All Player Configuration";
        string eventName = Convert.ToString(Actions.GetPlayerConfigurationByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddVIPLevelAsync(VipLevelRequestModel request)
    {
        string remarks = "Add VIP Level Player Configuration";
        string eventName = Convert.ToString(Actions.AddVIPLevelAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateVIPLevelAsync(VipLevelRequestModel request)
    {
        string remarks = "UpdateVIP Level Player Configuration";
        string eventName = Convert.ToString(Actions.UpdateVIPLevelAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetRiskLevelByFilterAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Risk Level Player Configuration Filter";
        string eventName = Convert.ToString(Actions.GetRiskLevelByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetRiskLevelByIdAsync(RiskLevelIdModel request)
    {
        string remarks = "Get Risk Level Player Configuration";
        string eventName = Convert.ToString(Actions.GetRiskLevelByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddRiskLeveldAsync(RiskLevelModel request)
    {
        string remarks = "Add Risk Level Player Configuration";
        string eventName = Convert.ToString(Actions.AddRiskLeveldAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateRiskLeveldAsync(RiskLevelModel request)
    {
        string remarks = "Update Risk Level Player Configuration";
        string eventName = Convert.ToString(Actions.UpdateRiskLeveldAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetMessageTypeListAsync(MessageTypeListFilterModel request)
    {
        string remarks = "Get Message Type List";
        string eventName = Convert.ToString(Actions.GetMessageTypeListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddMessageListAsync(CodeListMessageTypeModel request)
    {
        string remarks = "Add Message Type List";
        string eventName = Convert.ToString(Actions.AddMessageListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetMesssageTypeByIdAsync(MessageTypeIdModel request)
    {
        string remarks = "Get Message Type";
        string eventName = Convert.ToString(Actions.GetMesssageTypeByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetMessageStatusListAsync(MessageStatusListFilterModel request)
    {
        string remarks = "Get Message Status List";
        string eventName = Convert.ToString(Actions.GetMessageStatusListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddMessageStatusListAsync(MessageStatusRequestModel request)
    {
        string remarks = "Add Message Status List";
        string eventName = Convert.ToString(Actions.AddMessageStatusListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetMesssageStatusByIdAsync(MessageStatusIdModel request)
    {
        string remarks = "Get Message Status";
        string eventName = Convert.ToString(Actions.GetMesssageStatusByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetMessageResponseListAsync(MessageResponseListFilterModel request)
    {
        string remarks = "Get Message Resonse List";
        string eventName = Convert.ToString(Actions.GetMessageResponseListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddMessageResponseListAsync(MessageResponseRequestModel request)
    {
        string remarks = "Add Message Resonse List";
        string eventName = Convert.ToString(Actions.AddMessageResponseListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetMesssageResponseByIdAsync(MessageResponseIdModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Actions.GetMesssageResponseByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SaveSurveyQuestionAsync(SaveSurveyQuestionsModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Actions.SaveSurveyQuestionAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SaveSurveyTemplateAsync(SaveSurveyTemplateModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Actions.SaveSurveyTemplateAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetSurveyQuestionsByFilterAsync(SurveyQuestionsListFilterModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Actions.GetSurveyQuestionsByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetSurveyQuestionByIdAsync(SurveyQuestionByIdModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Actions.GetSurveyQuestionByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetSurveyTemplateByFilterAsync(SurveyTemplateListFilterModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Actions.GetSurveyTemplateByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetSurveyTemplateById(SurveyTemplateByIdModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Actions.GetSurveyTemplateByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetFeedbackTypeListAsync(FeedbackTypeListFilterModel request)
    {
        string remarks = "Get Feedback Type";
        string eventName = Convert.ToString(Actions.GetFeedbackTypeListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddFeedbackTypeListAsync(AddFeedbackTypeModel request)
    {
        string remarks = "Add Message Type";
        string eventName = Convert.ToString(Actions.AddFeedbackTypeListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetFeedbackTypeByIdAsync(FeedbackTypeIdModel request)
    {
        string remarks = "Get Feedback Type";
        string eventName = Convert.ToString(Actions.GetFeedbackTypeByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetFeedbackCategoryListAsync(FeedbakCategoryListFilterModel request)
    {
        string remarks = "Get Feedback Category List";
        string eventName = Convert.ToString(Actions.GetFeedbackCategoryListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddFeedbackCategoryListAsync(AddFeedbackCategoryModel request)
    {
        string remarks = "Add Feedback Category List";
        string eventName = Convert.ToString(Actions.AddFeedbackCategoryListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetFeedbackCategoryByIdAsync(FeedbackCategoryIdModel request)
    {
        string remarks = "Get Feedback Category By Id";
        string eventName = Convert.ToString(Actions.GetFeedbackCategoryByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetFeedbackAnswerListAsync(FeedbackAnswerListFilterModel request)
    {
        string remarks = "Get Feedback Answer Resonse";
        string eventName = Convert.ToString(Actions.GetFeedbackAnswerListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddFeedbackAnswerListAsync(AddFeedbackAnswerModel request)
    {
        string remarks = "Add Feedback Answer List Resonse";
        string eventName = Convert.ToString(Actions.AddFeedbackAnswerListAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetFeedbackAnswerByIdAsync(FeedbackAnswerIdModel request)
    {
        string remarks = "Get Feedback Answer Id Resonse";
        string eventName = Convert.ToString(Actions.GetFeedbackAnswerByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignPlayerListByFilterAsync(CampaignPlayerFilterRequestModel request)
    {
        string remarks = "Get Campaign Player List By Filter";
        string eventName = Convert.ToString(Actions.GetCampaignPlayerListByFilterAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignListAsync(CampaignListRequestModel request)
    {
        string remarks = "Get Campaign List";
        string eventName = Convert.ToString(Actions.GetCampaignListAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> SaveCampaignAsync(CampaignModel request)
    {
        string remarks = "Save campaign";
        string eventName = Convert.ToString(Actions.SaveCampaignAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignByIdAsync(CampaignIdModel request)
    {
        string remarks = "Get campaign";
        string eventName = Convert.ToString(Actions.GetCampaignByIdAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignGoalSettingByFilterAsync(CampaignGoalSettingByFilterRequestModel request)
    {
        string remarks = "Get Campaign Goal Setting By Filter";
        string eventName = Convert.ToString(Actions.GetCampaignGoalSettingByFilterAsync);
        string exchangeUri = _campaignSettingExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignGoalSettingByIdAsync(CampaignGoalSettingIdRequestModel request)
    {
        string remarks = "Get Campaign Goal Setting By Id";
        string eventName = Convert.ToString(Actions.GetCampaignGoalSettingByIdAsync);
        string exchangeUri = _campaignSettingExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddCampaignGoalSettingAsync(CampaignGoalSettingRequestModel request)
    {
        string remarks = "Add Campaign Goal Setting";
        string eventName = Convert.ToString(Actions.AddCampaignGoalSettingAsync);
        string exchangeUri = _campaignSettingExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetSegmentationByFilterAsync(SegmentationRequestModel request)
    {
        string remarks = "Get Segmentation By Filter";
        string eventName = Convert.ToString(Actions.GetSegmentationByFilterAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SaveSegmentAsync(SegmentationModel request)
    {
        string remarks = "Save Segmentation";
        string eventName = Convert.ToString(Actions.SaveSegmentationAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> TestSegmentAsync(SegmentationTestModel request)
    {
        string remarks = "Test Segment Conditions";
        string eventName = Convert.ToString(Actions.TestSegmentationAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ToStaticSegmentAsync(SegmentationToStaticModel request)
    {
        string remarks = "Test Segment Conditions";
        string eventName = Convert.ToString(Actions.ToStaticSegmentAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetSegmentDistributionByFilterAsync(SegmentDistributionByFilterRequestModel request)
    {
        string remarks = "Get Segment Distribution By Filter";
        string eventName = Convert.ToString(Actions.GetSegmentDistributionByFilterAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    //Call List Validation
    public async Task<bool> UpsertAgentValidationAsync(List<AgentValidationRequestModel> request)
    {
        string remarks = "Upsert agent validations";
        string eventName = Convert.ToString(Actions.UpsertAgentValidationAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.FirstOrDefault()?.QueueId, request.FirstOrDefault()?.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertLeaderValidationAsync(List<LeaderValidationsRequestModel> request)
    {
        string remarks = "Upsert leader validations";
        string eventName = Convert.ToString(Actions.UpsertLeaderValidationAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.FirstOrDefault()?.QueueId, request.FirstOrDefault()?.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertCallEvaluationAsync(CallEvaluationRequestModel request)
    {
        string remarks = "Upsert call evaluations";
        string eventName = Convert.ToString(Actions.UpsertCallEvaluationAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> DeleteCallEvaluationAsync(DeleteCallEvaluationRequestModel request)
    {
        string remarks = "delete  call evaluations";
        string eventName = Convert.ToString(Actions.DeleteCallEvaluationAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertLeaderJustificationAsync(List<LeaderJustificationRequestModel> request)
    {
        string remarks = "Upsert leader justifications";
        string eventName = Convert.ToString(Actions.UpsertLeaderJustificationAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.FirstOrDefault()?.QueueId, request.FirstOrDefault()?.UserId, remarks, eventName, exchangeUri);
    }

    //Agent Monitoring
    public async Task<bool> UpdateCampaignAgentStatusAsync(AgentStatusRequestModel request)
    {
        string remarks = "Update campaign agent status";
        string eventName = Convert.ToString(Actions.UpdateCampaignAgentStatusAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertDailyReportAsync(List<DailyReportRequestModel> request)
    {
        string remarks = "Upsert daily report";
        string eventName = Convert.ToString(Actions.UpsertDailyReportAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.FirstOrDefault()?.QueueId, request.FirstOrDefault()?.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> DeleteDailyReportByIdAsync(List<DeleteDailyReportRequestModel > request)
    {
        string remarks = "Delete daily report";
        string eventName = Convert.ToString(Actions.DeleteDailyReportByIdAsync);
        string exchangeUri = _campaignWorkspaceExchangeUri;

        return await BuildPublishMessage(request, request.FirstOrDefault()?.QueueId, request.FirstOrDefault()?.UserId, remarks, eventName, exchangeUri);
    }

    // Campaign Performance

    public async Task<bool> GetCampaignPerformanceListAsync(CampaignPerformanceRequestModel request)
    {
       string remarks = "Get Campaign Performance List";
       string eventName = Convert.ToString(Actions.GetCampaignPerformanceListAsync);
       string exchangeUri = _campaignDashboardExchangeUri;

       return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetViewContactLogListAsync(ContactLogListRequestModel request)
    {
        string remarks = "Get View Contact Log List ";
        string eventName = Convert.ToString(Actions.GetViewContactLogListAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetViewContactLogTeamListAsync(ContactLogListRequestModel request)
    {
        string remarks = "Get View Contact Log Team List ";
        string eventName = Convert.ToString(Actions.GetViewContactLogTeamListAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetViewContactLogUserListAsync(ContactLogListRequestModel request)
    {
        string remarks = "Get View Contact Log User List ";
        string eventName = Convert.ToString(Actions.GetViewContactLogUserListAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    #region CaseCommunication
    public async Task<bool> AddCaseCommunicationAsync(AddCaseCommunicationRequest request)
    {
        string remarks = "Add Case Communication";
        string eventName = Convert.ToString(Actions.AddCaseCommunicationAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCaseInformationbyIdAsync(CaseInformationRequest request)
    {
        string remarks = "Get Case Information by Id";
        string eventName = Convert.ToString(Actions.GetCaseInformationbyIdAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCommunicationByIdAsync(CommunicationByIdRequest request)
    {
        string remarks = "Get Communication By Id";
        string eventName = Convert.ToString(Actions.GetCommunicationByIdAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCommunicationListAsync(CommunicationListRequest request)
    {
        string remarks = "Get Communication List";
        string eventName = Convert.ToString(Actions.GetCommunicationListAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ChangeCaseStatusAsync(ChangeCaseStatusRequest request)
    {
        string remarks = "Change Case Status";
        string eventName = Convert.ToString(Actions.ChangeCaseStatusAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCommunicationSurveyAsync(CommunicationSurveyRequest request)
    {
        string remarks = "Get Commucation Survey";
        string eventName = Convert.ToString(Actions.GetCommunicationSurveyAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCommunicationFeedbackListAsync(CommunicationFeedbackListRequest request)
    {
        string remarks = "Get Communication Feedback List";
        string eventName = Convert.ToString(Actions.GetCommunicationFeedbackListAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateCaseInformationAsync(UpdateCaseInformationRequest request)
    {
        string remarks = "Update Case Information";
        string eventName = Convert.ToString(Actions.UpdateCaseInformationAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCaseCampaignByIdAsync(CaseCampaignByIdRequest request)
    {
        string remarks = "Get Case Campaign By Id";
        string eventName = Convert.ToString(Actions.GetCaseCampaignByIdAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCaseContributorByIdAsync(CaseContributorListRequest request)
    {
        string remarks = "Get Case Contributor By Id";
        string eventName = Convert.ToString(Actions.GetCaseContributorByIdAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    #endregion

    #region Campaign Dashboard
    public async Task<bool> GetCampaignSurveyAndFeedbackReportAsync(CampaignSurveyAndFeedbackReportRequestModel request)
    {
        string remarks = "Get Campaign Dashboard Survey and Feedback Report";
        string eventName = Convert.ToString(Actions.GetCampaignSurveyAndFeedbackReportAsync);
        string exchangeUri = _campaignDashboardExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }


    #endregion

    public async Task<bool> UpsertUserGridCustomDisplayAsync(UserGridCustomDisplayModel request)
    {
        string remarks = "Upsert user grid custom display";
        string eventName = Convert.ToString(Actions.UpsertUserGridCustomDisplayAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SaveManageThresholdAsync(SaveManageThresholdRequest request)
    {
        string remarks = "Save ManageThreshold";
        string eventName = Convert.ToString(Actions.SaveManageThresholdAsync);
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetPlayerConfigLanguageAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Player Configuration Language Details";
        string eventName = Convert.ToString(Actions.GetPlayerConfigLanguageAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetPlayerConfigPlayerStatusAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Player Configuration Player Status Details";
        string eventName = Convert.ToString(Actions.GetPlayerConfigPlayerStatusAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetPlayerConfigPortalAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Player Configuration Portal Details";
        string eventName = Convert.ToString(Actions.GetPlayerConfigPortalAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SavePlayerConfigCodeDetailsAsync(PlayerConfigCodeListDetailsRequestModel request)
    {
        string remarks = "Save Player Configuration Details";
        string eventName = Convert.ToString(Actions.SavePlayerConfigCodeDetailsAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, Constants.Services.SystemService.ToString());
    }

    public async Task<bool> SavePaymentMethodAsync(SavePaymentMethodRequestModel request)
    {
        string remarks = "Save Payment Method Details";
        string eventName = Convert.ToString(Actions.SavePaymentMethodAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SaveTicketFieldsAsync(SaveTicketFieldsRequestModel request)
    {
        string remarks = "Save Payment Method Details";
        string eventName = Convert.ToString(Actions.SaveTicketFieldsAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetPaymentGroupByFilterAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Payment Group by Filter";
        string eventName = Convert.ToString(Actions.GetPaymentGroupByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetMarketingChannelByFilterAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Marketing Channel by Filter";
        string eventName = Convert.ToString(Actions.GetMarketingChannelByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCurrencyByFilterAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Currency by Filter";
        string eventName = Convert.ToString(Actions.GetCurrencyByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetPaymentMethodByFilterAsync(PaymentMethodRequestModel request)
    {
        string remarks = "Get Payment Method by Filter";
        string eventName = Convert.ToString(Actions.GetPaymentMethodByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetPlayerConfigCountryAsync(PlayerConfigurationRequestModel request)
    {
        string remarks = "Get Player Configuration Country Details";
        string eventName = Convert.ToString(Actions.GetPlayerConfigCountryAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ValidateImportRetentionPlayerAsync(CampaignImportPlayerRequestModel request)
    {
        string remarks = "Validate Import Retention Player";
        string eventName = Convert.ToString(Actions.ValidateImportRetentionPlayerAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ProcessCampaignImportPlayersAsync(CampaignImportPlayerRequestModel request)
    {
        string remarks = "Process Import Retention Player";
        string eventName = Convert.ToString(Actions.ProcessCampaignImportPlayersAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignUploadPlayerListAsync(UploadPlayerFilterModel request)
    {
        string remarks = "GET Import Retention Player List";
        string eventName = Convert.ToString(Actions.GetCampaignUploadPlayerListAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> RemoveCampaignImportPlayersAsync(CampaignImportPlayerModel request)
    {
        string remarks = "Process Remove Retention Player";
        string eventName = Convert.ToString(Actions.RemoveCampaignImportPlayersAsync);
        string exchangeUri = _campaignManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignCustomEventSettingByFilterAsync(CampaignCustomEventSettingRequestModel request)
    {
        string remarks = "Get Campaign Custom Event Setting By Filter";
        string eventName = Convert.ToString(Actions.GetCampaignCustomEventSettingByFilterAsync);
        string exchangeUri = _campaignSettingExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> AddCampaignCustomEventSettingAsync(CampaignCustomEventSettingModel request)
    {
        string remarks = "Add Campaign Custom Event Setting";
        string eventName = Convert.ToString(Actions.AddCampaignCustomEventSettingAsync);
        string exchangeUri = _campaignSettingExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetRemProfileByFilterAsync(RemProfileFilterRequestModel request)
    {
        string remarks = "Get Rem Profile By Filter";
        string eventName = Convert.ToString(Actions.GetRemProfileByFilterAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetRemProfileByIdAsync(RemProfileFilterRequestModel request)
    {
        string remarks = "Get Rem Profile By id";
        string eventName = Convert.ToString(Actions.GetRemProfileByIdAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetRemDistributionByFilterAsync(RemDistributionFilterRequestModel request)
    {
        string remarks = "Get Rem Distribution By Filter";
        string eventName = Convert.ToString(Actions.GetRemDistributionByFilterAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, Constants.Services.REMService.ToString());
    }
    public async Task<bool> GetRemHistoryByFilterAsync(RemHistoryFilterRequestModel request)
    {
        string remarks = "Get Rem History By Filter";
        string eventName = Convert.ToString(Actions.GetRemHistoryByFilterAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetScheduleTemplateSettingListAsync(ScheduleTemplateListRequestModel request)
    {
        string remarks = "Get Schedule Template Setting List";
        string eventName = Convert.ToString(Actions.GetScheduleTemplateSettingListAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetScheduleTemplateSettingByIdAsync(ScheduleTemplateByIdRequestModel request)
    {
        string remarks = "Get Schedule Template Setting By Id";
        string eventName = Convert.ToString(Actions.GetScheduleTemplateSettingByIdAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetScheduleTemplateLanguageSettingListAsync(ScheduleTemplateLanguageRequestModel request)
    {
        string remarks = "Get Schedule Template Language Setting List";
        string eventName = Convert.ToString(Actions.GetScheduleTemplateLanguageSettingListAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SaveScheduleTemplateSettingAsync(SaveScheduleTemplateRequestModel request)
    {
        string remarks = "Save Schedule Template Setting";
        string eventName = Convert.ToString(Actions.SaveScheduleTemplateSettingAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpSertRemProfileAsync(RemProfileDetailsRequestModel request)
    {
        string remarks = "Add or edit Rem Profile details";
        string eventName = Convert.ToString(Actions.UpSertRemProfileAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetAutoDistributionSettingConfigsListByFilterAsync(AutoDistributionSettingFilterRequestModel request)
    {
        string remarks = "Get Rem Auto Distribution Setting (Configuration Tab) By Filter";
        string eventName = Convert.ToString(Actions.GetAutoDistributionSettingConfigsListByFilterAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, Constants.Services.REMService.ToString());
    }
    public async Task<bool> GetAutoDistributionSettingAgentsListByFilterAsync(AutoDistributionSettingFilterRequestModel request)
    {
        string remarks = "Get Rem Auto Distribution Setting (Agent Tab) By Filter";
        string eventName = Convert.ToString(Actions.GetAutoDistributionSettingAgentsListByFilterAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, Constants.Services.REMService.ToString());
    }
    public async Task<bool> SaveAutoDistributionConfigurationAsync(AutoDistributionConfigurationRequestModel request)
    {
        string remarks = "Save Auto Distribution Configuration";
        string eventName = Convert.ToString(Actions.SaveAutoDistributionConfigurationAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetAutoDistributionConfigurationDetailsByIdAsync(AutoDistributionConfigurationByIdRequestModel request)
    {
        string remarks = "Get Auto Distribution Configuration By Id";
        string eventName = Convert.ToString(Actions.GetAutoDistributionConfigurationDetailsByIdAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetAutoDistributionConfigurationListByAgentIdAsync(AutoDistributionConfigurationListByAgentIdRequestModel request)
    {
        string remarks = "Get AUto Distribution Configuration List By Agent Id";
        string eventName = Convert.ToString(Actions.GetAutoDistributionConfigurationListByAgentIdAsync);
        string exchangeUri = _remManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    //Campaign Journey
    public async Task<bool> GetJourneyGridAsync(JourneyGridRequestModel request)
    {
        string remarks = "Get journey grid";
        string eventName = Convert.ToString(Actions.GetJourneyGridAsync);
        string exchangeUri = _campaignJourneyExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetJourneyDetailsAsync(JourneyDetailsRequestModel request)
    {
        string remarks = "Get journey details";
        string eventName = Convert.ToString(Actions.GetJourneyDetailsAsync);
        string exchangeUri = _campaignJourneyExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SaveJourneyAsync(JourneyRequestModel request)
    {
        string remarks = "Save journey";
        string eventName = Convert.ToString(Actions.SaveJourneyAsync);
        string exchangeUri = _campaignJourneyExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertSegmentAsync(SegmentationModel request)
    {
        string remarks = "Add or edit Segment details";
        string eventName = Convert.ToString(Actions.SaveSegmentationAsync); 
        string exchangeUri = _playerManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertCampaignGoalSettingAsync(CampaignGoalSettingRequestModel request)
    {
        string remarks = "Add Campaign Goal Setting New";
        string eventName = Convert.ToString(Actions.UpsertCampaignGoalSettingAsync);
        string exchangeUri = _campaignSettingExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCampaignGoalSettingByIdDetailsAsync(CampaignGoalSettingIdRequestModel request)
    {
        string remarks = "Get Campaign Goal Setting By Id Details";
        string eventName = Convert.ToString(Actions.GetCampaignGoalSettingByIdDetailsAsync);
        string exchangeUri = _campaignSettingExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetPostChatSurveyByFilterAsync(PostChatSurveyFilterRequestModel request)
    {
        string remarks = "Get Post Chat Survey By Filter";
        string eventName = Convert.ToString(Actions.GetPostChatSurveyByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetSkillByFilterAsync(SkillFilterRequestModel request)
    {
        string remarks = "Get Skill By Filter";
        string eventName = Convert.ToString(Actions.GetSkillByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertPostChatSurveyAsync(PostChatSurveyRequestModel request)
    {
        string remarks = "Add or Edit Post Chat Survey";
        string eventName = Convert.ToString(Actions.UpsertPostChatSurveyAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetPostChatSurveyByIdAsync(PostChatSurveyIdRequestModel request)
    {
        string remarks = "Get Post Chat Survey by Id";
        string eventName = Convert.ToString(Actions.GetPostChatSurveyByIdAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertSkillAsync(SkillRequestModel request)
    {
        string remarks = "Add or Edit Skill";
        string eventName = Convert.ToString(Actions.UpsertSkillAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateTopicOrderAsync(UpdateTopicOrderRequestModel request)
    {
        string remarks = "Update Topic Order";
        string eventName = Convert.ToString(Actions.UpdateTopicOrderAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateSubtopicOrderAsync(UpdateSubtopicOrderRequestModel request)
    {
        string remarks = "Update Subtopic Order";
        string eventName = Convert.ToString(Actions.UpdateSubtopicOrderAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpdateTopicStatusAsync(UpdateTopicStatusRequestModel request)
    {
        string remarks = "Update Subtopic Order";
        string eventName = Convert.ToString(Actions.UpdateTopicStatusAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpserSertAgentSurveyAsync(AgentSurveyRequest request, string platform)
    {
        string provider = String.IsNullOrWhiteSpace(platform) ? "Live Person" : platform;
        string remarks = "Upsert Agent Survey " + provider;
        string eventName = Convert.ToString(Actions.UpserSertAgentSurveyAsync);
        string exchangeUri = _aswIntegrationExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetAppConfigSettingByFilterAsync(AppConfigSettingFilterRequestModel request)
    {
        string remarks = "Get App Config Setting";
        string eventName = Convert.ToString(Actions.GetAppConfigSettingByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertAppConfigSettingAsync(AppConfigSettingRequestModel request)
    {
        string remarks = "Upsert App Config Setting";
        string eventName = Convert.ToString(Actions.UpsertAppConfigSettingAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetCaseCommunicationListAsync(CaseCommunicationFilterRequest request)
    {
        string remarks = "Search case and communication";
        string eventName = Convert.ToString(Actions.GetCaseCommunicationListAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpSertCustomerServiceCaseCommunicationAsync(AddCustomerServiceCaseCommunicationRequest request)
    {
        string remarks = "Add case and communication Customer Service";
        string eventName = Convert.ToString(Actions.UpSertCustomerServiceCaseCommunicationAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> ChangeCustomerServiceCaseCommStatusAsync(ChangeStatusCustomerServiceRequest request)
    {
        string remarks = "Reopen case and communication Customer Service";
        string eventName = Convert.ToString(Actions.ChangeCustomerServiceCaseCommStatusAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpsertChatSurveyActionAndSummaryAsync(ChatSurveySummaryAndActionRequestModel request)
    {
        string remarks = "Update Chat Survey Sumary and action";
        string eventName = Convert.ToString(Actions.UpsertChatSurveyActionAndSummaryAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetCaseManagementPCSQuestionsByFilterAsync(PCSQuestionaireListByFilterRequestModel request)
    {
        string remarks = "Get Case Management PCS Questions By Filter";
        string eventName = Convert.ToString(Actions.GetCaseManagementPCSQuestionsByFilterAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);

    }

    public async Task<bool> GetChatSurveyByIdAsync(ChatSurveyByIdRequestModel request)
    {
        string remarks = "Get Chat Survey by Id";
        string eventName = Convert.ToString(Actions.GetChatSurveyByIdAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetCaseManagementPCSCommunicationByFilterAsync(CaseManagementPCSCommunicationByFilterRequestModel request)
    {
        string remarks = "Get Case Management PCS Communication By Filter";
        string eventName = Convert.ToString(Actions.GetCaseManagementPCSCommunicationByFilterAsync);
        string exchangeUri = _caseManagementExchangeUri;
        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    #region Communication Review
    public async Task<bool> SaveCommunicationReviewAsync(SaveCommunicationReviewRequestModel request)
    {
        string remarks = "Save Communication Review";
        string eventName = Convert.ToString(Actions.SaveCommunicationReviewAsync);
        string exchangeUri = _caseManagementExchangeUri;
        string moduleService = (Constants.Services.CaseManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, moduleService);
    }
    public async Task<bool> GetCommunicationHistoryByReviewIdAsync(CommunicationReviewRequestModel request)
    {
        string remarks = "Get Communication Review history ";
        string eventName = Convert.ToString(Actions.GetCommunicationHistoryByReviewIdAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetCommunicationReviewHistoryListAsync(CommunicationReviewHistoryRequestModel request)
    {
        string remarks = "Get Communication Review History";
        string eventName = Convert.ToString(Actions.GetCommunicationReviewHistoryListAsync);
        string exchangeUri = _caseManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    #endregion



    #region EngagemetHub
    public async Task<bool> GetBotAutoReplyListByFilterAsync(BotAutoReplyFilterRequestModel request)
    {
        string remarks = "Get Bot Auto Reply List";
        string eventName = Convert.ToString(Actions.GetBotAutoReplyByFilterAsync);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetBotByIdAsync(BotFilterRequestModel request)
    {
        string remarks = "Get Bot By Id List";
        string eventName = Convert.ToString(Actions.GetBotDetailByIdAsync);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpSertBotDetailsAsync(BotDetailsRequestModel request)
    {
        string remarks = "Upsert Bot Detail";
        string eventName = Convert.ToString(Actions.UpSertBotDetailsAsync);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> UpSertBotDetailsAutoReplyAsync(BotDetailsAutoReplyRequestModel request)
    {
        string remarks = "Upsert Bot Detail Auto Reply";
        string eventName = Convert.ToString(Actions.UpSertBotDetailAutoReplyAsync);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetBotDetailListResultByFilterAsync(BotDetailFilterRequestModel request)
    {
        string remarks = "Get Bot Detail List";
        string eventName = Convert.ToString(Actions.GetBotDetailListResultByFilterAsync);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetBroadcastListByFilter(BroadcastFilterRequestModel request)
    {
        string remarks = "Get Broadcast List By Filter";
        string eventName = Convert.ToString(Actions.GetBroadcastListByFilter);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }



    public async Task<bool> UpsertBroadcastConfigurationAsync(BroadcastConfigurationRequest request)
    {
        string remarks = "Upsert Broadcast Configuration ";
        string eventName = Convert.ToString(Actions.UpsertBroadcastConfiguration);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> GetBroadcastConfigurationByIdAsync(GetBroadcastConfigurationByIdRequest request)
    {
        string remarks = "Get BroadcastConfiguration By Id";
        string eventName = Convert.ToString(Actions.GetBroadcastConfigurationById);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);

    }


    public async Task<bool> GetBroadcastConfigurationRecipientsStatusProgressById(GetBroadcastConfigurationByIdRequest request)
    {
        string remarks = "Get GetBroadcastConfigurationRecipientsStatusProgress By Id";
        string eventName = Convert.ToString(Actions.GetBroadcastConfigurationRecipientsStatusProgressById);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    #endregion

    #region Ticket Management
    public async Task<bool> GetTicketFieldMappingByTicketTypeAsync(FieldMappingRequestModel request)
    {
        string remarks = "Get Field Mapping By Ticket Types";
        string eventName = Convert.ToString(Actions.GetTicketFieldMappingByTicketTypeAsync);
        string exchangeUri = _ticketManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }


    public async Task<bool> SaveTicketDetailsAsync(SaveTicketDetailsRequestModel request)
    {
        string remarks = "Save Ticket Details";
        string eventName = Convert.ToString(Actions.SaveTicketDetailsAsync);
        string exchangeUri = _ticketManagementExchangeUri;
        string moduleService = (Constants.Services.TicketManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, moduleService);
    }

    public async Task<bool> DeleteTicketAttachmentByIdAsync(DeleteAttachmentRequestModel request)
    {
        string remarks = "Delete Ticket Attachment";
        string eventName = Convert.ToString(Actions.DeleteTicketAttachmentByIdAsync);
        string exchangeUri = _ticketManagementExchangeUri;
        string moduleService = (Constants.Services.TicketManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, moduleService);
    }
    public async Task<bool> GetTicketCommentByTicketCommentId(GetTicketCommentRequestModel request)
    {
        string remarks = "Get Ticket Comments by Ticket Comment Id";
        string eventName = Convert.ToString(Actions.GetTicketCommentByTicketCommentId);
        string exchangeUri = _ticketManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> UpsertTicketComment(UpsertTicketCommentRequestModel request)
    {
        string remarks = "Upsert Ticket Comments";
        string eventName = Convert.ToString(Actions.UpsertTicketComment);
        string exchangeUri = _ticketManagementExchangeUri;
        string moduleService = (Constants.Services.TicketManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, moduleService);
    }
    public async Task<bool> DeleteTicketCommentByTicketCommentId(DeleteTicketCommentRequestModel request)
    {
        string remarks = "Delete Ticket Comments";
        string eventName = Convert.ToString(Actions.DeleteTicketComment);
        string exchangeUri = _ticketManagementExchangeUri;
        string moduleService = (Constants.Services.TicketManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, moduleService);
    }
    public async Task<bool> UpsertPopupTicketDetailsAsync(UpsertPopupTicketDetailsRequestModel request)
    {
        string remarks = "Upsert Popup Ticket Details";
        string eventName = Convert.ToString(Actions.UpsertPopupTicketDetailsAsync);
        string exchangeUri = _ticketManagementExchangeUri;
        string moduleService = (Constants.Services.TicketManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, moduleService);
    }
    public async Task<bool> GetSearchTicketByFilters(SearchTicketFilterRequestModel request)
    {
        string remarks = "Search Ticket By Filters";
        string eventName = Convert.ToString(Actions.GetSearchTicketByFilters);
        string exchangeUri = _ticketManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> UpsertSearchTicketFilter(UpsertSearchTicketFilterRequestModel request)
    {
        string remarks = "Upsert Search Ticket Filters";
        string eventName = Convert.ToString(Actions.UpsertSearchTicketFilter);
        string exchangeUri = _ticketManagementExchangeUri;
        string moduleService = (Constants.Services.TicketManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri, moduleService);
    }

    public async Task<bool> GetTicketStatusPopupMappingAsync(GetTicketStatusPopupMappingRequestModel request)
    {
        string remarks = "Get Ticket Status Popup Mapping";
        string eventName = Convert.ToString(Actions.GetTicketStatusPopupMappingAsync);
        string exchangeUri = _ticketManagementExchangeUri;
        string moduleService = (Constants.Services.TicketManagementService).ToString();

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri , moduleService);
    }
    public async Task<bool> GetTicketHistory(HistoryCollaboratorGridRequestModel request)
    {
        string remarks = "Get Ticket History";
        string eventName = Convert.ToString(Actions.GetTicketHistory);
        string exchangeUri = _ticketManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> GetCollaboratorGridList(HistoryCollaboratorGridRequestModel request)
    {
        string remarks = "Get Collaborator Grid List";
        string eventName = Convert.ToString(Actions.GetCollaboratorGridList);
        string exchangeUri = _ticketManagementExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    #endregion

    #region Search Leads
    public async Task<bool> GetLeadsByFilterAsync(SearchLeadsRequestModel request)
    {
        string remarks = "Get Search Leads";
        string eventName = Convert.ToString(Actions.GetLeadsByFilterAsync);
        string exchangeUri = _engagementHubExchageUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    #endregion

    #region Staff Performance
    //ReviewPeriod
    public async Task<bool> GetCommunicationReviewPeriodsByFilterAsync(ReviewPeriodRequestModel request)
    {
        string remarks = "Get System Review Periods";
        string eventName = Convert.ToString(Actions.GetCommunicationReviewPeriodsByFilterAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);        
    }
    #endregion

    public async Task<bool> GetEventSubscriptionAsync(EventSubscriptionFilterRequestModel request)
    {
        string remarks = "Get Event Subscription";
        string eventName = Convert.ToString(Actions.GetEventSubscriptionAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
    public async Task<bool> UpdateEventSubscriptionAsync(EventSubscriptionRequestModel request)
    {
        string remarks = "Update Event Subscription";
        string eventName = Convert.ToString(Actions.UpdateEventSubscriptionAsync);
        string exchangeUri = _systemExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }
}

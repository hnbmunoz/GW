using MediatR;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class AgentWorkspaceService : IAgentWorkspaceService
{
    private readonly IMediator _mediator;
    private readonly ILogger<AgentWorkspaceService> _logger;
    private readonly IAgentWorkspaceFactory _agentWorkspaceFactory;
    private readonly IMessagePublisherService _messagePublisherService;
    public AgentWorkspaceService(IMediator mediator, ILogger<AgentWorkspaceService> logger, IAgentWorkspaceFactory agentWorkspaceFactory, IMessagePublisherService messagePublisherService)
    {
        _mediator = mediator;
        _logger = logger;
        _agentWorkspaceFactory = agentWorkspaceFactory;
        _messagePublisherService = messagePublisherService;
    }

    public async Task<CampaignPlayerFilterResponseModel> GetCampaignPlayerListByFilterAsync(CampaignPlayerFilterRequestModel request)
    {
        var results = await _agentWorkspaceFactory.GetCampaignPlayerListByFilterAsync(request);
        return results;
    }


    public async Task<CallListNoteResponseModel> GetCallListNoteAsync(int callListNoteId)
    {
        var results = await _agentWorkspaceFactory.GetCallListNote(callListNoteId);
        return results;
    }

    public async Task<bool> SaveCallListNote(CallListNoteRequestModel request)
    {
        var results = await _agentWorkspaceFactory.SaveCallListNotes(request);

        return results;
    }

    public async Task<bool> TaggingAgentAsync(TagAgentRequestModel request)
    {
        var results = await _agentWorkspaceFactory.TagAgentAsync(request);

        return results;
    }
    public async Task<bool> DiscardAgentPlayerAsync(DiscardAgentRequestModel request)
    {
        var results = await _agentWorkspaceFactory.DiscardPlayerAsync(request);

        return results;
    }

    public async Task<List<LookupModel>> GetAllCampaignListAsync(int campaignType)
    {
        var results = await _agentWorkspaceFactory.GetAllCampaignAsync(campaignType);
        return results;
    }

    public async Task<List<LookupModel>> GetCampaignAgentListAsync(int campaignId)
    {
        var results = await _agentWorkspaceFactory.GetCampaignAgentsAsync(campaignId);
        return results;
    }

    public async Task<List<MessageStatusResponseModel>> GetMessageStatusListAsync()
    {
        var results = await _agentWorkspaceFactory.GetMessageStatusResponseListAsync();
        return results;
    }

    public async Task<bool> ValidateTagAsync(ValidateTagAgentRequestModel request)
    {
        return await _agentWorkspaceFactory.ValidateTaggingAsync(request);
    }

    public async Task<List<PlayerDepositAttemptsResponseModel>> GetPlayerDepositAttemptListAsync(int campaignPlayerId)
    {
        return await _agentWorkspaceFactory.GetPlayerDepositAttemptsAsync(campaignPlayerId);
    }

    public async Task<ServiceCommunicationHistoryResponseModel> GetCommunicationHistoryByFilter(ServiceCommunicationHistoryFilterRequestModel request)
    {
        return await _agentWorkspaceFactory.USP_GetCommunicationHistoryByFilter(request);
    }
}

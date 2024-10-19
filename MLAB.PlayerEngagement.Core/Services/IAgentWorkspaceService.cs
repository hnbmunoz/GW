using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IAgentWorkspaceService
{
    Task<CampaignPlayerFilterResponseModel> GetCampaignPlayerListByFilterAsync(CampaignPlayerFilterRequestModel request);
    Task<bool> SaveCallListNote(CallListNoteRequestModel request);
    Task<CallListNoteResponseModel> GetCallListNoteAsync(int callListNoteId);
    Task<bool> TaggingAgentAsync(TagAgentRequestModel request);
    Task<bool> DiscardAgentPlayerAsync(DiscardAgentRequestModel request);
    Task<List<LookupModel>> GetAllCampaignListAsync(int campaignType);
    Task<List<LookupModel>> GetCampaignAgentListAsync(int campaignId);
    Task<List<MessageStatusResponseModel>> GetMessageStatusListAsync();
    Task<bool> ValidateTagAsync(ValidateTagAgentRequestModel request);
    Task<List<PlayerDepositAttemptsResponseModel>> GetPlayerDepositAttemptListAsync(int campaignPlayerId);
    Task<ServiceCommunicationHistoryResponseModel> GetCommunicationHistoryByFilter(ServiceCommunicationHistoryFilterRequestModel request);
}

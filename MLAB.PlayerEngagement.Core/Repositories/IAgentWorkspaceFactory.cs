using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface IAgentWorkspaceFactory
{
    Task<CampaignPlayerFilterResponseModel> GetCampaignPlayerListByFilterAsync(CampaignPlayerFilterRequestModel request);
    Task<bool> SaveCallListNotes(CallListNoteRequestModel request);
    Task<CallListNoteResponseModel> GetCallListNote(int callListNoteId);
    Task<bool> TagAgentAsync(TagAgentRequestModel request);
    Task<bool> DiscardPlayerAsync(DiscardAgentRequestModel request);
    Task<List<LookupModel>> GetAllCampaignAsync(int campaignType);
    Task<List<LookupModel>> GetCampaignAgentsAsync(int campaignId);
    Task<List<MessageStatusResponseModel>> GetMessageStatusResponseListAsync();
    Task<bool> ValidateTaggingAsync(ValidateTagAgentRequestModel request);
    Task<List<PlayerDepositAttemptsResponseModel>> GetPlayerDepositAttemptsAsync(int campaignPlayerId);
    Task<ServiceCommunicationHistoryResponseModel> USP_GetCommunicationHistoryByFilter(ServiceCommunicationHistoryFilterRequestModel request);
  
}

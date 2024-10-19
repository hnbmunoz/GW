using MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

namespace MLAB.PlayerEngagement.Core.Services;

public  interface IAgentMonitoringService
{
    Task<List<AutoTaggedNameListResponseModel>> GetAutoTaggingNameListAsync(int campaignId);
    Task<AgentListResponseModel> GetCampaignAgentList(AgentListRequestModel request);
}

using MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

namespace MLAB.PlayerEngagement.Core.Repositories;

public  interface IAgentMonitoringFactory
{
    Task<List<AutoTaggedNameListResponseModel>> GetAutoTaggingNameListAsync(int campaignId);
    Task<Tuple<List<AgentResponseModel>, List<DailyReportResponseModel>, List<int>>> GetCampaignAgentList(AgentListRequestModel request);
}



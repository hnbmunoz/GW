using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Models.AgentMonitoring;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Logging;

namespace MLAB.PlayerEngagement.Application.Services;

public class AgentMonitoringService : IAgentMonitoringService
{
    private readonly ILogger<PlayerManagementService> _logger;
    private readonly IAgentMonitoringFactory _agentMonitoringFactory;
    public AgentMonitoringService(ILogger<PlayerManagementService> logger, IAgentMonitoringFactory agentMonitoringFactory)
    {
        _logger = logger;
        _agentMonitoringFactory = agentMonitoringFactory;
    }

    public async Task<List<AutoTaggedNameListResponseModel>> GetAutoTaggingNameListAsync(int campaignId)
    {
        var results = await _agentMonitoringFactory.GetAutoTaggingNameListAsync(campaignId);

        return results;

    }

    public async Task<AgentListResponseModel> GetCampaignAgentList(AgentListRequestModel request)
    {
        var results = await _agentMonitoringFactory.GetCampaignAgentList(request);

        AgentListResponseModel agentList = new AgentListResponseModel();

        if (results.Item1.Any())
        {
            agentList.Agents = results.Item1;
            agentList.DailyReports = results.Item2;
            agentList.RecordCount = results.Item3.FirstOrDefault();
        }

        return agentList;

    }
}

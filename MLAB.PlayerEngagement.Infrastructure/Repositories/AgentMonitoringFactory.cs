using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.AgentMonitoring;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class AgentMonitoringFactory : IAgentMonitoringFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AgentMonitoringFactory> _logger;

    #region Constructor
    public AgentMonitoringFactory(IMainDbFactory mainDbFactory, IConfiguration configuration, ILogger<AgentMonitoringFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _configuration = configuration;
        _logger = logger;
    }
    #endregion

    public async Task<List<AutoTaggedNameListResponseModel>> GetAutoTaggingNameListAsync(int campaignId)
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<AutoTaggedNameListResponseModel>
                (
                    DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_GetAutoTaggingNameList,
                    new
                    {
                        CampaignId = campaignId
                    }
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentMonitoringFactory} | GetAutoTaggingNameListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<AutoTaggedNameListResponseModel>().ToList();
    }

    public async Task<Tuple<List<AgentResponseModel>, List<DailyReportResponseModel>, List<int>>>  GetCampaignAgentList(AgentListRequestModel request)
    {


        try
        {
            _logger.LogInfo($"{Services.CampaignTaggingPointSettingService} | GetCampaignAgentList - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<AgentResponseModel, DailyReportResponseModel,int>
                (
                    DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_GetCampaignAgentList, new
                    {
                        CampaignId = request.CampaignId,
                        AutoTaggedId = request.AutoTaggedId,
                        AgentName = request.AgentName,
                        PageSize = request.PageSize,
                        OffsetValue = request.OffsetValue,
                        SortColumn = request.SortColumn,
                        SortOrder = request.SortOrder,
                        AgentIds = request.AgentId
                    }

                ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.ToList(), result.Item2.ToList(), result.Item3.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentMonitoringFactory} | GetCampaignAgentList : [Exception] - {ex.Message}");

            return Tuple.Create(Enumerable.Empty<AgentResponseModel>().ToList(),
                Enumerable.Empty<DailyReportResponseModel>().ToList(),
                Enumerable.Empty<int>().ToList());
        }

    }
}

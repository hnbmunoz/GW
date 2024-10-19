using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models.CampaignPerformance;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class CampaignPerformanceService : ICampaignPerformanceService
{
    private readonly ILogger<CampaignPerformanceService> _logger;
    private readonly ICampaignPerformanceFactory _campaignPerformanceFactory;
    public CampaignPerformanceService(ILogger<CampaignPerformanceService> logger, ICampaignPerformanceFactory campaignPerformanceFactory)
    {
        _logger = logger;
        _campaignPerformanceFactory = campaignPerformanceFactory;
    }

    public async Task<CampaignPerformanceFilterResponseModel> GetCampaignPerformanceFilterAsync(int campaignTypeId)
    {
        var results = await _campaignPerformanceFactory.GetCampaignPerformanceFilterAsync(campaignTypeId);

        CampaignPerformanceFilterResponseModel performanceResult = new CampaignPerformanceFilterResponseModel();


        if (results.Item1.Any())
        {
            performanceResult.Campaigns = results.Item1;
            performanceResult.CampaignGoals = results.Item2;
        }

        return performanceResult;
    }

  
}

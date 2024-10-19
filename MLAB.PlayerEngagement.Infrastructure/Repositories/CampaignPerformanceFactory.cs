using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.CampaignPerformance;
using MLAB.PlayerEngagement.Core.Repositories;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CampaignPerformanceFactory : ICampaignPerformanceFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<CampaignPerformanceFactory> _logger;

    #region Constructor
    public CampaignPerformanceFactory(IMainDbFactory mainDbFactory, ILogger<CampaignPerformanceFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }
    #endregion

    public async Task<Tuple<List<CampaignActiveAndEndedResponseModel>, List<CampaignGoalResponseModel>>> GetCampaignPerformanceFilterAsync(int campaignTypeId)
    {

        try
        {
            _logger.LogInfo($"{Factories.CampaignPerformanceFactory} | GetCampaignPerformanceFilterAsync ");

            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<CampaignActiveAndEndedResponseModel, CampaignGoalResponseModel>
                (
                    DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_GetCampaignActiveEnded, new
                    {
                        CampaignTypeId = campaignTypeId,    
                    }

                ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.ToList(), result.Item2.ToList() );
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignPerformanceFactory} | GetCampaignPerformanceFilterAsync : [Exception] - {ex.Message}");

            return Tuple.Create(Enumerable.Empty<CampaignActiveAndEndedResponseModel>().ToList(),
                Enumerable.Empty<CampaignGoalResponseModel>().ToList());
        }
    }

}

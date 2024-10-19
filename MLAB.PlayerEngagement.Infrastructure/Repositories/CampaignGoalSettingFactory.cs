using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CampaignGoalSettingFactory : ICampaignGoalSettingFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<CampaignGoalSettingFactory> _logger;

    public CampaignGoalSettingFactory(IMainDbFactory mainDbFactory, ILogger<CampaignGoalSettingFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }

    public async Task<bool> CheckCampaignGoalSettingByNameExistAsync(CampaignGoalSettingNameRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignGoalSettingFactory} | CheckCampaignGoalSettingByNameExistAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                .ExecuteQueryAsync<long>
                (
                    DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_CheckCampaignGoalSettingByNameExist, new
                    {
                        CampaignSettingId = request.CampaignSettingId,
                        CampaignSettingName = request.CampaignSettingName
                    }

                ).ConfigureAwait(false);
            return result.FirstOrDefault() == 1;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignGoalSettingFactory} | CheckCampaignGoalSettingByNameExistAsync : [Exception] - {ex.Message}");
        }

        return false;
    }
}

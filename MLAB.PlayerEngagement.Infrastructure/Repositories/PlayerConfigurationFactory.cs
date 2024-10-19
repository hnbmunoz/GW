using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class PlayerConfigurationFactory : IPlayerConfigurationFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<PlayerConfigurationFactory> _logger;

    public PlayerConfigurationFactory(IMainDbFactory mainDbFactory, ILogger<PlayerConfigurationFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }

    public async Task<int> ValidateVIPLevelNameAsync(string VIPLevelName)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerConfigurationFactory} | ValidateVIPLevelNameAsync - [VIPLevelName: {VIPLevelName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            ( DatabaseFactories.MLabDB,
                                StoredProcedures.USP_GetVIPLevelId,
                                 new
                                 {
                                     FilterValue = VIPLevelName
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerConfigurationFactory} | ValidateVIPLevelNameAsync : [Exception] - {ex.Message}");
        }
        return 1;
    }


    public async Task<bool> CheckExistingIDNameCodeListAsync(PlayerConfigCodeListValidatorRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerConfigurationFactory} | CheckExistingIDNameCodeListAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<int>
                (   DatabaseFactories.MLabDB,
                    StoredProcedures.USP_CheckExistingIDNameCodeList, new
                    {
                        PlayerConfigurationTypeId = request.PlayerConfigurationId,
                        PlayerConfigurationId = request.PlayerConfigurationId,
                        PlayerConfigurationName = request.PlayerConfigurationName,
                        PlayerConfigurationCode = request.PlayerConfigurationCode,
                        PlayerConfigurationICoreId = request.PlayerConfigurationICoreId,
                        PlayerConfigurationAction = request.PlayerConfigurationAction,
                        PlayerConfigurationBrandId = request.PlayerConfigurationBrandId,
                    }

                ).ConfigureAwait(false);

            return result == 1 ? true : false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerConfigurationFactory} | CheckExistingIDNameCodeListAsync : [Exception] - {ex.Message}");

            return false;
        }

    }

    public async Task<bool> ValidatePlayerConfigurationRecordAsync(PlayerConfigurationRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerConfigurationFactory} | ValidatePlayerConfigurationRecordAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (   DatabaseFactories.MLabDB,
                                StoredProcedures.USP_CheckExistingIDNameCodeList,
                                 new
                                 {
                                     PlayerConfigurationTypeId = request.PlayerConfigurationTypeId,
                                     PlayerConfigurationId = request.PlayerConfigurationId,
                                     PlayerConfigurationName = request.PlayerConfigurationName,
                                     PlayerConfigurationCode = request.PlayerConfigurationCode,
                                     PlayerConfigurationICoreId = request.PlayerConfigurationICoreId,
                                     PlayerConfigurationAction = request.PlayerConfigurationAction
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerConfigurationFactory} | ValidatePlayerConfigurationRecordAsync : [Exception] - {ex.Message}");
        }
        return false;

    }
    public async Task<List<TicketFieldsModel>> GetTicketFieldsList()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<TicketFieldsModel>(DatabaseFactories.TicketManagementDb,
                StoredProcedures.USP_GetTicketFields, null
            ).ConfigureAwait(false);

            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetTicketFieldsList : [Exception] - {ex.Message}");
            return Enumerable.Empty<TicketFieldsModel>().ToList();
        }
    }
    public async Task<List<LanguageModel>> GetLanguageOptionList()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<LanguageModel>(DatabaseFactories.MLabDB,
                StoredProcedures.USP_GetLanguageDetails, null
            ).ConfigureAwait(false);

            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetLanguageOptionList : [Exception] - {ex.Message}");
            return Enumerable.Empty<LanguageModel>().ToList();
        }
    }
}

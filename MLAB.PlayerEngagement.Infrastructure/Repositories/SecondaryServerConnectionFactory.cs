using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;
using MLAB.PlayerEngagement.Core.Models.AppConfigSettings;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class SecondaryServerConnectionFactory : ISecondaryServerConnectionFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<SecondaryServerConnectionFactory> _logger;

    public SecondaryServerConnectionFactory(IMainDbFactory mainDbFactory, ILogger<SecondaryServerConnectionFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }

    //check what DB to use
    private async Task<bool> IsSecondaryDbEnabledAsync(string appConfigSettingKey)
    {
        try
        {
            _logger.LogInfo($"Gateway | IsSecondaryDbEnabledAsync - request: {appConfigSettingKey}");
            var appConfigSettingFilterResult = await _mainDbFactory.ExecuteQueryMultipleAsync<AppConfigSettingResponseModel, int>(
                                                DatabaseFactories.MLabDB,
                                                StoredProcedures.Usp_GetAppConfigSettingByFilter,
                                                new
                                                {
                                                    ApplicationId = 383
                                                }).ConfigureAwait(false);

            return appConfigSettingFilterResult?.Item1?.Any(setting => setting.Key.ToString() == appConfigSettingKey && setting.Value == "false") ?? false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(SecondaryServerConnectionFactory)} | {nameof(IsSecondaryDbEnabledAsync)} : [Exception] - {ex.Message}");
            throw;
        }
    }

    private DatabaseFactories GetSecondaryServerValue(DatabaseFactories primaryDB)
    {
        switch (primaryDB)
        {
            case DatabaseFactories.PlayerManagementDB:
                return DatabaseFactories.PlayerManagementDBSecondary;

            case DatabaseFactories.MLabDB:
                return DatabaseFactories.MLabDBSecondary;


            default:
                throw new ArgumentOutOfRangeException(nameof(primaryDB), "Invalid module name and server specified.");
        }
    }

    public async Task<DatabaseFactories> GetDatabaseToUseAsync(string appConfigSettingKey, DatabaseFactories primaryDb)
    {
        try
        {

            _logger.LogInfo($"Gateway | GetDatabaseToUseAsync - request: {appConfigSettingKey},{primaryDb}");

            var isSecondaryDbEnabled = await IsSecondaryDbEnabledAsync(appConfigSettingKey);
            return isSecondaryDbEnabled ? GetSecondaryServerValue(primaryDb) : primaryDb;
        }
        catch (Exception ex)
        {
            _logger.LogInfo($"Gateway | GetDatabaseToUseAsync - Error found: {ex.Message}");
            throw;
        }
    }
}

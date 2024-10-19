using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Models.Player.Response;
using MLAB.PlayerEngagement.Core.Models.Player.Request;
using System.Globalization;
using MLAB.PlayerEngagement.Infrastructure.Utilities;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class PlayerFactor : IPlayerFactor
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PlayerFactor> _logger;
    private readonly ISecondaryServerConnectionFactory _secondaryServerConnectionFactory;

    #region Constructor
    public PlayerFactor(IMainDbFactory mainDbFactory, IConfiguration configuration, ILogger<PlayerFactor> logger, ISecondaryServerConnectionFactory secondaryServerConnectionFactory)
    {
        _mainDbFactory = mainDbFactory;
        _configuration = configuration;
        _logger = logger;
        _secondaryServerConnectionFactory = secondaryServerConnectionFactory;   
    }
    #endregion


    public async Task<PlayerResponseModel> GetPlayerAsync(PlayerByIdRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | GetPlayerAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<PlayerResponseModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetPlayerById, new
                                {
                                    PlayerId = request.PlayerId,
                                    HasAccess = request.HasAccess,
                                    PageSource = request.PageSource,
                                    UserId = request.UserId,
                                    BrandName = request.BrandName
                                }

                            ).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetPlayerAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<PlayerResponseModel>().FirstOrDefault();
    }

    public async Task<PlayerFilterResponseModel> GetPlayersAsync(PlayerFilterRequestModel request)
    {
        try
        {
            var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("PlayerSearchPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

            _logger.LogInfo($"{Factories.PlayerFactor} | GetPlayersAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.PlayerFactor} | GetPlayersAsync | ConnectionString - [{connectionString}]");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<int, PlayerResponseModel>
                            (
                                //DatabaseFactories.PlayerManagementDB,
                                connectionString,
                                StoredProcedures.USP_GetPlayerList, new
                                {
                                    StartDate = string.IsNullOrEmpty(request.StartDate) ? null : request.StartDate.ToLocalDateTime(),
                                    EndDate = string.IsNullOrEmpty(request.EndDate) ? null : request.EndDate.ToLocalDateTime(),
                                    Brands = request.Brands.Count == 0? null : String.Join("|", request.Brands.Select(x=>x.Value)),
                                    StatusId = request.StatusId,
                                    InternalAccount = request.InternalAccount,
                                    PlayerId = request.PlayerId,
                                    CurrencyId = request.CurrencyId,
                                    MarketingChannels = request.MarketingChannels.Count == 0 ? null : String.Join("|", request.MarketingChannels.Select(x => x.Value)),
                                    RiskLevels = request.RiskLevels.Count == 0 ? null : String.Join("|", request.RiskLevels.Select(x => x.Value)),
                                    Username = string.IsNullOrEmpty(request.Username) ? null : request.Username,
                                    VIPLevels = request.VIPLevels.Count == 0 ? null : String.Join("|", request.VIPLevels.Select(x => x.Value)),
                                    MarketingSource = string.IsNullOrEmpty(request.MarketingSource) ? null : request.MarketingSource,
                                    PageSize = request.PageSize,
                                    OffsetValue = request.OffsetValue,
                                    SortColumn = string.IsNullOrEmpty(request.SortColumn)? "RegistrationDate" : request.SortColumn,
                                    SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "ASC" : request.SortOrder,
                                    UserId = request.UserId
                                }

                            ).ConfigureAwait(false);
            return new PlayerFilterResponseModel
            {
                RecordCount = result.Item1.First(),
                Players = result.Item2.ToList()                    
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetPlayersAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<PlayerFilterResponseModel>().First();
    }

    public async Task<PlayerCaseFilterResponseModel> GetPlayerCasesAsync(PlayerCaseRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | GetPlayerCasesAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                       .ExecuteQueryMultipleAsync<int, PlayerCaseResponseModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetPlayerCaseList, new
                                {
                                    MlabPlayerId = request.MlabPlayerId,
                                    CreatedDate = string.IsNullOrEmpty(request.CreatedDate) ? null : request.CreatedDate,
                                    CaseId = request.CaseId,
                                    CommunicationId = request.CommunicationId,
                                    CaseStatusId = request.CaseStatus,
                                    MessageTypeId = request.MessageTypeId,
                                    MessageStatusId = request.MessageStatusId,
                                    MessageResponseId = request.MessageResponseId,
                                    CampaignNameId = request.CampaignNameId,
                                    CreatedBy = request.CreatedBy, 
                                    PageSize = request.PageSize,
                                    OffsetValue = request.OffsetValue,
                                    SortColumn = string.IsNullOrEmpty(request.SortColumn) ? "CaseId" : request.SortColumn,
                                    SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "ASC" : request.SortOrder,
                                    CaseTypeId = request.CaseTypeId,
                                    CampaignTypeId = request.CampaignTypeId
                                }

                            ).ConfigureAwait(false);

            return new PlayerCaseFilterResponseModel
            {
                RecordCount = result.Item1.First(),
                PlayerCases = result.Item2.ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetPlayerCasesAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<PlayerCaseFilterResponseModel>().First();
    }

    public async Task<Tuple<int, ContactLogThresholdModel>> SavePlayerContactAsync(PlayerContactRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | SavePlayerContactAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<int, ContactLogThresholdModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_AddPlayerContact, new
                                {
                                    MlabPlayerId = request.MlabPlayerId,
                                    UserId = request.UserId,
                                    ContactTypeId = request.ContactTypeId,
                                    PageName = request.PageName
                                }

                            ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.FirstOrDefault(), result.Item2.FirstOrDefault());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | SavePlayerContactAsync : [Exception] - {ex.Message}");
        }

        return Tuple.Create(0, Enumerable.Empty<ContactLogThresholdModel>().FirstOrDefault());
    }

    public async Task<List<LookupModel>> GetPlayerCampaignLookupsAsync(int? campaignType)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LookupModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaign, new { CampaignType = campaignType }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetPlayerCampaignLookupsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<int> ValidateCaseCampaignPlayerAsync(CaseCampaigndPlayerIdRequest request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | ValidateCaseCampaignPlayerAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_ValidateCaseCampaignId,
                                 new
                                 {
                                     PlayerId = request.PlayerId,
                                     CampaignId = request.CampaignId,
                                     BrandName = request.BrandName
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | ValidateCaseCampaignPlayerAsync : [Exception] - {ex.Message}");
        }

        return 0;
    }

    public async Task<List<GetManageThresholdsResponse>> GetManageThresholdsAsync()
    {

        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<GetManageThresholdsResponse>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetManageThresholds, null
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetManageThresholdsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<GetManageThresholdsResponse>().ToList();
    }

    public async Task<PlayerSensitiveDataResponseModel> GetPlayerSensitiveDataAsync(PlayerSensitiveDataRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | GetPlayerSensitiveData - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<PlayerSensitiveDataResponseModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetPlayerSensitiveData, new
                                {
                                    UserId = request.UserId,
                                    HasAccess = request.HasAccess,
                                    MlabPlayerId = request.MlabPlayerId,
                                    SensitiveField = request.SensitiveField
                                }

                            ).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetPlayerSensitiveData : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<PlayerSensitiveDataResponseModel>().FirstOrDefault();
    }

    public async Task<Tuple<int, List<T>>> GetViewContactLogAsync<T>(ContactLogListRequestModel request, int srcType)
    {
        try
        {
            var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("PlayerSearchPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

            _logger.LogInfo($"{Factories.PlayerFactor} | GetViewContactLogListAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.PlayerFactor} | GetViewContactLogListAsync | Start Time : {DateTime.Now}");
            _logger.LogInfo($"{Factories.PlayerFactor} | GetViewContactLogListAsync | DBConnectionString - [{connectionString}]");

            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<int, T>(connectionString,
                    StoredProcedures.USP_ViewContactLogs,
                    new
                    {
                        DateFrom = DateUtils.ConvertToUniversalTime(request.ActionDateFrom, "yyyy-MM-dd HH:mm"),
                        DateTo = DateUtils.ConvertToUniversalTime(request.ActionDateTo, "yyyy-MM-dd HH:mm"),
                        TeamIds = string.IsNullOrEmpty(request.TeamIds) ? null : request.TeamIds,
                        UserIds = string.IsNullOrEmpty(request.UserIds) ? null : request.UserIds,
                        srcType = srcType, // Summary or Team Summary or Log User Summary,
                        Pagesize = request.PageSize,
                        OffsetValue = request.OffsetValue,
                        SortOrder = request.SortOrder,
                        SortColumn = request.SortColumn
                    }
                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.PlayerFactor} | GetViewContactLogListAsync | End Time : {DateTime.Now}");

            return Tuple.Create(result.Item1.FirstOrDefault(), result.Item2.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetViewContactLogListAsync : [Exception] - {ex.Message}");
        }

        return Tuple.Create(0, Enumerable.Empty<T>().ToList());
    }
}

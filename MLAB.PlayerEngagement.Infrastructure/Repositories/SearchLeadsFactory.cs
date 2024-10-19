using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.SearchLeads;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class SearchLeadsFactory : ISearchLeadsFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<SearchLeadsFactory> _logger;

    public SearchLeadsFactory(IMainDbFactory mainDbFactory, ILogger<SearchLeadsFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }

    public async Task<LinkPlayerDetailsModel> GetLeadLinkDetailsByIdAsync(long mlabPlayerId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SearchLeadsFactory} | GetLeadLinkDetailsByIdAsync - [GetLeadLinkDetailsByIdAsync: {mlabPlayerId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<LinkPlayerDetailsModel>
                            (DatabaseFactories.IntegrationDb,
                                StoredProcedures.Usp_GetLeadLinkDetailsById,
                                 new
                                 {
                                     MlabPlayerid = mlabPlayerId,
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SearchLeadsFactory} | GetLeadLinkDetailsByIdAsync : [Exception] - {ex.Message}");
        }
        return new LinkPlayerDetailsModel();


       
    }

    public async Task<bool> LinkUnlinkPlayerAsync(long leadId, long linkedMlabPlayerId, long userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SearchLeadsFactory} | LinkUnlinkPlayerAsync - [LinkUnlinkPlayerAsync: Lead Id = {leadId} MlabPlayerId = {linkedMlabPlayerId} UserId = {userId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (DatabaseFactories.IntegrationDb,
                                StoredProcedures.Usp_LinkUnlinkPlayer,
                                 new
                                 {
                                     LeadId = leadId,
                                     LinkMlabPlayerId = linkedMlabPlayerId,   
                                     UserId = userId,
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SearchLeadsFactory} | LinkUnlinkPlayerAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<bool> RemoveLeadAsync(long leadId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SearchLeadsFactory} | RemoveLeadAsync - [LinkUnlinkPlayerAsync: {leadId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (DatabaseFactories.IntegrationDb,
                                StoredProcedures.Usp_RemoveLead,
                                 new
                                 {
                                     LeadId = leadId,
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SearchLeadsFactory} | RemoveLeadAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<List<AllSourceBOTModel>> GetAllSourceBOTAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.SearchLeadsFactory} | GetAllSourceBOTAsync");

            var result = await _mainDbFactory
                            .ExecuteQueryAsync<AllSourceBOTModel>
                                (DatabaseFactories.IntegrationDb,
                                    StoredProcedures.USP_GetAllSourceBOT, new
                                    {
                                       
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SearchLeadsFactory} | GetAllSourceBOTAsync : [Response] - {result.ToList().Count()}");
            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SearchLeadsFactory} | GetAllSourceBOTAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<AllSourceBOTModel>().ToList();
    }

    public async Task<List<LeadPlayerByUsernameResponse>> GetLeadPlayersByUsernameAsync(string username, long userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SearchLeadsFactory} | GetLeadPlayersByUsernameAsync ");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LeadPlayerByUsernameResponse>
                            (
                                DatabaseFactories.IntegrationDb,
                                StoredProcedures.USP_GetLeadPlayersByUsername, new
                                {
                                    Username = username,
                                    UserId = userId
                                }

                            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SearchLeadsFactory} | GetLeadPlayersByUsernameAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LeadPlayerByUsernameResponse>().ToList();
    }

    public async Task<List<LeadSelectedResultResponseModel>> GetLeadSelectionByFilterAsync(SearchLeadsRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SearchLeadsFactory} | GetLeadSelectionByFilterAsync | Param={JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LeadSelectedResultResponseModel>
                            (
                                DatabaseFactories.IntegrationDb,
                                 StoredProcedures.Usp_GetLeadSelectionByFilter
                                 , new
                                 {
                                     LeadName = request.LeadName,
                                     LinkedPlayerUsername = request.LinkedPlayerUsername,
                                     StageIDs = request.StageIDs,
                                     SourceId = request.SourceId,
                                     BrandIDs = request.BrandIDs,
                                     CurrencyIDs = request.CurrencyIDs,
                                     VIPLevelIDs = request.VIPLevelIDs,
                                     CountryIDs = request.CountryIDs,
                                     LeadIds = string.IsNullOrWhiteSpace(request.LeadIds)? null : request.LeadIds
                                 }

                            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SearchLeadsFactory} | GetLeadSelectionByFilterAsync : [Exception] - {ex.Message} | Param={{JsonConvert.SerializeObject(request)");
        }
        return Enumerable.Empty<LeadSelectedResultResponseModel>().ToList();
    }
}

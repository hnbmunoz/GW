using System.Data;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Models.SearchLeads;

namespace MLAB.PlayerEngagement.Application.Services;

public class SearchLeadsService : ISearchLeadsService
{
    private readonly ILogger<SearchLeadsService> _logger;
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ISearchLeadsFactory _searchLeadsFactory;

    public SearchLeadsService(ILogger<SearchLeadsService> logger, IMainDbFactory mainDbFactory, ISearchLeadsFactory searchLeadsFactory)
    {
        _logger = logger;
        _mainDbFactory = mainDbFactory;
        _searchLeadsFactory = searchLeadsFactory;  
    }

    public async Task<List<AllSourceBOTModel>> GetAllSourceBOTAsync()
    {
        return await _searchLeadsFactory.GetAllSourceBOTAsync();
    }

    public async Task<LinkPlayerDetailsModel> GetLeadLinkDetailsByIdAsync(long mlabPlayerId)
    {
        return await _searchLeadsFactory.GetLeadLinkDetailsByIdAsync(mlabPlayerId);
    }

    public async Task<bool> LinkUnlinkPlayerAsync(long leadId, long linkedMlabPlayerId, long userId)
    {
        return await _searchLeadsFactory.LinkUnlinkPlayerAsync(leadId, linkedMlabPlayerId, userId);
    }

    public async Task<bool> RemoveLeadAsync(long leadId)
    {
        return await _searchLeadsFactory.RemoveLeadAsync(leadId);
    }

    public async Task<List<LeadPlayerByUsernameResponse>> GetLeadPlayersByUsernameAsync(string username, long userId)
    {
        return await _searchLeadsFactory.GetLeadPlayersByUsernameAsync(username, userId);

    }

    public async Task<List<LeadSelectedResultResponseModel>> GetLeadSelectionByFilterAsync(SearchLeadsRequestModel request)
    {
        return await _searchLeadsFactory.GetLeadSelectionByFilterAsync(request);
    }
}

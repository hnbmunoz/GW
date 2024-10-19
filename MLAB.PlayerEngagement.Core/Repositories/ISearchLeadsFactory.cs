
using MLAB.PlayerEngagement.Core.Models.SearchLeads;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ISearchLeadsFactory
{
    Task<bool> LinkUnlinkPlayerAsync(long leadId, long linkedMlabPlayerId, long userId);
    Task<bool> RemoveLeadAsync(long leadId);
    Task<LinkPlayerDetailsModel> GetLeadLinkDetailsByIdAsync(long mlabPlayerId);

    public Task<List<AllSourceBOTModel>> GetAllSourceBOTAsync();

    Task<List<LeadPlayerByUsernameResponse>> GetLeadPlayersByUsernameAsync(string username, long userId);
    Task<List<LeadSelectedResultResponseModel>> GetLeadSelectionByFilterAsync(SearchLeadsRequestModel request);
}

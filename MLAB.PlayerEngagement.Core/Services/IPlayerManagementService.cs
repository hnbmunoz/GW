using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Player.Request;
using MLAB.PlayerEngagement.Core.Models.Player.Response;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IPlayerManagementService
{
    Task<List<LookupModel>> GetPlayerCampaignLookupsAsync(int? campaignType);
    Task<PlayerFilterResponseModel> GetPlayersAsync(PlayerFilterRequestModel request);
    Task<PlayerResponseModel> GetPlayerAsync(PlayerByIdRequestModel request);
    Task<Tuple<int,ContactLogThresholdModel>> SavePlayerContactAsync(PlayerContactRequestModel request);
    Task<PlayerCaseFilterResponseModel> GetPlayerCasesAsync(PlayerCaseRequestModel request);
    Task<Tuple<int, string>> ValidateCaseCampaignPlayerAsync(CaseCampaigndPlayerIdRequest request);
    Task<ContactLogSummaryResponseModel> GetViewContactLogListAsync(ContactLogListRequestModel request);
    Task<List<GetManageThresholdsResponse>> GetManageThresholdsAsync();
    Task<ContactLogTeamResponseModel> GetViewContactLogTeamListAsync(ContactLogListRequestModel request);
    Task<ContactLogUserResponseModel> GetViewContactLogUserListAsync(ContactLogListRequestModel request);
    Task<PlayerSensitiveDataResponseModel> GetPlayerSensitiveDataAsync(PlayerSensitiveDataRequestModel request);
}

using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Player.Request;
using MLAB.PlayerEngagement.Core.Models.Player.Response;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface IPlayerFactor
{
    Task<List<LookupModel>> GetPlayerCampaignLookupsAsync(int? campaignType);
    Task<PlayerFilterResponseModel> GetPlayersAsync(PlayerFilterRequestModel request);
    Task<PlayerResponseModel> GetPlayerAsync(PlayerByIdRequestModel request);
    Task<Tuple<int,ContactLogThresholdModel>> SavePlayerContactAsync(PlayerContactRequestModel request);
    Task<PlayerCaseFilterResponseModel> GetPlayerCasesAsync(PlayerCaseRequestModel request);
    Task<int> ValidateCaseCampaignPlayerAsync(CaseCampaigndPlayerIdRequest request);
    Task<List<GetManageThresholdsResponse>> GetManageThresholdsAsync();
    Task<PlayerSensitiveDataResponseModel> GetPlayerSensitiveDataAsync(PlayerSensitiveDataRequestModel request);

    Task<Tuple<int, List<T>>> GetViewContactLogAsync<T>(ContactLogListRequestModel request, int srcType);

}

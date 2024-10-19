using MLAB.PlayerEngagement.Core.Models.CallListValidation;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ICallListValidationService
{
    Task<CallValidationFilterResponseModel> GetCallListValidationFilterAsync(int campaignId);
    Task<CallValidationListResponseModel> GetCallValidationListAsync(CallValidationListRequestModel request);
    Task<List<LeaderJustificationListResponseModel>> GetLeaderJustificationListAsync();
}

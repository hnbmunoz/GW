using MLAB.PlayerEngagement.Core.Models.CallListValidation;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ICallListValidationFactory
{
    Task<CallValidationFilterResponseModel> GetCallListValidationFilterAsync(int campaignId);
    Task<CallValidationListResponseModel> GetCallValidationListAsync(CallValidationListRequestModel request);
    Task<List<LeaderJustificationListResponseModel>> GetLeaderJustificationListAsync();
}




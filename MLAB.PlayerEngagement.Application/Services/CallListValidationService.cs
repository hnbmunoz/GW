using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models.CallListValidation;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class CallListValidationService : ICallListValidationService
{
    private readonly ILogger<PlayerManagementService> _logger;
    private readonly ICallListValidationFactory _callListValidationFactory;
    public CallListValidationService(ILogger<PlayerManagementService> logger, ICallListValidationFactory callListValidationFactory)
    {
        _logger = logger;
        _callListValidationFactory = callListValidationFactory;
    }
    public async Task<CallValidationFilterResponseModel> GetCallListValidationFilterAsync(int campaignId)
    {
        var results = await _callListValidationFactory.GetCallListValidationFilterAsync(campaignId);
        CallValidationFilterResponseModel filters = new CallValidationFilterResponseModel();

        filters.CallCaseStatusOutcomes = results.CallCaseStatusOutcomes;
        filters.PlayerIds = results.PlayerIds;
        filters.AgentNames = results.AgentNames;
        filters.UserNames = results.UserNames;
        filters.Justifications = results.Justifications;

        return filters;
    }
    public async Task<CallValidationListResponseModel> GetCallValidationListAsync(CallValidationListRequestModel request)
    {
        var results = await _callListValidationFactory.GetCallValidationListAsync(request);
        CallValidationListResponseModel validationList = new CallValidationListResponseModel();

        validationList.CallValidations = results.CallValidations;
        validationList.AgentValidations = results.AgentValidations;
        validationList.LeaderValidations = results.LeaderValidations;
        validationList.CallEvaluations = results.CallEvaluations;
        validationList.RecordCount = results.RecordCount;

        return validationList;
    }
    public async Task<List<LeaderJustificationListResponseModel>> GetLeaderJustificationListAsync()
    {
        var results = await _callListValidationFactory.GetLeaderJustificationListAsync();
        return results;
    }
}

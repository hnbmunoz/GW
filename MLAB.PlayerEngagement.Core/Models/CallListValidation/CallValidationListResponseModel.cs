namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class CallValidationListResponseModel
{
    public List<CallValidationResponseModel> CallValidations { get; set; }
    public List<AgentValidationsResponseModel> AgentValidations { get; set; }
    public List<LeaderValidationsResponseModel> LeaderValidations { get; set; }
    public List<CallEvaluationResponseModel> CallEvaluations { get; set; }
    public int  RecordCount { get; set; }
}

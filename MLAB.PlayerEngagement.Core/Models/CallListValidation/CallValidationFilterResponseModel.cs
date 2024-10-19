namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class CallValidationFilterResponseModel
{
    public List<string> CallCaseStatusOutcomes { get; set; }
    public List<string> PlayerIds { get; set; }
    public List<AgentFilterResponseModel> AgentNames { get; set; }
    public List<string> UserNames { get; set; }
    public List<LeaderJustificationFilterResponseModel> Justifications { get; set; }
}

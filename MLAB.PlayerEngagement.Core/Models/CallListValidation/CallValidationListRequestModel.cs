namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class CallValidationListRequestModel
{
    public int? CampaignId { get; set; }
    public string PlayerId { get; set; }
    public string Username { get; set; }
    public string AgentName { get; set; }
    public string  Status { get; set; }
    public String Currency { get; set; }
    public DateTime? RegistrationStartDate { get; set; }
    public DateTime? RegistrationEndDate { get; set; }
    public string Deposited { get; set; }
    public int? FtdStartAmount { get; set; }
    public int? FtdEndAmount { get; set; }
    public DateTime? FtdStartDate { get; set; }
    public DateTime ?FtdEndDate { get; set; }
    public DateTime? TaggedStartDate { get; set; }
    public DateTime? TaggedEndDate { get; set; }
    public string  PrimaryGoalReached { get; set; }
    public int?  PrimaryGoalCount { get; set; }
    public int? PrimaryGoalAmount { get; set; }
    public string CallListNotes { get; set; }
    public string MobileNumber { get; set; }
    public string MessageStatusAndResponse { get; set; }
    public DateTime? CallCaseCreatedStartDate { get; set; }
    public DateTime? CallCaseCreatedEndDate { get; set; }
    public string SystemValidation { get; set; }
    public string AgentValidation { get; set; }
    public string LeaderValidation { get; set; }
    public string LeaderJustification { get; set; }
    public double? CallEvaluationStartPoint { get; set; }
    public double? CallEvaluationEndPoint { get; set; }
    public string CallEvaluationNotes { get; set; }
    public string HighDepositAmount { get; set; }
    public string AgentValidationNotes { get; set; }
    public string LeaderValidationNotes { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }

}

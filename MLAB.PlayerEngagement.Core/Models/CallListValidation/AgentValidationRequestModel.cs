namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class AgentValidationRequestModel : BaseModel
{
    public int AgentValidationId { get; set; }
    public bool AgentValidationStatus { get; set; }
    public string AgentValidationNotes { get; set; }
    public bool IsAgentValidated { get; set; }
    public int CampaignPlayerId { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

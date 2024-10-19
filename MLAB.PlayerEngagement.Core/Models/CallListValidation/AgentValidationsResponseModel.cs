namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public  class AgentValidationsResponseModel
{
    public int AgentValidationId { get; set; }
    public bool AgentValidationStatus { get; set; }
    public string AgentValidationNotes { get; set; }
    public bool IsAgentValidated { get; set; }
    public int CampaignPlayerId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}

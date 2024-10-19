namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class CallEvaluationRequestModel : BaseModel
{
    public int CallEvaluationId { get; set; }
    public double?  EvaluationPoint { get; set; }
    public string  EvaluationNotes { get; set; }
    public int CampaignPlayerId { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

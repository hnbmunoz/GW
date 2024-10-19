namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public  class CallEvaluationResponseModel
{
    public int CallEvaluationId { get; set; }
    public double? EvaluationPoint { get; set; }
    public string EvaluationNotes { get; set; }
    public int CampaignPlayerId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}

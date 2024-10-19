namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class LeaderValidationsRequestModel : BaseModel
{
    public int LeaderValidationId { get; set; }
    public bool LeaderValidationStatus { get; set; }
    public int JustificationId { get; set; }
    public string LeaderValidationNotes { get; set; }
    public int  HighDeposit { get; set; } 
    public bool IsLeaderValidated { get; set; }
    public int CampaignPlayerId { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

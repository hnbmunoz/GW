namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public  class LeaderJustificationRequestModel : BaseModel
{
    public int LeaderJustificationId { get; set; }
    public string Justification { get; set; }
    public bool Status { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class LeaderJustificationListResponseModel
{
    public string Justification { get; set; }
    public bool Status { get; set; }
    public int LeaderJustificationId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}

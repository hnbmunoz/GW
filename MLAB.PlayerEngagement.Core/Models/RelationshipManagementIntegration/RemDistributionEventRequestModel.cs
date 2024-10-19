namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagementIntegration;

public class RemDistributionEventRequestModel : BaseModel
{
    public string timestamp { get; set; }
    public long MlabPlayerId { get; set; }
    public string PlayerID { get; set; }
    public long RemProfileId { get; set; }
    public int AssignAction { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagementIntegration;

public class RemOnlineStatusRequestModel : BaseModel
{
    public string Timestamp { get; set; }
    public int RemProfileId { get; set; }
    public bool OnlineStatus { get; set; }
}

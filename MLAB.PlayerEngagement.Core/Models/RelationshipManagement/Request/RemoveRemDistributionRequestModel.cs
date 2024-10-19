namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class RemoveRemDistributionRequestModel
{
    public long RemDistributionId { get; set; }
    public long UserId { get; set; }
    public long MlabPlayerId { get; set; }
    public string PlayerId { get; set; }
    public long RemProfileId { get; set; }
    public int HasIntegration { get; set; }
}

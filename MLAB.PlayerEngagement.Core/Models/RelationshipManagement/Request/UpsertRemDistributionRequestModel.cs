namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class UpsertRemDistributionRequestModel
{
    public long RemDistributionId { get; set; }
    public long MlabPlayerId { get; set; }
    public string PlayerId { get; set; }
    public long RemProfileId { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
    public int HasIntegration { get; set; }
}

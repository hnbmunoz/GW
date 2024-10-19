namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement;

public class RemDistributionModel
{
    public long RemDistributionId { get; set; }
    public long PlayerId { get; set; }
    public string VipLevel { get; set; }
    public string Username { get; set; }
    public string PlayerStatus { get; set; }
    public string Brand { get; set; }
    public string Currency { get; set; }
    public long RemProfileId { get; set; }
    public string RemProfileName { get; set; }
    public string AgentName { get; set; }
    public string PseudoName { get; set; }
    public bool AssignStatus { get; set; }
    public DateTime? DistributionDate { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? LastAssigmentDate { get; set; }
    public string AssignedBy { get; set; }
    public string PrevReMProfileName { get; set; }
}

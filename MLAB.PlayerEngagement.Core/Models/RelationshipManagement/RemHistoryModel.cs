namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement;

public class RemHistoryModel
{
    public string Username { get; set; }
    public string ActionType { get; set; }
    public DateTime? AssignmentDate { get; set; }
    public long RemProfileId { get; set; }
    public string RemProfileName { get; set; }
    public string AgentName { get; set; }
    public string PseudoName { get; set; }
}

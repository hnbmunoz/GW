namespace MLAB.PlayerEngagement.Core.Models;

public class ContactLogTeamModel
{
    public int UserId { get; set; }
    public string UserFullName { get; set; }
    public int TotalClickMobileCount { get; set; }
    public int TotalClickEmailCount { get; set; }
    public int TotalUniquePlayerCount { get; set; }
}

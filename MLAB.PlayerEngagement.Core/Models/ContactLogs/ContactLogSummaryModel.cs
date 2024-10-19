namespace MLAB.PlayerEngagement.Core.Models;

public class ContactLogSummaryModel
{
    public long TeamId { get; set; }
    public string TeamName { get; set; }
    public long TotalUniqueUserCount { get; set; }
    public long TotalUniquePlayerCount { get; set; }
}

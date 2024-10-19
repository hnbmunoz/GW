namespace MLAB.PlayerEngagement.Core.Models;

public class ContactLogTeamRequestModel
{
    public int TeamId { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

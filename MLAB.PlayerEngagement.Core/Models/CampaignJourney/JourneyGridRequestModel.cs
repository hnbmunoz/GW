namespace MLAB.PlayerEngagement.Core.Models.CampaignJourney;

public class JourneyGridRequestModel : BaseModel
{
    #nullable enable
    public string? CreatedDateFrom { get; set; }
    public string? CreatedDateTo { get; set; }
    public string? JourneyId { get; set; }
    public string? JourneyStatus { get; set; }
    #nullable disable
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

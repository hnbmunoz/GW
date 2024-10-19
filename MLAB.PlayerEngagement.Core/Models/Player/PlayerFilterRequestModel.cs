namespace MLAB.PlayerEngagement.Core.Models;

public class PlayerFilterRequestModel
{
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public List<LookupModel>  Brands { get; set; }
    public int? StatusId { get; set; }
    public bool? InternalAccount { get; set; }
    public string PlayerId { get; set; }
    public int? CurrencyId { get; set; }
    public List<LookupModel> MarketingChannels { get; set; }
    public List<LookupModel> RiskLevels { get; set; }
    public string Username { get; set; }
    public List<LookupModel> VIPLevels { get; set; }
    public string MarketingSource { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public long? UserId { get; set; }
}

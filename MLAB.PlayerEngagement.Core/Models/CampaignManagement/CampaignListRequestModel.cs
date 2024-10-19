namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignListRequestModel : BaseModel
{
    public string CampaignCreatedDateFrom { get; set; }
    public string CampaignCreatedDateTo { get; set; }
    public string CampaignName { get; set; }
    public int? CampaignId { get; set; }
    public string CampaignStatusIds { get; set; }
    public string CampaignTypeIds { get; set; }
    public string BrandIds { get; set; }
    public string CurrencyIds { get; set; }
    public int? PageSize { get; set; }
    public int?  OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

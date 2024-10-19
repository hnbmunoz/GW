namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class UploadPlayerFilterModel :BaseModel
{
    public long CampaignId { get; set; }
    public string Guid { get; set; }
    public string Username { get; set; }
    public string PlayerId { get; set; }
    public string Brand { get; set; }
    public string Status { get; set; }
    public string LastDepositDateFrom { get; set; }
    public string LastDepositDateTo { get; set; }
    public decimal? LastDepositAmountFrom { get; set; }
    public decimal? LastDepositAmountTo { get; set; }
    public int? BonusAbuser { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }

}

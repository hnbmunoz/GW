namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class PointToIncentiveRangeConfigRequestModel
{
    public int PointToIncentiveRangeConfigurationId { get; set; }
    public int? CampaignSettingId { get; set; }
    public int? CurrencyId { get; set; }
    public int? RangeNo { get; set; }
    public decimal IncentiveValueAmount { get; set; }
    public decimal ValidPointAmountFrom { get; set; }
    public decimal ValidPointAmountTo { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models;

public class RangeConfigurationPointToIncentiveModel
{
    public int? PointToIncentiveRangeConfigurationId { get; set; }
    public int? CampaignSettingId { get; set; }
    public string RangeNo { get; set; }
    public decimal? IncentiveValueAmount { get; set; }
    public decimal? ValidPointAmountFrom { get; set; }
    public decimal? ValidPointAmountTo { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }

}

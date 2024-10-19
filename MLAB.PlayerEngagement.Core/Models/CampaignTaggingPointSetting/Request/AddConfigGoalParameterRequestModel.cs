namespace MLAB.PlayerEngagement.Core.Models;

public class AddConfigGoalParameterRequestModel
{
    public int? GoalParameterRangeConfigurationId { get; set; }
    public int? CampaignSettingId { get; set; }
    public int? CurrencyId { get; set; }
    public int? RangeNo { get; set; }
    public decimal? PointAmount { get; set; }
    public decimal? RangeFrom { get; set; }
    public decimal? RangeTo { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

}

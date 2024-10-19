namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class GoalParameterRangeConfigurationModel
{
    public int GoalParameterRangeConfigurationId { get; set; }
    public int? CampaignSettingId { get; set; }
    public int? CurrencyId { get; set; }
    public string CurrencyName { get; set; }

    public int? RangeNo { get; set; }
    public decimal? PointAmount { get; set; }
    public decimal? RangeFrom { get; set; }
    
    public decimal? RangeTo { get; set; }
   
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

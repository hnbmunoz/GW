namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class PointIncentiveSettingRequestModel : BaseModel
{
    public int CampaignSettingId { get; set; }
    public int? CampaignSettingTypeId { get; set; }
    public string CampaignSettingName { get; set; }
    public string CampaignSettingDescription { get; set; }
    public int? SettingTypeId { get; set; }
    public int? IsActive { get; set; }
    public int? GoalParameterAmountId { get; set; } 
    public int? GoalParameterCountId { get; set; }
    public List<PointToIncentiveRangeConfigRequestModel> PointToIncentiveRangeConfigurationTypeList { get; set; }
    public List<UserTaggingRequestModel> GoalParameterRangeConfigurationTypeList { get; set; }

}

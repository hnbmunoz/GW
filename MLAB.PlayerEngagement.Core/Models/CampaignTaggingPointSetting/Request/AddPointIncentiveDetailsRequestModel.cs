namespace MLAB.PlayerEngagement.Core.Models;

public class AddPointIncentiveDetailsRequestModel : BaseModel
{
    public int CampaignSettingId { get; set; }
    public int CampaignSettingTypeId { get; set; }
    public string CampaignSettingName { get; set; }
    public string CampaignSettingDescription { get; set; }
    public int? SettingTypeId { get; set; }
    public int? IsActive { get; set; }

    public int? GoalParameterAmountId { get; set; }
    public int? GoalParameterCountId { get; set; }
    public List<AddConfigPointToValueRequestModel> PointToIncentiveRangeConfigurationType { get; set; }

    public List<AddConfigGoalParameterRequestModel> GoalParameterRangeConfigurationType { get; set; }
    
}

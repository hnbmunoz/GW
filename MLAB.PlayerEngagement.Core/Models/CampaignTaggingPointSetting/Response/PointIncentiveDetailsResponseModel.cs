namespace MLAB.PlayerEngagement.Core.Models;

public class PointIncentiveDetailsResponseModel : BaseModel
{
    public int CampaignSettingId { get; set; }
    public int CampaignSettingTypeId { get; set; }
    public string CampaignSettingName { get; set; }
    public string CampaignSettingDescription { get; set; }
    public int? SettingTypeId { get; set; }
    public int? IsActive { get; set; }

    public string CurrencyId { get; set; }  //double check data type from backend

    public List<RangeConfigurationPointToIncentiveModel> RangeConfigurationPointIncentive { get; set; }

    public List<RangeConfigurationGoalParameterModel> RangeConfigurationGoalParameter { get; set; }
   
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
    
}

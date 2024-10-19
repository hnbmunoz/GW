namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class AutoTaggingDetailsRequestModel : BaseModel
{
    public int CampaignSettingId { get; set; }
    public string CampaignSettingName { get; set; }
    public string CampaignSettingDescription { get; set; }
    public int? SettingTypeId { get; set; }
    public int? IsActive { get; set; }
    public int? CampaignSettingTypeId { get; set; }

    public int? GoalParameterAmountId { get; set; } 
    public int? GoalParameterCountId { get; set; }
    //public int? UserId { get; set; }
    public List<TaggingConfigurationRequestModel> TaggingConfigurationList { get; set; }
    public List<UserTaggingRequestModel> UserTaggingList { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    //public int? UserId { get; set; }
}

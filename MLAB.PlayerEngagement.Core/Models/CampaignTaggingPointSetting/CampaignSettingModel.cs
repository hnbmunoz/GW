namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class CampaignSettingModel
{
    public int CampaignSettingId { get; set; }
    public string CampaignSettingName { get; set; }
    public string CampaignSettingDescription { get; set; }
    public int? SettingTypeId { get; set; }
    public int? IsActive { get; set; }
    public int? CampaignSettingTypeId { get; set; }
    
    public int? GoalParameterAmountId { get; set; }
    public int? GoalParameterCountId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

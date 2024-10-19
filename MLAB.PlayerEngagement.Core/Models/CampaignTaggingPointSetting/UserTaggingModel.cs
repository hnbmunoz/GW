namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class UserTaggingModel
{
    public int TaggedUserId { get; set; }
    public int TaggingConfigurationId { get; set; }
    public string TaggingConfigurationName { get; set; }
    public string TaggedUserName { get; set; }
    public int? UserId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

}

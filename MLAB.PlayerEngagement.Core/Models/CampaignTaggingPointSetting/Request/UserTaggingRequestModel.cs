namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class UserTaggingRequestModel
{
    public int TaggedUserId { get; set; }
    public int TaggingConfigurationId { get; set; }
    public string TaggingConfigurationName { get; set; }
    public int? UserId { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

}

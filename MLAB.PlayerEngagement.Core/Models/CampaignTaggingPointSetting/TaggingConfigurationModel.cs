namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class TaggingConfigurationModel
{
    public int TaggingConfigurationId { get; set; }
    public string TaggingConfigurationName { get; set; }
    public int CampaignSettingId { get; set; }
    public string SegmentName { get; set; }
    public int? PriorityNumber { get; set; }
    public int? SegmentId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

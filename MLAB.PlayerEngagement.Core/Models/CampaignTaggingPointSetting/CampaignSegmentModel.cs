namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class CampaignSegmentModel
{
    public int SegmentId { get; set; }
    public string SegmentName { get; set; }
    public string SegmentDescription { get; set; }
    public int? IsActive { get; set; }
    public int? IsStatic { get; set; }
    public string QueryForm { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

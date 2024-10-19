namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignAutoTaggingListModel
{
    public int TaggingConfigurationId { get; set; }
    public int CampaignSettingId { get; set; }
    public string SegmentName { get; set; }
    public int SegmentId { get; set; }
    public string TaggingConfigurationName { get; set; }
    public int PriorityNumber { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }
}

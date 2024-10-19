namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignConfigurationAutoTaggingModel
{
    public int CampaignConfigurationAutoTaggingId { get; set; }
    public int CampaignConfigurationId { get; set; }
    public int PriorityNumber { get; set; }
    public int TaggingConfigurationId { get; set; }
    public bool IsActive { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace;

public class CampaignPlayerTaggingInfoModel
{
    public int CampaignPlayerId { get; set; }
    public int CampaignId { get; set; }
    public int PlayerId { get; set; }
    public int? AgentId { get; set; }
    public string AgentName { get; set; }
    public string TaggedBy { get; set; }
    public DateTime? TaggedDate { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

public class AgentResponseModel
{
    public string CampaignName { get; set; }
    public string AgentName { get; set; }
    public int AgentId { get; set; }
    public int CampaignId { get; set; }
    public bool Status { get; set; }
    public int TaggedCountForTheCampaignPeriod { get; set; }
    public int TaggedCountToday { get; set; }
    public string LastTaggedDateAndTime { get; set; }
    public string AutoTaggingName { get; set; }
   public int CampaignAgentId { get; set; }
   public int CampaignStatus { get; set; }
   public string CampaignType { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.SearchLeads;

public class LeadSelectedResultResponseModel
{
    public long BroadcastConfigurationRecipientId { get; set; }
    public long BroadcastConfigurationId { get; set; }
    public long LeadId { get; set; }
    public string? LeadName { get; set; }
    public string? RecipientType { get; set; }
    public string? PlayerId { get; set; }
    public string? Username { get; set; }
    public string? Brand { get; set; }
    public string? Currency { get; set; }
    public string? VipLevel { get; set; }
    public long BroadcastResultId { get; set; }
    public string? BroadcastResult { get; set; }
    public string? BroadcastResultReason { get; set; }
    public DateTime CreatedDate { get; set; }
    public long BotId { get; set; }

}

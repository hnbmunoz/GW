namespace MLAB.PlayerEngagement.Core.Models.EngagementHub;

public class BroadcastConfigurationRecipientModel
{
    public long BroadcastConfigurationRecipientId { get; set; }

    public long BroadcastConfigurationId { get; set; }

    public int LeadId { get; set; }

    public long BroadcastResultId { get; set; }

    public string BroadcastResultReason { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public long BotId { get; set; }
}

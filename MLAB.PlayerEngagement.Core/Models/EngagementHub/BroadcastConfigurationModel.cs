namespace MLAB.PlayerEngagement.Core.Models.EngagementHub;

public class BroadcastConfigurationModel
{
    public long BroadcastConfigurationId { get; set; }

    public int BroadcastId { get; set; }

    public string BroadcastName { get; set; }

    public DateTime BroadcastDate { get; set; }

    public long BroadcastStatusId { get; set; }

    public long MessageTypeId { get; set; }

    public string Attachment { get; set; }

    public string Message { get; set; }
    public long BotId{ get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}

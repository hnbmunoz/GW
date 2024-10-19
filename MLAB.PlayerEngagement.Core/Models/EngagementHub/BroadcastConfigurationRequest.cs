namespace MLAB.PlayerEngagement.Core.Models.EngagementHub;

public class BroadcastConfigurationRequest : BaseModel
{
    public BroadcastConfigurationModel BroadcastConfiguration { get; set; }
    public List<BroadcastConfigurationRecipientModel> BroadcastConfigurationRecipients { get; set; }
}

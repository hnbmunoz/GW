namespace MLAB.PlayerEngagement.Core.Models.Administrator;

public class EventSubscriptionRequestModel: BaseModel
{
    public string SubscriberEventType { get; set; }
    public string ServiceURL { get; set; }
}

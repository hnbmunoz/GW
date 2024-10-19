namespace MLAB.PlayerEngagement.Core.Entities;

public class ExchangeQueue
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Action { get; set; }
    public Guid CacheId { get; set; }
    public string ExchangeUri { get; set; }
    public string CallbackUri { get; set; }
    public string QueueStatus { get; set; }
    public string UserId { get; set; }
    public string Remarks { get; set; }
}

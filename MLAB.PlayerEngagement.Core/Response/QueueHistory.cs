namespace MLAB.PlayerEngagement.Core.Response;

public class QueueHistory
{
    public Guid Id { get; set; }
    public Guid ServiceTypeId { get; set; }
    public Guid QueueId { get; set; }
    public string QueueName { get; set; }
    public string UserId { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedByUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public string QueueStatus { get; set; }
    public Guid RedisCacheId { get; set; }
    public string Action { get; set; }

}

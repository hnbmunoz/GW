﻿namespace MLAB.PlayerEngagement.Core.Response;

public class QueueRequests
{
    public Guid Id { get; set; }
    public Guid QueueId { get; set; }
    public string QueueName { get; set; }
    public string UserId { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedByUser { get; set; }
    public DateTime CreatedDate { get; set; } 
    public Guid RedisCacheRequestId { get; set; }
    public string QueueStatus { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid RedisCacheResponseId { get; set; }
    public string Remarks { get; set; }
    public string Action { get; set; }

}
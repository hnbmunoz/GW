namespace MLAB.PlayerEngagement.Core.Response;

public class QueueRequestResponse
{
    public int RecordCount { get; set; }
    public List<QueueRequests> QueueRequests { get; set; }
}

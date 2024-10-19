namespace MLAB.PlayerEngagement.Core.Response;

public class QueueHistoryResponse
{
    public int RecordCount { get; set; }
    public List<QueueHistory> QueueHistory { get; set; }
}

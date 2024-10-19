namespace MLAB.PlayerEngagement.Core.Models;

public class QueueFilterRequestModel 
{
    public string CreatedFrom { get; set; }
    public string CreatedTo { get; set; }
    public string CreatedBy { get; set; }
    public string Action { get; set; }
    public string Status { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

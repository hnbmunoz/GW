namespace MLAB.PlayerEngagement.Core.Models;

public class ContactLogListRequestModel : BaseModel
{
    public string ActionDateFrom { get; set; }
    public string ActionDateTo { get; set; }
    public string TeamIds { get; set; }
    public string UserIds { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

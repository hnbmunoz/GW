namespace MLAB.PlayerEngagement.Core.Models;

public class CommunicationListRequest:  BaseModel
{
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public int CaseInformatIonId { get; set; }
}

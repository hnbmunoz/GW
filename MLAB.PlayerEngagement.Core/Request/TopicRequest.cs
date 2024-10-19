using MLAB.PlayerEngagement.Core.Models;

namespace MLAB.PlayerEngagement.Core.Request;

public class TopicRequest : BaseModel
{
    public string TopicName { get; set; } = string.Empty;
    public long CaseTypeId { get; set; }
    public string BrandIds { get; set; } = string.Empty;
    public string CurrencyIds { get; set; } = string.Empty;
    public string TopicStatus { get; set; } = string.Empty;
    public int? PageSize { get; set; } = null; 
    public int? OffsetValue { get; set; } = null;
    public string SortColumn { get; set; } = string.Empty;
    public string SortOrder { get; set; } = string.Empty;
}

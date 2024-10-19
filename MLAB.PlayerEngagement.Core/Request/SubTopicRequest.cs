using MLAB.PlayerEngagement.Core.Models;

namespace MLAB.PlayerEngagement.Core.Request;

public class SubTopicRequest : BaseModel
{
    public string SubtopicName { get; set; }
    public string Status { get; set; }
    public int TopicId { get; set; }
    public string TopicIds { get; set; }
    public string CurrencyIds { get; set; }
    public string BrandIds { get; set; }
    public long? CaseTypeId { get; set; }
    public int? PageSize { get; set; } = null;
    public int? OffsetValue { get; set; } = null;
    public string SortColumn { get; set; } = string.Empty;
    public string SortOrder { get; set; } = string.Empty;
}

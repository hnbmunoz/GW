namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentationTestModel : BaseModel
{
    public string QueryForm { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public int? SegmentId { get; set; }
    public string QueryJoins { get; set; }
}

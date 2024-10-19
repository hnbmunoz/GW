namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentationRequestModel : BaseModel
{
    public string SegmentName { get; set; }
    public string SegmentTypeId { get; set; }
    public bool? SegmentStatus { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

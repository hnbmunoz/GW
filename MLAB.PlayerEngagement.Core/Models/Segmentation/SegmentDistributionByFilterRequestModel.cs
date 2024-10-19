namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentDistributionByFilterRequestModel: BaseModel
{
    public string SegmentId { get; set; }
    public string PlayerId { get; set; }
    public string UserName { get; set; }
    public string VarianceName { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

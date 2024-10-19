namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentDistributionByFilterResponseModel
{
    public int TotalRecordCount { get; set; }
    public List<SegmentDistributionUdt> SegmentDistributions { get; set; }
}

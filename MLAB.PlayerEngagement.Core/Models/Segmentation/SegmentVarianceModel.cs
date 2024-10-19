namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentVarianceModel
{
    public int SegmentId { get; set; }
    public int VarianceId { get; set; }
    public string VarianceName { get; set; }
    public bool IsActive { get; set; }
    public int Percentage { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

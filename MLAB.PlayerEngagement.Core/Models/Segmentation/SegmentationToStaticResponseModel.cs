namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentationToStaticResponseModel
{
    public SegmentationModel Segmentation { get; set; }
    public List<SegmentPlayer> Players { get; set; }
    public string PlayerIdList { get; set; }
}

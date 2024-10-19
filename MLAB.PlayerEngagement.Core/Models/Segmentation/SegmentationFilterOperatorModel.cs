namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentationFilterOperatorModel
{
    public int Id { get; set; }
    public string Label { get; set; }
    public string Value { get; set; }
    public string DataType { get; set; }
    public bool IsTemplate { get; set; }
    public int? RelationalOperatorValueTypeId { get; set; }
}

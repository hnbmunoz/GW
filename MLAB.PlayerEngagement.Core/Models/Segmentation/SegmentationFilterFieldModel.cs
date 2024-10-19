namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentationFilterFieldModel
{
    public int Id { get; set; }
    public string Label { get; set; }
    public string Value { get; set; }
    public string DataType { get; set; }
    public int SegmentConditionTypeId { get; set; }
    public int SegmentConditionSourceId { get; set; }
    public string RelationalOperatorIds { get; set; }
    public string SegmentConditionValueTypeIds { get; set; }
    public string FieldSourceTable { get; set; }
	public bool IsMulti { get; set; }
    public int PartOfSetCount { get; set; }
    public string LookupSource { get; set; }
    public string DependentLookupSource { get; set; }
    public bool IsFieldDynamic { get; set; }
    public string FieldLookupSource { get; set; }
}

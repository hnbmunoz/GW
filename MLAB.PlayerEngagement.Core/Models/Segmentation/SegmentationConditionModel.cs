namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentationConditionModel
{
    public int SegmentConditionId { get; set; }
    public int SegmentId { get; set; }
    public string SegmentName { get; set; }
    public string SegmentConditionType { get; set; }
    public string SegmentConditionLogicOperator { get; set; }
    public int? SegmentConditionFieldId { get; set; }
    public int? RelationalOperatorId { get; set; }
    public string SegmentConditionValue { get; set; }
    public string SegmentConditionValue2 { get; set; }
    public Guid Key { get; set; }
    public Guid? ParentKey { get; set; }
    public int CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public string CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public string UpdatedByName { get; set; }
    public string UpdatedDate { get; set; }
    public int? FieldLocked { get; set; }
    public int? OperatorLocked { get; set; }
}

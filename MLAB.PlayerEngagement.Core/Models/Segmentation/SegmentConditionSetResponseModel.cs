namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentConditionSetResponseModel
    {
        public int SegmentConditionSetId { get; set; }
	public int SegmentConditionFieldId { get; set; }
	public int ParentSegmentConditionFieldId { get; set; }
	public int? RelationalOperatorId { get; set; }
	public string ConditionalOperator { get; set; }
	public bool Locked { get; set; }
	public bool OperatorLocked { get; set; }
	public bool Removable { get; set; }
	public int CreatedBy { get; set; }
	public DateTime CreatedDate { get; set; }
}

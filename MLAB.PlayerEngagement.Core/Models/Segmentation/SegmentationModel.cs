namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentationModel : BaseModel
{
    public int SegmentId { get; set; }
    public string SegmentName { get; set; }
    public string SegmentDescription { get; set; }
    public bool IsActive { get; set; }
    public bool IsStatic { get; set; }
    public int? StaticParentId { get; set; }
    public string QueryForm { get; set; }
    public int CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public string CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public string UpdatedByName { get; set; }
    public string UpdatedDate { get; set; }
    public int SegmentTypeId { get; set; }
    public List<SegmentationConditionModel> SegmentConditions { get; set; }
    public List<SegmentVarianceModel> SegmentVariances { get; set; }
    public List<InFileSegmentPlayerModel> InFileSegmentPlayer { get; set; }
    public string QueryJoins { get; set; }
    public bool IsReactivated { get; set; }
    public string QueryFormTableau { get; set; }
    public int? TableauEventQueueId { get; set; }
    public int InputTypeId { get; set; }
    public long PlayerId { get; set; } 

}

namespace MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;

public class SkillFilterRequestModel : BaseModel
{
    public long? BrandId { get; set; }
    public string LicenseId { get; set; }
    public string SkillId { get; set; }
    public string SkillName { get; set; }
    public long? MessageTypeId { get; set; }
    public string MessageTypeIds { get; set; }
    public bool? IsActive { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

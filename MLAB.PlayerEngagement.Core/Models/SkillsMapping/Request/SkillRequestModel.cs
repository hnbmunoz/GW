namespace MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;

public class SkillRequestModel : BaseModel
{
    public long? Id { get; set; }
    public long BrandId { get; set; }
    public string LicenseId { get; set; }
    public string SkillId { get; set; }
    public string SkillName { get; set; }
    public long MlabPlayerId { get; set; }
    public long AgentUserId { get; set; }
    public long TeamId { get; set; }
    public long TopicId { get; set; }
    public long SubtopicId { get; set; }
    public bool IsActive { get; set; }
    public long MessageTypeId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models;

public class UpdateCaseInformationRequest : BaseModel
{
    public long CaseInformationId { get; set; }
    public long MlabPlayerId { get; set; }
    public long CaseCreatorId { get; set; }
    public long CampaignId { get; set; }
    public long CaseStatusId { get; set; }
    public long CaseTypeId { get; set; }
    public long TopicId { get; set; }
    public long SubtopicId { get; set; }
}

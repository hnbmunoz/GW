namespace MLAB.PlayerEngagement.Core.Models;

public class AddCaseCommunicationRequest: BaseModel
{
    public string PlayerId { get; set; }
    public int CaseInformationId { get; set; }
    public int CaseCreatorId { get; set; }
    public int CaseTypeId { get; set; }
    public int CampaignId { get; set; }
    public int CaseStatusId { get; set; }
    public int TopicId { get; set; }
    public int SubtopicId { get; set; }
    public int CallListNoteId { get; set; }
    public string CallListNote { get; set; }
    public int SubtopicLanguageId { get; set; }
    public int TopicLanguageId { get; set; }
    public string Subject { get; set; }
#nullable enable
    public string? CallingCode { get; set; }
    public bool? HasFlyfoneCdr { get; set; }
    public int? SubscriptionId { get; set; }
    public AddCommunicationRequest? caseCommunication { get; set; }
    public string? BrandName { get; set; }
}

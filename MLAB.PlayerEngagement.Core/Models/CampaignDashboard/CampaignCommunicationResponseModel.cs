namespace MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

public class CampaignCommunicationResponseModel
{
    public string CampaignName { get; set; }
    public long CommunicationId { get; set; }
    public long PlayerId { get; set; }
    public long CallListId { get; set; }
    public long CaseId { get; set; }
    public string CaseTopic { get; set; }
    public string CaseSubTopic { get; set; }
    public DateTime? CaseCreatedDate { get; set; }
    public DateTime? CaseUpdatedDate { get; set; }
#nullable enable
    public string? CaseCreatedBy { get; set; }
#nullable disable
    public string CaseUpdatedBy { get; set; }
    public string CommunicationMessageType { get; set; }
    public string CommunicationMessageStatus { get; set; }
    public string CommunicationMessageResponse { get; set; }
    public DateTime? EndCommunicationDate { get; set; }
    public DateTime? StartCommunicationDate { get; set; }
    public string CommunicationContent { get; set; }
    public string CommunicationCreatedBy { get; set; }
    public DateTime? CommunicationCreatedDate { get; set; }
    public string CommunicationUpdatedBy { get; set; }
    public DateTime? CommunicationUpdatedDate { get; set; }
}

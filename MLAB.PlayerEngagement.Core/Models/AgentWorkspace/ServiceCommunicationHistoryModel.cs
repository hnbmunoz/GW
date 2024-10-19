namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace;

public class ServiceCommunicationHistoryModel
{
    public int CampaignId { get; set; }
    public int CaseId { get; set; }
    public int CommunicationId { get; set; }
    public string MessageType { get; set; }
    public string MessageStatus { get; set; }
    public string MessageResponse { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CaseType { get; set; }
    public int CaseTypeId { get; set; }
    public string RecordingUrl { get; set; }
    public string Duration { get; set; }
}

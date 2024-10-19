namespace MLAB.PlayerEngagement.Core.Models;

public class  PlayerCaseResponseModel
{
    public int CaseId { get; set; }
    public string CaseStatus { get; set; }
    public string CaseType { get; set; }
    public int CaseTypeId { get; set; }
    public string CampaignType { get; set; }
    public string CaseCreatedBy { get; set; }
    public DateTime CaseCreatedDate { get; set; }
    public string CommunicationCreatedBy { get; set; }
    public int CommunicationId { get; set; }
    public string MessageType { get; set; }
    public string MessageStatus { get; set; }
    public string MessageResponse { get; set; }
    public string CampaignName { get; set; }
    public string CreatedByName { get; set; }
    public DateTime CreatedDate { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignConfigurationCommunicationModel
{
    public int? CampaignCommunicationSettingId { get; set; }
    public int? CampaignConfigurationId { get; set; }
    public int MessageGroupId { get; set; }
    public string MessageType { get; set; }
    public string CaseType { get; set; }
    public string Interval { get; set; }
    public int? CustomEventId { get; set; }
    public string MessageStatus { get; set; }
    public string Topic { get; set; }
    public string SubTopic { get; set; }
    public string CommunicationContent { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

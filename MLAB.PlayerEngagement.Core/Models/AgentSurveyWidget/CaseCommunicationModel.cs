namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;

public class CaseCommunicationModel
{
    public long CommunicationId { get; set; }
    public string ConversationId { get; set; }   = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string CaseBrandName { get; set; } = string.Empty;
    public string CaseStatusName { get;set; } = string.Empty;
    public string CommunicationCreatedBy { get; set; } = string.Empty;
    public string CaseTopic { get; set; } = string.Empty;
    public string CaseSubtopic { get; set; } = string.Empty;
  

}

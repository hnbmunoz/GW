namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;

public class AgentSurveyRequest : BaseModel
{
    public long AgentSurveyId { get; set; } = new();
    public string ConversationId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public long TopicLanguageId { get; set; } = new();
    public long SubTopicLanguageId { get; set; } = new();
    public long CreatedBy { get; set; } = new();
    public long SubmittedBy { get; set; } = new();
    public long LanguageId { get; set; } = new();
    public List<AgentSurveyFeedback> ASWFeedbackDetailsType { get; set; } = new();
    public long CommunicationProviderId { get; set; } = new();
    public string BrandName { get; set; }
    public List<CampaignIds> CampaignId { get; set; } = new();

}
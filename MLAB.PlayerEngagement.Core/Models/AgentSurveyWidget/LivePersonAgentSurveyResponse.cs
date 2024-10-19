using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;

public class LivePersonAgentSurveyResponse
{
    public long AgentSurveyId { get; set; }
    public string UserName { get; set; }   = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public long TopicId { get; set; } = new();
    public string TopicName { get;set; } = string.Empty;
    public long SubTopicId { get; set; } = new();
    public string SubTopicName { get; set; } = string.Empty;
    public long LanguageId { get; set; } = new();
    public long? CaseStatusId { get; set; }
    public long? TopicLanguageId { get; set; }
    public long? SubtopicLanguageId { get; set; }
    public string SubmittedByName { get; set; }
    public string SubmittedDate { get; set; }
    public CaseCommunicationModel CaseCommunicationDetails { get; set; }
    public List<AgentSurveyFeedback> ASWFeedbackDetailsType { get; set; }
    public List<CampaignOptionModel> CampaignList { get; set;}

}

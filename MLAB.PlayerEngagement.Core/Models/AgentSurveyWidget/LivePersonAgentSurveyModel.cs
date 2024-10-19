namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;

public class LivePersonAgentSurveyModel
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

}

namespace MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Response;

public class GetTopicOrderResponseModel
{
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public int Position { get; set; }
    public string CaseTypeName { get; set; }
    public string TopicStatus { get; set; }
}

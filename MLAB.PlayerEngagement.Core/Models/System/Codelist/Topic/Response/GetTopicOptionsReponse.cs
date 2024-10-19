namespace MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Response;

public class GetTopicOptionsReponse
{
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public bool IsActive { get; set; }
    public int CaseTypeId { get; set; }
    public int Position { get; set; }
    public string CaseTypeName { get; set; }
}
namespace MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;

public class UpdateTopicStatusRequestModel: BaseModel
{
    public int TopicId { get; set; }
    public bool IsActive { get; set; }
}

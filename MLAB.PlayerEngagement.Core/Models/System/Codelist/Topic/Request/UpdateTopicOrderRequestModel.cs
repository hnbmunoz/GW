using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Udt;

namespace MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;

public class UpdateTopicOrderRequestModel: BaseModel
{
    public List<TopicOrderTypeUdtModel> TopicOrderType { get; set; }
}

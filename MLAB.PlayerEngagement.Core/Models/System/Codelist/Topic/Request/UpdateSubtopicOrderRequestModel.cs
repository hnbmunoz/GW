using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Udt;

namespace MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;

public class UpdateSubtopicOrderRequestModel: BaseModel
{
    public List<SubtopicOrderTypeUdtModel> SubtopicOrderType { get; set; }
}

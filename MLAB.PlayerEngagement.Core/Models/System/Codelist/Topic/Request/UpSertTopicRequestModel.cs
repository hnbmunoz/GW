using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Udt;

namespace MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;

public class UpSertTopicRequestModel : BaseModel
{
    public int CodeListId { get; set; }
    public int CaseTypeId { get; set; }
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public List<TopicBrandUdtModel> TopicBrandType { get; set; }
    public List<TopicCurrencyUdtModel> TopicCurrencyType { get; set; }
    public List<TopicLanguageUdtModel> TopicLanguageType { get; set; }
    public bool IsActive { get; set; }
    public int Position { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models;

public class CodeListTopicModel : BaseModel
{
    public long CodeListId { get; set; }
    public bool IsActive { get; set; }
    public List<TopicModel> Topics  { get; set; }
}

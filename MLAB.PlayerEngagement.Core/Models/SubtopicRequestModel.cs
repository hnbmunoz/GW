namespace MLAB.PlayerEngagement.Core.Models;

public class SubtopicRequestModel : BaseModel
{
    public int CodeListId { get; set; }
    public string CodeListStatus { get; set; }
    public List<SubTopicModel> Subtopics { get; set; }

}

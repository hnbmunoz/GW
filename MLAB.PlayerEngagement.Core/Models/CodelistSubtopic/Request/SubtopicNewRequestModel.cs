namespace MLAB.PlayerEngagement.Core.Models.CodelistSubtopic.Request;

public class SubtopicNewRequestModel : BaseModel
{
    public int SubtopicId { get; set; }
    public string SubtopicName { get; set; }
    public bool IsActive { get; set; }
    public int Position { get; set; }
    public List<SubtopicTopicRequestModel> SubtopicTopicList { get; set; }
    public List<SubtopicLanguageRequestModel> SubtopicLanguageList { get; set; }
    public List<SubtopicCurrencyRequestModel> SubtopicCurrencyList { get; set; }
}

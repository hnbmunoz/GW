namespace MLAB.PlayerEngagement.Core.Models;

public class SubTopicModel
{
    public int Id { get; set; }
    public string SubTopicName { get; set; }
    public string IsActive { get; set; }
    public int Position { get; set; }
    public int? TopicId { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public List<SubtopicBrandRequestModel> Brand { get; set; }
    public List<SubtopicCurrencyRequestModel> currency { get; set; }
    public List<SubtopicTopicRequestModel> topic { get; set; }
}

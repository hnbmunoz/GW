namespace MLAB.PlayerEngagement.Core.Models;

public class TopicModel
{
    public int Id { get; set; }
    public string TopicName { get; set; }
    public int CodeListId { get; set; }
    public int Position { get; set; }
    public bool IsActive { get; set; }
    public int CreatedBy { get; set; }
    //public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    //public DateTime? UpdatedDate { get; set; }
}

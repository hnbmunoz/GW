namespace MLAB.PlayerEngagement.Core.Models.Message;

public class AddMessageStatusModel
{
    public int Id { get; set; }
    public string MessageStatusName { get; set; }
    public List<string> MessageTypeIds { get; set; }
    public int MessageTypeId { get; set; }
    public int Position { get; set; }
    public bool IsActive { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.Message;

public class AddMessageResponseModel
{
    public int Id { get; set; }
    public string MessageResponseName { get; set; }
    public List<string> MessageStatusIds { get; set; }
    public int MessageStatusId { get; set; }
    public int Position { get; set; }
    public bool IsActive { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}

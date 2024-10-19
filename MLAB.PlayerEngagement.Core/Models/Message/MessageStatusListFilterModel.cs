namespace MLAB.PlayerEngagement.Core.Models.Message;

public class MessageStatusListFilterModel : BaseModel
{
    public int MessageTypeId { get; set; }
    public string MessageStatusName { get; set; }
    public string MessageStatusStatus { get; set; }
    public string MessageTypeIds { get; set; }
}

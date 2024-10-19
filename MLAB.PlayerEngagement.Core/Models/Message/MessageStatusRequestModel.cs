namespace MLAB.PlayerEngagement.Core.Models.Message;

public class MessageStatusRequestModel : BaseModel
{

    public long CodeListId { get; set; }
    public bool IsActive { get; set; }
    public List<AddMessageStatusModel> MessageStatus { get; set; }
}

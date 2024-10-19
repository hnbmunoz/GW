namespace MLAB.PlayerEngagement.Core.Models.Message;

public class MessageResponseRequestModel :BaseModel
{
    public long CodeListId { get; set; }
    public bool IsActive { get; set; }
    public List<AddMessageResponseModel> MessageResponses { get; set; }

}

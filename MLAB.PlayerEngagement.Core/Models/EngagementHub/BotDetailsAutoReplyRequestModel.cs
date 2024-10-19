namespace MLAB.PlayerEngagement.Core.Models.Request;

public class BotDetailsAutoReplyRequestModel : BaseModel
{
    public int BOTDetailsAutoReplyId { get; set; }
    public int BotDetailId { get; set; }
    public string Trigger { get; set; }
    public string ChatValue { get; set; }
    public string Type { get; set; }
    public int TelegramBotAutoReplyTriggerId { get; set; }
    public string Message { get; set; }
    public string Attachment { get; set; }
}

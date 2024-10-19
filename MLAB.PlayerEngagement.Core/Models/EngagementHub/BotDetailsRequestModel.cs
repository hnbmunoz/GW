namespace MLAB.PlayerEngagement.Core.Models.Request;

public class BotDetailsRequestModel : BaseModel
{
    public long BotDetailId { get; set; }
    public long BotId { get; set; }
    public string BotUsername { get; set; }
    public string BotToken { get; set; }
    public int BrandId { get; set; }
    public long BotMlabUserId { get; set; }
    public int StatusId { get; set; }
}

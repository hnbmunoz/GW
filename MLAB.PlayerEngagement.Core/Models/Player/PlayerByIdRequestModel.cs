namespace MLAB.PlayerEngagement.Core.Models;

public class PlayerByIdRequestModel
{
    public string PlayerId { get; set; }
    public bool? HasAccess { get; set; }
    public string PageSource { get; set; }
    public long UserId { get; set; }
    public string BrandName { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models;

public class PlayerContactRequestModel
{
    public long MlabPlayerId { get; set; }
    public long UserId { get; set; }
    public int ContactTypeId { get; set; }
    public string PageName { get; set; }
}

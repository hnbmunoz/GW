namespace MLAB.PlayerEngagement.Core.Models;

public class LockUserRequestModel : BaseModel
{
    public int UserIdRequest { get; set; }
    public int UserStatusId { get; set; }
    public int UpdatedBy { get; set; }

}

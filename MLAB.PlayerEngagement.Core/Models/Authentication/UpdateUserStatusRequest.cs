using MLAB.PlayerEngagement.Core.Constants;

namespace MLAB.PlayerEngagement.Core.Models.Authentication;

public class UpdateUserStatusRequest
{
    public int UserId { get; set; }
    public UserStatus UserStatus { get; set; }
    public int UpdatedBy { get; set; }
}

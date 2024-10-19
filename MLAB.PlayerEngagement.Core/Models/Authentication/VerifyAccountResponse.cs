using MLAB.PlayerEngagement.Core.Constants;

namespace MLAB.PlayerEngagement.Core.Models.Authentication;

public class VerifyAccountResponse
{
    public int UserId { get; set; }
    private int UserStatusId { get; set; }
    private int ValidUser { get; set; }
    public bool IsCurrentlyLocked { get; set; }
    public string FullName { get; set; }
    public string MCoreUserId { get; set; }

    public UserStatus UserStatus
    {
        get
        {
            return (UserStatus)this.UserStatusId;
        }
    }

    public bool IsValidUser
    {
        get
        {
            return this.ValidUser == 1;
        }
    }
}

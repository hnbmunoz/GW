namespace MLAB.PlayerEngagement.Core.Models.Authentication
{
    public class UpdateUserOnlineStatusRequest
    {
        public int UserId { get; set; }
        public bool IsOnline { get; set; }
    }
}

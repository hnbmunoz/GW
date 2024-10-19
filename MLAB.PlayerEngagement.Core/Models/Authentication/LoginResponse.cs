namespace MLAB.PlayerEngagement.Core.Models.Authentication;

public class LoginResponse
{
    public int UserId { get; set; }
    public string Access { get; set; }
    public string Token { get; set; }
    public DateTime? ExpiresIn { get; set; }
    public string FullName { get; set; }
    public string MCoreUserId { get; set; }
}

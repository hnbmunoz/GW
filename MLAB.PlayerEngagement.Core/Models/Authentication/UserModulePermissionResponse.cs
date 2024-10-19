namespace MLAB.PlayerEngagement.Core.Models.Authentication;

public class UserModulePermissionResponse
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public string Access { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.Authentication;

public class ResetPasswordRequest : BaseModel
{
    public string Email { get; set; }
}

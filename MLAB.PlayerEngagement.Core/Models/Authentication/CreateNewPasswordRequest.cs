namespace MLAB.PlayerEngagement.Core.Models.Authentication;

public class CreateNewPasswordRequest : BaseModel
{
    public string PasswordId { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public string ActionId { get; set; }

   
}

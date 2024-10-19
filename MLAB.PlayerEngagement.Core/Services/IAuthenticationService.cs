using MLAB.PlayerEngagement.Core.Models.Authentication;
using MLAB.PlayerEngagement.Core.Models;
namespace MLAB.PlayerEngagement.Core.Services;

public interface IAuthenticationService
{
    Task<bool> ValidateEmailAsync(string email);
    Task<LoginResponse> VerifyAccountAsync(LoginRequest request);
    Task<bool> ActivateAccountAsync(UserStatusRequest request);
    Task<bool> DeactivateAccountAsync(UserStatusRequest request);
    Task<LoginResponse> GetUserModulePermissionAsync(int userId);
    Task<LoginResponse> VerifyAdminAccountAccessAsync(LoginRequest request);
    Task<bool> VerifyUserChatByProviderAsync(VerifyUserChatProviderRequest request);
    Task<bool> UpdateUserStatusByIdAsync(UpdateUserOnlineStatusRequest request);
}

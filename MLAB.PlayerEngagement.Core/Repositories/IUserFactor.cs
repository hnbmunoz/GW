using MLAB.PlayerEngagement.Core.Models.Authentication;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Users.Udt;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface IUserFactor
{
    Task<bool> ValidateEmailAsync(string email);
    Task<VerifyAccountResponse> VerifyAccountAsync(LoginRequest login);
    Task<List<UserModulePermissionResponse>> GetUserModulePermissionAsync(int userId);
    Task<bool> UpdateUserStatusAsync(UpdateUserStatusRequest request);
    Task<List<RoleModel>> GetRolesFilterAsync();
    Task<List<TeamModel>> GetTeamsFilterAsync();
    Task<List<LookupModel>> GetAgentsForTagging();
    Task<List<LookupModel>> GetUserLookupsAsync(string filter);
    Task<List<UserListOptionModel>> GetUserListOptionAsync();
    Task<List<LookupModel>> GetCommProviderUserListOptionAsync();
    Task<List<LookupModel>> GetTeamListByUserIdOptionAsync(long userId);
    Task<AdminAccessResponse> VerifyAdminAccountAccessAsync(LoginRequest request);
    Task<List<UserOptionModel>> GetUserOptionsAsync();
    Task<bool> VerifyUserChatByProviderAsync(VerifyUserChatProviderRequest request);
    Task<List<CommunicationProviderAccountUdt>> GetCommunicationProviderAccountListByUserIdAsync(int userId);
    Task<List<DataRestrictionAccessModel>> GetDataRestrictionDetailsByUserIdAsync(long userId);
    Task<bool> UpdateUserStatusByIdAsync(UpdateUserOnlineStatusRequest request);
    Task<bool> SetUserIdleAsync(string userId, bool isIdle);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Users.Request;
using MLAB.PlayerEngagement.Core.Models.Users.Udt;

namespace MLAB.PlayerEngagement.Core.Services
{
    public interface IUserManagementService
    {
        Task<List<RoleModel>> GetRolesFilterAsync();
        Task<List<TeamModel>> GetTeamsFilterAsync();
        Task<List<LookupModel>> GetAgentsForTagging();
        Task<List<LookupModel>> GetUserLookupsAsync(string filter);    
        Task<List<UserListOptionModel>> GetUserListOptionAsync();
        Task<List<LookupModel>> GetCommProviderUserListOptionAsync();
        Task<List<LookupModel>> GetTeamListByUserIdOptionAsync(long userId);
        Task<List<UserOptionModel>> GetUserOptionsAsync();
        Task<bool> ValidateUserProviderNameAsync(UserProviderRequestModel request);
        Task<bool> ValidateCommunicationProviderAsync(CommunicationProviderRequestModel request);
        Task<List<CommunicationProviderAccountUdt>> GetCommunicationProviderAccountListByUserIdAsync(int userId);
        Task<List<DataRestrictionAccessModel>> GetDataRestrictionDetailsByUserIdAsync(long userId);
        Task<bool> SetUserIdleAsync(string userId, bool isIdle);
    }
}

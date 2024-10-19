using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Users.Request;
using MLAB.PlayerEngagement.Core.Models.Users.Udt;
using Microsoft.AspNetCore.Authorization;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserManagementController : BaseController
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly IUserManagementService _userManagementService;

    public UserManagementController(IMessagePublisherService messagePublisherService, IUserManagementService userManagementService)
    {
        _messagePublisherService = messagePublisherService;
        _userManagementService = userManagementService;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetRoleListAsync([FromBody] RoleFilterModel request)
    {
        var result = await _messagePublisherService.GetRoleListAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddRoleAsync([FromBody] RoleRequestModel request)
    {
        var result = await _messagePublisherService.AddRoleAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateRoleAsync([FromBody] RoleRequestModel request)
    {
        var result = await _messagePublisherService.UpdateRoleAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetRoleByIdAsync([FromBody] RoleIdRequestModel request)
    {
        var result = await _messagePublisherService.GetRoleByIdAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCloneRoleAsync([FromBody] RoleIdRequestModel request)
    {
        var result = await _messagePublisherService.GetCloneRoleAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetAllRoleAsync([FromBody] BaseModel request)
    {
        var result = await _messagePublisherService.GetAllRoleAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetTeamListAsync([FromBody] TeamFilterModel request)
    {
        var result = await _messagePublisherService.GetTeamListAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddTeamAsync([FromBody] TeamRequestModel request)
    {
        var result = await _messagePublisherService.AddTeamAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateTeamAsync([FromBody] TeamRequestModel request)
    {
        var result = await _messagePublisherService.UpdateTeamAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetTeamByIdAsync([FromBody] TeamIdRequestModel request)
    {
        var result = await _messagePublisherService.GetTeamByIdAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetUserListAsync([FromBody] UserFilterModel userFilter)
    {
        var result = await _messagePublisherService.GetUserListAsync(userFilter);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetUserByIdAsync([FromBody] UserIdRequestModel user)
    {
        var result = await _messagePublisherService.GetUserByIdAsync(user);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCommunicationProviderAccountListbyIdAsync([FromBody] UserIdRequestModel user)
    {
        var result = await _messagePublisherService.GetCommunicationProviderAccountListbyIdAsync(user);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddUserAsync([FromBody] UserRequestModel createUser)
    {
        var result = await _messagePublisherService.AddUserAsync(createUser);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateUserAsync([FromBody] UserRequestModel updateUser)
    {
        var result = await _messagePublisherService.UpdateUserAsync(updateUser);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> LockUserAsync([FromBody] LockUserRequestModel lockUser)
    {
        var result = await _messagePublisherService.LockUserAsync(lockUser);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRolesFilterAsync()
    {
        var result = await _userManagementService.GetRolesFilterAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTeamsFilterAsync()
    {
        var result = await _userManagementService.GetTeamsFilterAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<LookupModel>> GetUserLookupsAsync(string filter)
    {
        var result = await _userManagementService.GetUserLookupsAsync(filter);
        return result;
    }        
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<UserListOptionModel>> GetUserListOptionAsync()
    {
        var result = await _userManagementService.GetUserListOptionAsync();
        return result;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<LookupModel>> GetCommProviderUserListOptionAsync()
    {
        var result = await _userManagementService.GetCommProviderUserListOptionAsync();
        return result;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<LookupModel>> GetTeamListByUserIdOptionAsync(long userId)
    {
        var result = await _userManagementService.GetTeamListByUserIdOptionAsync(userId);
        return result;
    }

        [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<UserOptionModel>> GetUserOptionsAsync()
    {
        var result = await _userManagementService.GetUserOptionsAsync();
        return result;
    }
   
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidateUserProviderNameAsync(UserProviderRequestModel request)
    {
        try
            {
                var result = await _userManagementService.ValidateUserProviderNameAsync(request);

                return result;
            }
         catch (Exception)
            {

                return false;
            }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidateCommunicationProviderAsync(CommunicationProviderRequestModel request)
    {
        try
        {
            var result = await _userManagementService.ValidateCommunicationProviderAsync(request);

            return result;
        }
        catch (Exception)
        {

            return false;
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<CommunicationProviderAccountUdt>> GetCommunicationProviderAccountListByUserIdAsync(int userId)
    {
        var result = await _userManagementService.GetCommunicationProviderAccountListByUserIdAsync(userId);
        return result;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<DataRestrictionAccessModel>> GetDataRestrictionDetailsByUserIdAsync(long userId)
    {
        var userIdSystem = UserId != null ? Int64.Parse(UserId) : userId;
        var result = await _userManagementService.GetDataRestrictionDetailsByUserIdAsync(userIdSystem);
        return result;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetUserIdleAsync(bool isIdle)
    {
        
        await _userManagementService.SetUserIdleAsync(this.UserId, isIdle);
        return Ok(new { message = "User status updated successfully" });
    }
}

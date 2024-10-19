using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using System.Net;
using System.Security.Claims;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseController : ControllerBase
{
    protected async Task<ResponseModel> GetResultAsync(bool isSuccess)
    {
        return isSuccess ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Something went wrong");
    }

    protected string UserId { 
        get { 
            return HttpContext.User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.NameIdentifier)?.Value; 
        } 
    }
}

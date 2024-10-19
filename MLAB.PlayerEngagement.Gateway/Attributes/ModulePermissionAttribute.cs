using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MLAB.PlayerEngagement.Core.Constants;
using System.Security.Claims;

namespace MLAB.PlayerEngagement.Gateway.Attributes;

public class ModulePermissionAttribute : TypeFilterAttribute
{
    public ModulePermissionAttribute(string claimValue) : base(typeof(ModulePermissionFilter))
    {
        Arguments = new object[] { new Claim(ModulePermissions.ClaimType, claimValue) };
    }
}

public class ModulePermissionFilter : IAuthorizationFilter
{
    readonly Claim _claim;

    public ModulePermissionFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string[] claimArrays = _claim.Value.Split('|');
        bool hasClaim = false;

        foreach (string claim in claimArrays)
        {
             hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value.Contains(claim));
             if (hasClaim)
            {
                break;
            }
        }

        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}

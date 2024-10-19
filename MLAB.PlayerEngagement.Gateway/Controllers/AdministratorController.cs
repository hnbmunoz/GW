using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Administrator;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Gateway.Attributes;
using System.Net;


namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdministratorController : BaseController
{
    private readonly IAdminstratorService _adminService;
    private readonly IMessagePublisherService _messagePublisherService;
    public AdministratorController(IAdminstratorService adminService, IMessagePublisherService messagePublisherService)
    {
        _adminService = adminService;
        _messagePublisherService = messagePublisherService;
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQueueRequestAsync([FromBody] QueueFilterRequestModel queueFilter)
    {
        var result = await _adminService.GetQueueRequestAsync(queueFilter);
        return Ok(result);
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQueueHistoryAsync([FromBody] QueueFilterRequestModel queueFilter)
    {
        var result = await _adminService.GetQueueHistoryAsync(queueFilter);
        return Ok(result);
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDistinctQueueStatusAsync()
    {
        var result = await _adminService.GetDistinctQueueStatus();
        return Ok(result);
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDistinctQueueActionsAsync()
    {
        var result = await _adminService.GetDistinctQueueActions();
        return Ok(result);
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Write)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteQueueByCreatedDateRange([FromBody] DeleteQueueRequestModel queueFilter)
    {
        var result = await _adminService.DeleteQueueByCreatedDateRange(queueFilter);
        return Ok(result);
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetAppConfigSettingByFilterAsync(AppConfigSettingFilterRequestModel filter)
    {
        var result = await _messagePublisherService.GetAppConfigSettingByFilterAsync(filter);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpsertAppConfigSettingAsync(AppConfigSettingRequestModel model)
    {
        var result = await _messagePublisherService.UpsertAppConfigSettingAsync(model);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetEventSubscriptionAsync(EventSubscriptionFilterRequestModel filter)
    {
        var result = await _messagePublisherService.GetEventSubscriptionAsync(filter);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [ModulePermissionAttribute(ModulePermissions.Admin_Permission_Read)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateEventSubscriptionAsync(EventSubscriptionRequestModel model)
    {
        var result = await _messagePublisherService.UpdateEventSubscriptionAsync(model);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

}

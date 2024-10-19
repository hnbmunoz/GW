using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagementIntegration;
using System.Text;
using System.Globalization;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Models.EngagementHub;
using MLAB.PlayerEngagement.Core.Models.Request;
using MLAB.PlayerEngagement.Application.Services;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EngagementHubController : ControllerBase
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly IEngagementHubService _engagementHubService;


    public EngagementHubController(IMessagePublisherService messagePublisherService, IEngagementHubService engagementHubService)
    {
        _messagePublisherService = messagePublisherService;
        _engagementHubService = engagementHubService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBotAutoReplyListByFilterAsync([FromBody] BotAutoReplyFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetBotAutoReplyListByFilterAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBotByIdAsync([FromBody] BotFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetBotByIdAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpSertBotDetailsAsync([FromBody] BotDetailsRequestModel request)
    {
        var result = await _messagePublisherService.UpSertBotDetailsAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpSertBotDetailsAutoReplyAsync(BotDetailsAutoReplyRequestModel request)
    {
        var result = await _messagePublisherService.UpSertBotDetailsAutoReplyAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidateBotIdAsync(long botId)
    {
        try
        {
            var result = await _engagementHubService.ValidateBotIdAsync(botId);

            return result;
        }
        catch (Exception)
        {

            return false;
        }

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBotDetailListResultByFilterAsync([FromBody] BotDetailFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetBotDetailListResultByFilterAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBroadcastListByFilter([FromBody] BroadcastFilterRequestModel request)
    {

        var result = await _messagePublisherService.GetBroadcastListByFilter(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBroadcastConfigurationByIdAsync(GetBroadcastConfigurationByIdRequest request)
    {
        var result = await _messagePublisherService.GetBroadcastConfigurationByIdAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpsertBroadcastConfigurationAsync([FromBody] BroadcastConfigurationRequest request)
    {
        var result = await _messagePublisherService.UpsertBroadcastConfigurationAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBroadcastConfigurationRecipientsStatusProgressByIdAsync([FromBody] GetBroadcastConfigurationByIdRequest request)
    {
        var result = await _messagePublisherService.GetBroadcastConfigurationRecipientsStatusProgressById(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> CancelBroadcast(long broadcastConfigurationId, long userId)
    {
        try
        {
            var result = await _engagementHubService.CancelBroadcast(broadcastConfigurationId, userId);

            return result;
        }
        catch (Exception)
        {
            return false;
        }

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> DeleteAutoReplyAsync(long telegramBotAutoReplyTriggerId)
    {
        try
        {
            var result = await _engagementHubService.DeleteAutoReplyAsync(telegramBotAutoReplyTriggerId);

            return result;
        }
        catch (Exception)
        {

            return false;
        }

    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TelegramCustomAutoReplyCountAsync(long botDetailId)
    {
        try
        {
            var result = await _engagementHubService.TelegramCustomAutoReplyCountAsync(botDetailId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

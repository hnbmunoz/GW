using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Models.SearchLeads;
using MLAB.PlayerEngagement.Core.Models.Request;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SearchLeadsController : ControllerBase
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly ISearchLeadsService _searchLeadsService;


    public SearchLeadsController(IMessagePublisherService messagePublisherService, ISearchLeadsService searchLeadsService)
    {
        _messagePublisherService = messagePublisherService;
        _searchLeadsService = searchLeadsService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeadsByFilterAsync([FromBody] SearchLeadsRequestModel request)
    {
        var result = await _messagePublisherService.GetLeadsByFilterAsync(request);

        return result ? new OkResult() : new BadRequestObjectResult(new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered"));


    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeadSelectionByFilterAsync([FromBody] SearchLeadsRequestModel request)
    {
        var result = await _searchLeadsService.GetLeadSelectionByFilterAsync(request);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> LinkUnlinkPlayerAsync(long leadId, long linkedMlabPlayerId, long userId)
    {
        try
        {
            var result = await _searchLeadsService.LinkUnlinkPlayerAsync(leadId, linkedMlabPlayerId, userId);

            return result;
        }
        catch (Exception)
        {
            return false;
        }

    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> RemoveLeadAsync(long leadId)
    {
        try
        {
            var result = await _searchLeadsService.RemoveLeadAsync(leadId);

            return result;
        }
        catch (Exception)
        {

            return false;
        }

    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeadLinkDetailsByIdAsync(long mlabPlayerId)
    {
        try
        {
            var result = await _searchLeadsService.GetLeadLinkDetailsByIdAsync(mlabPlayerId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSourceBOTAsync()
    {
        try
        {
            var result = await _searchLeadsService.GetAllSourceBOTAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeadPlayersByUsernameAsync(string username, long userId)
    {
        var result = await _searchLeadsService.GetLeadPlayersByUsernameAsync(username, userId);
        return Ok(result);
    }
}

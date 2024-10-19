using Microsoft.AspNetCore.Mvc;
using System.Net;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Models.AgentMonitoring;
using MLAB.PlayerEngagement.Application.Responses;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AgentMonitoringController : BaseController
{

    private readonly IAgentMonitoringService _agentMonitoringService;
    private readonly IMessagePublisherService _messagePublisherService;
    public AgentMonitoringController(IAgentMonitoringService agentMonitoringService, IMessagePublisherService messagePublisherService)
    {
        _agentMonitoringService = agentMonitoringService;
        _messagePublisherService = messagePublisherService;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutoTaggingNameListAsync([FromBody] TaggedNameRequestModel request)
    {
        try
        {
            var result = await _agentMonitoringService.GetAutoTaggingNameListAsync(request.CampaignId);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignAgentList([FromBody] AgentListRequestModel request)
    {
        try
        {
            var result = await _agentMonitoringService.GetCampaignAgentList(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateCampaignAgentStatusAsync([FromBody] AgentStatusRequestModel request)
    {
        var result = await _messagePublisherService.UpdateCampaignAgentStatusAsync(request);

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
    public async Task<ResponseModel> UpsertDailyReportAsync([FromBody] List<DailyReportRequestModel> request)
    {
        var result = await _messagePublisherService.UpsertDailyReportAsync(request);

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
    public async Task<ResponseModel> DeleteDailyReportByIdAsync([FromBody] List<DeleteDailyReportRequestModel> request)
    {
        var result = await _messagePublisherService.DeleteDailyReportByIdAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using System.Net;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.CampaignPerformance;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CampaignPerformanceController : BaseController
{
    private readonly ICampaignPerformanceService _campaignPerformanceService;
    private readonly IMessagePublisherService _messagePublisherService;
    public CampaignPerformanceController(ICampaignPerformanceService campaignPerformanceService, IMessagePublisherService messagePublisherService)
    {
        _campaignPerformanceService = campaignPerformanceService;
        _messagePublisherService = messagePublisherService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignPerformanceFilterAsync(int campaignTypeId)
    {
        try
        {
            var result = await _campaignPerformanceService.GetCampaignPerformanceFilterAsync(campaignTypeId);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignPerformanceListAsync([FromBody] CampaignPerformanceRequestModel request)
    {
        var result = await _messagePublisherService.GetCampaignPerformanceListAsync(request);

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

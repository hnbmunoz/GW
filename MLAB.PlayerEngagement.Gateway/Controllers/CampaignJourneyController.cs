using System.Net;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.CampaignJourney;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CampaignJourneyController : BaseController
{
    private readonly ICampaignJourneyService _campaignJourneyService;
    private readonly IMessagePublisherService _messagePublisherService;

    public CampaignJourneyController(ICampaignJourneyService campaignJourneyService, IMessagePublisherService messagePublisherService)
    {
        _campaignJourneyService = campaignJourneyService;
        _messagePublisherService = messagePublisherService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetJourneyGridAsync([FromBody] JourneyGridRequestModel request)
    {
        var result = await _messagePublisherService.GetJourneyGridAsync(request);
        return result == true ? new ResponseModel() : new ResponseModel((int) HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetJourneyDetailsAsync([FromBody] JourneyDetailsRequestModel request)
    {
        var result = await _messagePublisherService.GetJourneyDetailsAsync(request);
        return result == true ? new ResponseModel() : new ResponseModel((int) HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetJourneyCampaignDetailsAsync(string campaignId)
    {
        var result = await _campaignJourneyService.GetJourneyCampaignDetailsAsync(campaignId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveJourneyAsync([FromBody] JourneyRequestModel request)
    {
        var result = await _messagePublisherService.SaveJourneyAsync(request);
        return result == true ? new ResponseModel() : new ResponseModel((int) HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetJourneyCampaignNamesAsync(string searchFilterField, int searchFilterType, int campaignType)
    {
        var result = await _campaignJourneyService.GetJourneyCampaignNamesAsync(searchFilterField, searchFilterType, campaignType);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetJourneyNamesAsync()
    {
        var result = await _campaignJourneyService.GetJourneyNamesAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetJourneyStatusAsync()
    {
        var result = await _campaignJourneyService.GetJourneyStatusAsync();
        return Ok(result);
    }
}

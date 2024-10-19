using Microsoft.AspNetCore.Mvc;
using System.Net;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.CallListValidation;
using Microsoft.AspNetCore.Authorization;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CallListValidationController : BaseController
{
    private readonly ICallListValidationService _callListValidationService;
    private readonly IMessagePublisherService _messagePublisherService;
    public CallListValidationController(ICallListValidationService callListValidationService, IMessagePublisherService messagePublisherService)
    {
        _callListValidationService = callListValidationService;
        _messagePublisherService = messagePublisherService;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCallListValidationFilterAsync([FromBody] CallValidationFilterRequestModel request)
    {
        try
        {
            var result = await _callListValidationService.GetCallListValidationFilterAsync(request.CampaignId);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCallValidationListAsync([FromBody] CallValidationListRequestModel request)
    {
        try
        {
            var result = await _callListValidationService.GetCallValidationListAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeaderJustificationListAsync()
    {
        try
        {
            var result = await _callListValidationService.GetLeaderJustificationListAsync();
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpsertAgentValidationAsync([FromBody] List<AgentValidationRequestModel> request)
    {
        var result = await _messagePublisherService.UpsertAgentValidationAsync(request);

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
    public async Task<ResponseModel> UpsertLeaderValidationAsync([FromBody] List<LeaderValidationsRequestModel> request)
    {
        var result = await _messagePublisherService.UpsertLeaderValidationAsync(request);

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
    public async Task<ResponseModel> UpsertCallEvaluationAsync([FromBody] CallEvaluationRequestModel request)
    {
        var result = await _messagePublisherService.UpsertCallEvaluationAsync(request);
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
    public async Task<ResponseModel> DeleteCallEvaluationAsync([FromBody] DeleteCallEvaluationRequestModel request)
    {
        var result = await _messagePublisherService.DeleteCallEvaluationAsync(request);

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
    public async Task<ResponseModel> UpsertLeaderJustificationAsync([FromBody] List<LeaderJustificationRequestModel> request)
    {
        var result = await _messagePublisherService.UpsertLeaderJustificationAsync(request);

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

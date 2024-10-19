using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PostChatSurveyController : BaseController
{
    private readonly ISystemService _systemService;
    private readonly IMessagePublisherService _messagePublisherService;

    public PostChatSurveyController(ISystemService systemService, IMessagePublisherService messagePublisherService)
    {
        _systemService = systemService;
        _messagePublisherService = messagePublisherService;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetPostChatSurveyByFilterAsync([FromBody] PostChatSurveyFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetPostChatSurveyByFilterAsync(request);

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
    public async Task<IActionResult> GetSkillsByLicenseIdAsync(string LicenseId)
    {
        try
        {
            var result = await _systemService.GetSkillsByLicenseIdAsync(LicenseId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPCSLookupsAsync()
    {
        try
        {
            var result = await _systemService.GetPCSLookupsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPostChatSurveyByIdAsync(PostChatSurveyIdRequestModel request)
    {
        try
        {
            var result = await _systemService.GetPostChatSurveyByIdAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> TogglePostChatSurveyAsync([FromBody] PostChatSurveyToggleRequestModel request)
    {
        var result = await _systemService.TogglePostChatSurveyAsync(request);
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
    public async Task<ResponseModel> UpsertPostChatSurveyAsync([FromBody] PostChatSurveyRequestModel request)
    {
        var result = await _messagePublisherService.UpsertPostChatSurveyAsync(request);

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
    public async Task<bool> ValidatePostChatSurveyQuestionIDAsync([FromBody] ValidatePostChatSurveyQuestionIDModel request)
    {
        var result = await _systemService.ValidatePostChatSurveyQuestionID(request);

        return result;
    }
}

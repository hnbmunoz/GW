using System.Net;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SkillMappingController : BaseController
{
    private readonly ISystemService _systemService;
    private readonly IMessagePublisherService _messagePublisherService;

    public SkillMappingController(ISystemService systemService, IMessagePublisherService messagePublisherService)
    {
        _systemService = systemService;
        _messagePublisherService = messagePublisherService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSkillByFilterAsync([FromBody] SkillFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetSkillByFilterAsync(request);

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
    public async Task<ResponseModel> UpsertSkillAsync([FromBody] SkillRequestModel request)
    {
        var result = await _messagePublisherService.UpsertSkillAsync(request);

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
    public async Task<ResponseModel> ToggleSkillAsync([FromBody] SkillToggleRequestModel request)
    {
        var result = await _systemService.ToggleSkillAsync(request);
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
    public async Task<ResponseModel> ValidateSkillAsync([FromBody] ValidateSkillRequestModel request)
    {
        var result = await _systemService.ValidateSkillAsync(request);
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

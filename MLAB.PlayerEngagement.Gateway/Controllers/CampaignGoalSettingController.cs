using System.Net;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CampaignGoalSettingController : ControllerBase
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly ICampaignGoalSettingService _campaignGoalSettingService;
    public CampaignGoalSettingController(IMessagePublisherService messagePublisherService, ICampaignGoalSettingService campaignGoalSettingService)
    {
        _messagePublisherService = messagePublisherService;
        _campaignGoalSettingService = campaignGoalSettingService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignGoalSettingByFilterAsync([FromBody] CampaignGoalSettingByFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetCampaignGoalSettingByFilterAsync(request);

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
    public async Task<ResponseModel> AddCampaignGoalSettingAsync([FromBody] CampaignGoalSettingRequestModel request)
    {
        var result = await _messagePublisherService.AddCampaignGoalSettingAsync(request);

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
    public async Task<ResponseModel> GetCampaignGoalSettingByIdAsync([FromBody] CampaignGoalSettingIdRequestModel request)
    {
        var result = await _messagePublisherService.GetCampaignGoalSettingByIdAsync(request);

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
    public async Task<bool> CheckCampaignGoalSettingByNameExistAsync([FromBody] CampaignGoalSettingNameRequestModel request)
    {
        var result = await _campaignGoalSettingService.CheckCampaignGoalSettingByNameExistAsync(request);

        return result;
    }       
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpsertCampaignGoalSettingAsync([FromBody] CampaignGoalSettingRequestModel request)
    {
        var result = await _messagePublisherService.UpsertCampaignGoalSettingAsync(request);

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
    public async Task<ResponseModel> GetCampaignGoalSettingByIdDetailsAsync([FromBody] CampaignGoalSettingIdRequestModel request)
    {
        var result = await _messagePublisherService.GetCampaignGoalSettingByIdDetailsAsync(request);

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

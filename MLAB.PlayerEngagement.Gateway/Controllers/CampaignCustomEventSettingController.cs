using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CampaignCustomEventSettingController : ControllerBase
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly ICampaignCustomEventSettingService _campaignCustomEventSettingService;
    public CampaignCustomEventSettingController(IMessagePublisherService messagePublisherService, ICampaignCustomEventSettingService campaignCustomEventSettingService)
    {
        _messagePublisherService = messagePublisherService;
        _campaignCustomEventSettingService = campaignCustomEventSettingService;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignCustomEventSettingByFilterAsync([FromBody] CampaignCustomEventSettingRequestModel request)
    {
        var result = await _messagePublisherService.GetCampaignCustomEventSettingByFilterAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddCampaignCustomEventSettingAsync([FromBody] CampaignCustomEventSettingModel request)
    {
        var result = await _messagePublisherService.AddCampaignCustomEventSettingAsync(request);

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
    public async Task<ResponseModel> ValidatePlayerConfigurationRecordAsync([FromBody] CampaignCustomEventSettingRequestModel request)
    {
        var hasDuplicate = await _campaignCustomEventSettingService.CheckExistingCampaignCustomEventSettingByFilterAsync(request);

        if (hasDuplicate)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Unable to proceed, ID or Name already exists."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }
}

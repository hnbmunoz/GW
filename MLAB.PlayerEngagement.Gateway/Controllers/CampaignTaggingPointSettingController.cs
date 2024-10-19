using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CampaignTaggingPointSettingController : BaseController
{
    private readonly ICampaignTaggingPointSettingService _campaignSettingService;

    public CampaignTaggingPointSettingController(ICampaignTaggingPointSettingService campaignSettingService)
    {
        _campaignSettingService = campaignSettingService;
    }


    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignSettingTypeSelectionAsync(int masterReferenceId, int masterReferenceIsParent)
    {
        var result = await _campaignSettingService.GetCampaignSettingTypeSelectionAsync(masterReferenceId, masterReferenceIsParent);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignSettingListAsync([FromBody] AutoTaggingPointIncentiveFilterRequestModel request)
    {
        return await GetResultAsync(await _campaignSettingService.GetCampaignSettingListAsync(request));
    }


    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetAutoTaggingDetailsByIdAsync([FromBody] AutoTaggingFilterByIdRequestModel request)
    {
        var result = await _campaignSettingService.GetAutoTaggingDetailsByIdAsync(request);

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
    public async Task<ResponseModel> GetPointIncentiveDetailsByIdAsync([FromBody] AutoTaggingFilterByIdRequestModel request)
    {
        var result = await _campaignSettingService.GetPointIncentiveDetailsByIdAsync(request);

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
    public async Task<ResponseModel> AddAutoTaggingListAsync([FromBody] AutoTaggingDetailsRequestModel request)
    {
        var result = await _campaignSettingService.AddAutoTaggingListAsync(request);
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
    public async Task<ResponseModel> AddPointIncentiveSettingAsync([FromBody] AddPointIncentiveDetailsRequestModel request)
    {
        var result = await _campaignSettingService.AddPointIncentiveSettingAsync(request);
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
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaggingSegmentAsync()
    {
        var result = await _campaignSettingService.GetTaggingSegmentAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersByModuleAsync(int subMainModuleDetailId)
    {
        var result = await _campaignSettingService.GetUsersByModuleAsync(subMainModuleDetailId);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> CheckCampaignSettingByNameIfExistAsync([FromBody] CampaignSettingNameRequestModel request)
    {
        var result = await _campaignSettingService.CheckCampaignSettingByNameIfExistAsync(request);

        return result;
    }

}

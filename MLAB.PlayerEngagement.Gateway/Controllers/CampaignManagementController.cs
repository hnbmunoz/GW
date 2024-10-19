using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CampaignManagementController : BaseController
{

    private readonly ICampaignManagementService _campaignManagementService;
    private readonly IMessagePublisherService _messagePublisherService;
            
    public CampaignManagementController(ICampaignManagementService campaignManagementService, IMessagePublisherService messagePublisherService)
    {
        _campaignManagementService = campaignManagementService;
        _messagePublisherService = messagePublisherService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCampaignAsync()
    {
        var result = await _campaignManagementService.GetAllCampaignAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignLookupByFilterAsync([FromBody] CampaignLookupByFilterRequestModel request)
    {
        var result = await _campaignManagementService.GetCampaignByFilterAsync(request);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCampaignType()
    {
        var result = await _campaignManagementService.GetAllCampaignType();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignListAsync([FromBody] CampaignListRequestModel request)
    {

        var test = AppDomain.CurrentDomain.BaseDirectory;
        var result = await _messagePublisherService.GetCampaignListAsync(request);

        return result == true ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCampaignStatusAsync()
    {
        var result = await _campaignManagementService.GetAllCampaignStatusAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSurveyTemplateAsync()
    {
        var result = await _campaignManagementService.GetAllSurveyTemplateAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSegmentAsync()
    {
        var result = await _campaignManagementService.GetAllSegmentAsync();
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentWithSegmentTypeNameAsync()
    {
        var result = await _campaignManagementService.GetSegmentWithSegmentTypeNameAsync();
        return Ok(result);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentationByIdAsync(int segmentationId)
    {
        var result = await _campaignManagementService.GetSegmentationByIdAsync(segmentationId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignGoalSettingListAsync()
    {
        var result = await _campaignManagementService.GetCampaignGoalSettingListAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLeaderValidationAsync()
    {
        var result = await _campaignManagementService.GetAllLeaderValidation();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignGoalSettingByIdAsync(int campaignGoalSettingId)
    {
        var result = await _campaignManagementService.GetCampaignGoalSettingByIdAsync(campaignGoalSettingId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutoTaggingDetailsByIdAsync(int campaignSettingId)
    {
        var result = await _campaignManagementService.GetAutoTaggingDetailsByIdAsync(campaignSettingId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveCampaignAsync([FromBody] CampaignModel request)
    {
        var validation = await _campaignManagementService.ValidateCampaign(request);

        if(validation.Item1 != StatusCodes.Status200OK)
        {
            return new ResponseModel((int)validation.Item1, validation.Item2);
        }

        var result = await _messagePublisherService.SaveCampaignAsync(request);

        return result == true ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignByIdAsync([FromBody] CampaignIdModel request)
    {
        var result = await _messagePublisherService.GetCampaignByIdAsync(request);

        return result == true ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignConfigurationSegmentByIdAsync(long segmentId, long? varianceId)
    {
        var result = await _campaignManagementService.GetCampaignConfigurationSegmentByIdAsync(segmentId, varianceId);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignSystemLookUpAsync()
    {
        var result = await _campaignManagementService.GetCampaignGoalParametersAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserByModuleAsync()
    {
        var result = await _campaignManagementService.GetCampaignGoalParametersAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEligibilityTypeAsync()
    {
        var result = await _campaignManagementService.GetEligibilityTypeAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSearchFilterAsync()
    {
        var result = await _campaignManagementService.GetSearchFilterAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCampaignBySearchFilterAsync(int searchFilterType, string searchFilterField, int campaignType)
    {
        var result = await _campaignManagementService.GetAllCampaignBySearchFilterAsync(searchFilterType, searchFilterField, campaignType);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [RequestSizeLimit(5242880)]
    public async Task<ResponseModel> ValidateImportPlayersAsync([FromBody] CampaignImportPlayerRequestModel request)
    {
        var result = await _messagePublisherService.ValidateImportRetentionPlayerAsync(request);

        return result == true ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [RequestSizeLimit(5242880)]
    public async Task<ResponseModel> ProcessCampaignImportPlayersAsync([FromBody] CampaignImportPlayerRequestModel request)
    {
        var result = await _messagePublisherService.ProcessCampaignImportPlayersAsync(request);

        return result == true ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignUploadPlayerListAsync([FromBody] UploadPlayerFilterModel request)
    {
        var result = await _messagePublisherService.GetCampaignUploadPlayerListAsync(request);

        return result == true ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> RemoveCampaignImportPlayersAsync([FromBody] CampaignImportPlayerModel request)
    {
        var result = await _messagePublisherService.RemoveCampaignImportPlayersAsync(request);

        return result == true ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> GetExportCampaignUploadPlayerListAsync([FromBody] UploadPlayerFilterModel request)
    {

        try
        {
            var result = await _campaignManagementService.GetExportCampaignUploadPlayerListAsync(request);

            var sb = new StringBuilder();
            sb.Append("Player ID, Username, Brand, Status, last deposit date, last deposit amount, bonus abuser, last bet product, last bet date").Append("\r\n");
            int index = 1;
            foreach (var p in result)
            {
                sb.Append($"{p.PlayerId},{p.Username},{p.Brand},{p.Status},{p.LastDepositDate},{p.LastDepositAmount},{p.BonusAbuser},{p.LastBetProduct},{p.LastBetDate}");

                sb.Append("\r\n");
                index++;
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Campaign_Players_Results.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Campaign_Players_Results.csv");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCampaignCustomEventSettingNameAsync()
    {
        var result = await _campaignManagementService.GetAllCampaignCustomEventSettingNameAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateHasPlayerInCampaignAsync(int campaignId, string campaignGuid)
    {
        var result = await _campaignManagementService.ValidateHasPlayerInCampaignAsync(campaignId, campaignGuid);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignPeriodBySourceId(long sourceID)
    {
        var result = await _campaignManagementService.GetCampaignPeriodBySourceId(sourceID);
        return Ok(result);
    }
}

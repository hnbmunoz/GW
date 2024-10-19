using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.Segmentation;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;
using System.Text;
using MLAB.PlayerEngagement.Core.Extensions;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SegmentController : BaseController
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly ISegmentationService _segmentationService;

    public SegmentController(IMessagePublisherService messagePublisherService, ISegmentationService segmentationService)
    {
        _messagePublisherService = messagePublisherService;
        _segmentationService = segmentationService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSegmentationByFilterAsync(SegmentationRequestModel request)
    {
        return await GetResultAsync(await _messagePublisherService.GetSegmentationByFilterAsync(request));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> TestSegmentAsync(SegmentationTestModel request)
    {
        return await GetResultAsync(await _messagePublisherService.TestSegmentAsync(request));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TestStaticSegmentAsync(SegmentationTestModel request)
    {
        try
        {
            var result = await _segmentationService.TestStaticSegmentAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> DeactivateSegmentAsync(int SegmentId, int userId)
    {
        var result = await _segmentationService.DeactivateSegmentAsync(SegmentId, userId);
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
    public async Task<IActionResult> GetSegmentConditionFilterFieldsAsync()
    {
        try
        {
            var result = await _segmentationService.GetSegmentConditionFieldsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentConditionFilterOperatorsAsync()
    {
        try
        {
            var result = await _segmentationService.GetSegmentConditionOperatorsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveSegmentAsync([FromBody] SegmentationModel request)
    {
        return await GetResultAsync(await _messagePublisherService.SaveSegmentAsync(request));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentByIdAsync(int segmentId)
    {
        try
        {
            var result = await _segmentationService.GetSegmentByIdAsync(segmentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ToStaticSegmentAsync(SegmentationToStaticModel request)
    {
        return await GetResultAsync(await _messagePublisherService.ToStaticSegmentAsync(request));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConvertToStaticSegmentAsync(SegmentationToStaticModel request)
    {
        try
        {
            var result = await _segmentationService.ToStaticSegmentation(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateSegmentAsync(int? segmentId, string segmentName)
    {

        var isExist = await _segmentationService.ValidateSegmentAsync(segmentId, segmentName);
        if (isExist)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Segment Name already exists."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> ExportToCSVAsync(SegmentationTestModel request)
    {
        try
        {
            var result = await _segmentationService.TestSegmentAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("Id, PlayerId, UserName, BrandName, CurrencyName, VipLevelName, AccountStatus, RegistrationDate").Append("\r\n");
            int index = 1;
            foreach (var p in result)
            {
                sb.Append($"{index}, {p.PlayerId}, {p.UserName}, {p.BrandName}, {p.CurrencyName}, {p.VipLevelName}, {p.AccountStatus}, {p.RegistrationDate?.ToMlabExportDateString()}");
                sb.Append("\r\n");
                index++;

            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Test_Segment_Result.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Test_Segment_ERROR.csv");
        }

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpSertSegmentAsync([FromBody] SegmentationModel request)
    {
        var result = await _messagePublisherService.UpsertSegmentAsync(request);
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
    public async Task<IActionResult> GetSegmentConditionSetByParentIdAsync(int ParentSegmentConditionFieldId)
    {
        try
        {
            var result = await _segmentationService.GetSegmentConditionSetByParentIdAsync(ParentSegmentConditionFieldId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignGoalNamesByCampaignIdAsync(int CampaignId)
    {
        try
        {
            var result = await _segmentationService.GetCampaignGoalNamesByCampaignIdAsync(CampaignId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVariancesBySegmentIdAsync(int SegmentId)
    {
        try
        {
            var result = await _segmentationService.GetVariancesBySegmentIdAsync(SegmentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentLookupsAsync()
    {
        try
        {
            var result = await _segmentationService.GetSegmentLookupsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetSegmentDistributionByFilterAsync([FromBody] SegmentDistributionByFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetSegmentDistributionByFilterAsync(request);
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
    public async Task<IActionResult> ValidateInFilePlayersAsync(ValidateInFileRequestModel request)
    {
        try
        {
            var result = await _segmentationService.ValidateInFilePlayersAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TriggerVarianceDistributionAsync([FromBody] TriggerVarianceDistributionRequestModel request)
    {
        try
        {
            var result = await _segmentationService.TriggerVarianceDistributionAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> VarianceDistributionExportToCSVAsync([FromBody]  SegmentDistributionByFilterRequestModel request)
    {
        try
        {
            var result =  await _segmentationService.GetVarianceDistributionForCSVAsync(request);

            StringBuilder sb = new StringBuilder();
            //sb.Append("Id, PlayerId, UserName, BrandName, CurrencyName, VipLevelName, AccountStatus, RegistrationDate").Append("\r\n");
            sb.Append("Id, PlayerId, UserName, BrandName, CurrencyName, VipLevelName, AccountStatus, RegistrationDate, VarianceName").Append("\r\n");
            int index = 1;
            foreach (var p in result.SegmentDistributions)
            {
                sb.Append($"{index}, {p.PlayerId}, {p.UserName}, {p.BrandName}, {p.CurrencyName}, {p.VIPLevelName}, {p.AccountStatus}, {p.RegistrationDate.ToMlabExportDateString()}, {p.VarianceName}");
                sb.Append("\r\n");
                index++;

            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Variance_Distribution_Result.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Variance_Distribution_ERROR.csv");
        }

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentConditionsBySegmentIdAsync(int SegmentId)
    {
        try
        {
            var result = await _segmentationService.GetSegmentConditionsBySegmentIdAsync(SegmentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageStatusByCaseTypeIdAsync(string CaseTypeId)
    {
        try
        {
            var result = await _segmentationService.GetMessageStatusByCaseTypeIdAsync(CaseTypeId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageResponseByMultipleIdAsync(string MessageStatusId)
    {
        try
        {
            var result = await _segmentationService.GetMessageResponseByMultipleIdAsync(MessageStatusId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateCustomQueryAsync(string customQuery)
    {

        var response = await _segmentationService.ValidateCustomQueryAsync(customQuery);
        if (!response.IsValid)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = response.Message
            };
        }
        else
        {
            return new ResponseModel();
        }
    }
}
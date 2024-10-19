using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.Player;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;
using System.Text;
using MLAB.PlayerEngagement.Core.Models.Player.Request;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class PlayerController : BaseController
{

    private readonly IMessagePublisherService _messagePublisherService;
    private readonly IPlayerManagementService _playerService;
    private readonly ISegmentationService _segmentationService;

    public PlayerController(IMessagePublisherService messagePublisherService, IPlayerManagementService playerService, ISegmentationService segmentationService)
    {
        _messagePublisherService = messagePublisherService;
        _playerService = playerService;
        _segmentationService = segmentationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [RequestSizeLimit(5242880)]
    public async Task<ResponseModel> ValidateImportPlayersAsync([FromBody] ImportPlayersRequestModel request)
    {
        var result = await _messagePublisherService.ValidateImportPlayerAsync(request);

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
    [RequestSizeLimit(5242880)]
    public async Task<ResponseModel> ImportPlayersAsync([FromBody] ImportPlayersRequestModel request)
    {
        var result = await _messagePublisherService.ImportPlayerAsync(request);

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
    public async Task<ResponseModel> GetPlayerAsync([FromBody] PlayerByIdRequestModel request)
    {
        var result = await _playerService.GetPlayerAsync(request);

        if (result == null)
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");

        var responseModel = new ResponseModel
        {
            Data = result
        };

        return responseModel;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlayerCampaignLookupsAsync(int? campaignType)
    {
        try
        {
            var result = await _playerService.GetPlayerCampaignLookupsAsync(campaignType);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlayersAsync([FromBody] PlayerFilterRequestModel request)
    {
        try
        {
            var result = await _playerService.GetPlayersAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SavePlayerContactAsync([FromBody] PlayerContactRequestModel request)
    {
        //if (!isSuccess)
        //    return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        try
        {
            var result = await _playerService.SavePlayerContactAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlayerCasesAsync([FromBody] PlayerCaseRequestModel request)
    {
        try
        {
            var result = await _playerService.GetPlayerCasesAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetViewContactLogListAsync([FromBody] ContactLogListRequestModel request)
    {
        var result = await _messagePublisherService.GetViewContactLogListAsync(request);

        if (result == true)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    
        //}
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetViewContactLogTeamListAsync([FromBody] ContactLogListRequestModel request)
    {
        try
        {
            var result = await _playerService.GetViewContactLogTeamListAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetViewContactLogUserListAsync([FromBody] ContactLogListRequestModel request)
    {
    
        try
        {
            var result = await _playerService.GetViewContactLogUserListAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> ExportToCsvSummaryContactLogAsync([FromBody] ContactLogListRequestModel request)
    {
        try
        {
            var result =  await _playerService.GetViewContactLogListAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("Team Name, Total Unique User, Total Unique Player").Append("\r\n");
            int index = 1;
            foreach (var p in result.ContactLogSummaryList)
            {
                sb.Append($"{p.TeamName}, {p.TotalUniqueUserCount}, {p.TotalUniquePlayerCount}");

                sb.Append("\r\n");
                index++;
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Contact_Logs_Summary.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Contact_Logs_Summary.csv");
        }

    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> ExportToCsvTeamContactLogAsync([FromBody] ContactLogListRequestModel request)
    {
        try
        {
            var result = await _playerService.GetViewContactLogTeamListAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("User Full Name, Total Click Mobile Number, Total Click Email Address, Total Unique Player").Append("\r\n");
            int index = 1;
            foreach (var p in result.ContactLogTeamList)
            {
                sb.Append($"{p.UserFullName}, {p.TotalClickMobileCount}, {p.TotalClickEmailCount}, {p.TotalUniquePlayerCount}");

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
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> ExportToCsvUserContactLogAsync([FromBody] ContactLogListRequestModel request)
    {
        try
        {
            var result = await _playerService.GetViewContactLogUserListAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("User Full Name, Player Username, Brand, Currency, VIP Level, Action Date, Viewed Data").Append("\r\n");
            int index = 1;
            foreach (var p in result.ContactLogUserList)
            {
                sb.Append($"{p.UserFullName}, {p.PlayerUserName}, {p.Brand}, {p.Currency}, {p.VipLevel}, {p.ActionDate}, {p.ViewData}");

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
    public async Task<IActionResult> GetManageThresholdsAsync()
    {
        try
        {
            var result = await _playerService.GetManageThresholdsAsync();
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [RequestSizeLimit(5242880)]
    public async Task<ResponseModel> SaveManageThresholdAsync([FromBody] SaveManageThresholdRequest request)
    {
        var result = await _messagePublisherService.SaveManageThresholdAsync(request);

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
    public async Task<IActionResult> GetPlayerSensitiveDataAsync([FromBody] PlayerSensitiveDataRequestModel request)
    {
        try
        {
            var result = await _playerService.GetPlayerSensitiveDataAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

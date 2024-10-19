using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagementIntegration;
using System.Text;
using System.Globalization;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Extensions;
using static MassTransit.ValidationResultExtensions;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RelationshipManagementController : ControllerBase
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly IRelationshipManagementService _remProfileService;
    private readonly IRemIntegrationPublisherService _remIntegrationPublisherService;


    public RelationshipManagementController(IMessagePublisherService messagePublisherService,
        IRelationshipManagementService remProfileService,
        IRemIntegrationPublisherService remIntegrationPublisherService)
    {
        _messagePublisherService = messagePublisherService;
        _remProfileService = remProfileService;
        _remIntegrationPublisherService = remIntegrationPublisherService;
    }

    #region RemProfile
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetRemProfileByFilterAsync([FromBody] RemProfileFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetRemProfileByFilterAsync(request);

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
    public async Task<ResponseModel> GetRemProfileByIdAsync([FromBody] RemProfileFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetRemProfileByIdAsync(request);

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
    public async Task<ResponseModel> UpdateRemProfileStatus(UpdateRemProfileRequestModel request)
    {

        var result = await _remProfileService.UpdateRemProfileStatus(request);

        if (result)
        {
            await _remIntegrationPublisherService.SendUpdateRemProfile(
                new RemProfileEventRequestModel()
                {
                    RemProfileId = request.RemProfileID.Value,
                    UserId = request.UserId.Value.ToString(),
                    QueueId = Guid.NewGuid().ToString()

                });

            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateRemOnlineStatus(UpdateRemProfileRequestModel request)
    {
        var result = await _remProfileService.UpdateRemProfileStatus(request);

        if (result)
        {
            await _remIntegrationPublisherService.SendSetOnlineStatus(
                new RemOnlineStatusRequestModel()
                {
                    OnlineStatus = request.OnlineStatus,
                    RemProfileId = request.RemProfileID.Value,
                    UserId = request.UserId.Value.ToString(),
                    QueueId = Guid.NewGuid().ToString(),
                    Timestamp = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)

        });

            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRemDistributionPlayerAsync([FromBody] RemProfileFilterRequestModel request)
    {
        try
        {
            var result = await _remProfileService.GetRemDistributionPlayerAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidateRemProfileNameAsync(ValidateRemProfileNameRequestModel request)
    {
        try
        {
            var result = await _remProfileService.ValidateRemProfileNameAsync(request);

            return result;
        }
        catch (Exception)
        {

            return false;
        }

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<RemProfileReusableResponseModel>> GetReusableRemProfileDetails()
    {
        var result = await _remProfileService.GetReusableRemProfileDetails();

        return result;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpSertRemProfileAsync([FromBody] RemProfileDetailsRequestModel request)
    {
        var result = await _messagePublisherService.UpSertRemProfileAsync(request);

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
    public async Task<ResponseModel> ValidateRemProfileIfHasPlayer([FromBody] UpdateRemProfileRequestModel request)
    {
        var result = await _remProfileService.ValidateRemProfileIfHasPlayer(request.RemProfileID.Value);

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
    public async Task<FileContentResult> ExportRemProfileToCsv(RemProfileFilterRequestModel request)
    {
        try
        {
            var result = await _remProfileService.GetRemProfileByFilterAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("ReM Profile ID, ReM Profile Name, Agent Name, Pseudo Name, Online Status, ReM Profile Status").Append("\r\n");
            int index = 1;
            foreach (var p in result.Item2)
            {
                sb.Append($"{p.RemProfileID}, {p.RemProfileName}, {p.AgentName}, {p.PseudoNamePP}, {p.OnlineStatus}, {p.AgentConfigStatus}");
                sb.Append("\r\n");
                index++;

            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", $"ReM Profile {DateTime.Now.ToString("dd/MM/yyyy")}.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Rem_Profile_ERROR.csv");
        }

    }
    #endregion

    #region RemDistribution
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetRemDistributionByFilterAsync([FromBody] RemDistributionFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetRemDistributionByFilterAsync(request);

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
    public async Task<ResponseModel> UpsertRemDistributionAsync([FromBody] UpsertRemDistributionRequestModel[] request)
    {

        var result = await _remProfileService.UpsertRemDistributionAsync(request);

        if (result == true)
        {
            foreach (var item in request)
            {
                if (item.HasIntegration == 1) { 
                    var remDistributionEventRequestModel = new RemDistributionEventRequestModel()
                    {
                        RemProfileId = item.RemProfileId,
                        UserId = item.UpdatedBy.ToString(),
                        PlayerID = item.PlayerId,
                        QueueId = Guid.NewGuid().ToString(),
                        timestamp = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)
                    };

                    if (item.RemDistributionId == 0) 
                    {
                        remDistributionEventRequestModel.AssignAction = (int)RemIntegrationEvents.Assign;
                        await _remIntegrationPublisherService.SendAssignPlayerToRemProfile(remDistributionEventRequestModel);
                    }
                    else 
                    {
                        remDistributionEventRequestModel.AssignAction = (int)RemIntegrationEvents.Reassign;
                        await _remIntegrationPublisherService.SendReassignPlayerFromRemProfile(remDistributionEventRequestModel);
                    }
                }
            }    
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> RemoveRemDistributionAsync(RemoveRemDistributionRequestModel request)
    {

        var result = await _remProfileService.RemoveRemDistributionAsync(request.RemDistributionId, request.UserId);

        if (result == true)
        {
            if (request.HasIntegration == 1)
            {
                await _remIntegrationPublisherService.SendRemovePlayerFromRemProfile(
            
                new RemDistributionEventRequestModel()
                {
                    RemProfileId = request.RemProfileId,
                    UserId = request.UserId.ToString(),
                    MlabPlayerId = request.MlabPlayerId,
                    QueueId = Guid.NewGuid().ToString(),
                    AssignAction = (int)RemIntegrationEvents.Remove,
                    timestamp = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)
                });
            }
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> ExportRemDistributionToCsv(RemDistributionFilterRequestModel request)
    {
        try
        {
            var result = await _remProfileService.GetRemDistributionByFilterAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("Player Id, VIP Level, Username, Status, Brand, Currency, Rem Profile Name, Agent Name, Pseudo Name, Previous ReM Agent Name, Assign Status, Distribution Date, Assigned By").Append("\r\n");
            int index = 1;
            foreach (var p in result.RemDistributionList)
            {
                sb.Append($"{p.PlayerId}, {p.VipLevel}, {p.Username}, {p.PlayerStatus}, {p.Brand}, {p.Currency}, {p.RemProfileName}, {p.AgentName}, {p.PseudoName}, {p.PrevReMProfileName}, {p.AssignStatus}, {p.DistributionDate?.ToMlabExportDateString()}, {p.AssignedBy}");
                sb.Append("\r\n");
                index++;

            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", $"Rem Distribution {DateTime.Now.ToString("dd/MM/yyyy")}.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Rem_Distribution_ERROR.csv");
        }

    }
    #endregion

    #region RemSetting
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScheduleTemplateSettingListAsync([FromBody] ScheduleTemplateListRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.GetScheduleTemplateSettingListAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScheduleTemplateSettingByIdAsync([FromBody] ScheduleTemplateByIdRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.GetScheduleTemplateSettingByIdAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetScheduleTemplateLanguageSettingListAsync([FromBody] ScheduleTemplateLanguageRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.GetScheduleTemplateLanguageSettingListAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SaveScheduleTemplateSettingAsync([FromBody] SaveScheduleTemplateRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.SaveScheduleTemplateSettingAsync(request);
            
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateTemplateSettingAsync(ValidateTemplateRequestModel request)
    {

        var result = await _remProfileService.ValidateTemplateSettingAsync(request);

        if (result == false)
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
    public async Task<FileContentResult> ExportRemSettingToCsv(ScheduleTemplateListRequestModel request)
    {
        try
        {
            var result = await _remProfileService.GetScheduleTemplateSettingListAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("Schedule Template Name, Created Date, Created By, Last Modified Date, Last Modified By").Append("\r\n");
            int index = 1;
            foreach (var p in result.ScheduleTemplateResponseList)
            {
                sb.Append($"{p.ScheduleTemplateName}, {p.CreatedDate?.ToMlabExportDateString()}, {p.CreatedByName}, {p.UpdatedDate?.ToMlabExportDateString()}, {p.UpdatedByName}");
                sb.Append("\r\n");
                index++;

            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", $"Rem History {DateTime.Now.ToString("dd/MM/yyyy")}.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Rem_Profile_ERROR.csv");
        }

    }

    #endregion

    #region RemHistory
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetRemHistoryByFilterAsync([FromBody] RemHistoryFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetRemHistoryByFilterAsync(request);

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
    public async Task<FileContentResult> ExportRemHistoryToCsv(RemHistoryFilterRequestModel request)
    {
        try
        {
            var result = await _remProfileService.GetRemHistoryByFilterAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("Username, Action Type, Assignment Date, ReM Profile Name, Agent Name, Pseudo Name").Append("\r\n");
            int index = 1;
            foreach (var p in result.RemHistoryList)
            {
                sb.Append($"{p.Username}, {p.ActionType}, {p.AssignmentDate?.ToMlabExportDateString()}, {p.RemProfileName}, {p.AgentName}, {p.PseudoName}");
                sb.Append("\r\n");
                index++;

            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", $"Rem History {DateTime.Now.ToString("dd/MM/yyyy")}.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Rem_Profile_ERROR.csv");
        }

    }
    #endregion

    #region ReMAutoDistributionSetting
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetAutoDistributionSettingConfigsListByFilterAsync([FromBody] AutoDistributionSettingFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetAutoDistributionSettingConfigsListByFilterAsync(request);

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
    public async Task<ResponseModel> GetAutoDistributionSettingAgentsListByFilterAsync([FromBody] AutoDistributionSettingFilterRequestModel request)
    {
        var result = await _messagePublisherService.GetAutoDistributionSettingAgentsListByFilterAsync(request);

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
    public async Task<IActionResult> UpdateMaxPlayerCountConfigAsync(UpdateMaxPlayerCountConfigRequestModel request)
    {
        try
        {
            var result = await _remProfileService.UpdateMaxPlayerCountConfigAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred: " + ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRemovedVipLevels()
    {
        try
        {
            var result = await _remProfileService.GetRemovedVipLevels();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAutoDistributionConfigListOrderAsync()
    {
        try
        {
            var result = await _remProfileService.GetAllAutoDistributionConfigListOrderAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveDistributionbyVipLevelAsync(RemoveDistributionByVIPLevelRequestModel request)
    {
        try
        {
            var result = await _remProfileService.RemoveDistributionbyVipLevelAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred: " + ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAutoDistributionSettingPriorityAsync(UpdateAutoDistributionSettingPriorityRequestModel request)
    {
        try
        {
            var result = await _remProfileService.UpdateAutoDistributionSettingPriorityAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred: " + ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAutoDistributionConfigurationStatusAsync(UpdateAutoDistributionConfigStatusRequestModel request)
    {
        try
        {
            var result = await _remProfileService.UpdateAutoDistributionConfigurationStatusAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred: " + ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAutoDistributionConfigurationByIdAsync(int autoDistributionSettingId)
    {
        try
        {
            var result = await _remProfileService.DeleteAutoDistributionConfigurationByIdAsync(autoDistributionSettingId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred: " + ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> ValidateAutoDistributionConfigurationNameAsync(int autoDistributionSettingId, string configurationName)
    {
        try
        {
            var result = await _remProfileService.ValidateAutoDistributionConfigurationNameAsync(autoDistributionSettingId, configurationName);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutoDistributionConfigurationCountAsync(int userId)
    {
        try
        {
            var result = await _remProfileService.GetAutoDistributionConfigurationCountAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred: " + ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SaveAutoDistributionConfigurationAsync([FromBody] AutoDistributionConfigurationRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.SaveAutoDistributionConfigurationAsync(request);

            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutoDistributionConfigurationDetailsByIdAsync([FromBody] AutoDistributionConfigurationByIdRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.GetAutoDistributionConfigurationDetailsByIdAsync(request);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutoDistributionConfigurationListByAgentIdAsync(AutoDistributionConfigurationListByAgentIdRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.GetAutoDistributionConfigurationListByAgentIdAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    #endregion

    #region Shared
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRemLookupsAsync()
    {
        try
        {
            var result = await _remProfileService.GetRemLookupsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllScheduleTemplateListAsync()
    {
        try
        {
            var result = await _remProfileService.GetAllScheduleTemplateListAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageTypeChannelListAsync()
    {
        try
        {
            var result = await _remProfileService.GetMessageTypeChannelListAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    #endregion
}

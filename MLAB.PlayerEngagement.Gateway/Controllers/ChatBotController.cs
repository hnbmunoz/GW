using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models.ChatBot;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Gateway.Attributes;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ChatBotController : BaseController
{
    private readonly IChatbotService _chatbotService;
    public ChatBotController(IChatbotService chatbotService)
    {
        _chatbotService = chatbotService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseAndPlayerInformationResponse))]
    [ModulePermissionAttribute(ModulePermissions.CaseManagement_Permission_Read)]
    public async Task<IActionResult> GetCaseAndPlayerInformationByParam([FromBody] CaseAndPlayerInformationRequest request)
    {
        try
        {
            var result = await _chatbotService.GetCaseAndPlayerInformationByParamAsync(request);
            return (result == null) ? StatusCode(200, new Object() { }) : StatusCode(result.ErrorCode, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerTransactionResponse))]
    [ModulePermissionAttribute(ModulePermissions.CaseManagement_Permission_Read)]
    public async Task<IActionResult> GetPlayerTransactionDataByParam([FromBody] PlayerTransactionRequest request)
    {
        try
        {
            try
            {
                var result = await _chatbotService.GetPlayerTransactionDataByParamAsync(request);
                return (result == null) ? StatusCode(200, new Object() { }) : StatusCode(result.ErrorCode, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ASWDetailResponse))]
    [ModulePermissionAttribute(ModulePermissions.CaseManagement_Permission_Write)]
    public async Task<IActionResult> SubmitAswDetail([FromBody] ASWDetailRequest request)
    {
        try
        {
            var userId = UserId != null ? Int64.Parse(UserId) : 0;
            request.UserId = userId;

            var result = await _chatbotService.SubmitAswDetail(request);
            return (result == null) ? StatusCode(400, new Object() { }) : StatusCode(result.ErrorCode, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChatbotStatusResponse))]
    [ModulePermissionAttribute(ModulePermissions.CaseManagement_Permission_Write)]
    public async Task<IActionResult> SetCaseStatus([FromBody] SetStatusRequest request)
    {
        try
        {
            var userId = UserId;
            var result = await _chatbotService.SetCaseStatusAsync(request, userId != null ? Int64.Parse(userId) : null);
            return (result == null) ? StatusCode(400, new Object() { }) : StatusCode(result.ErrorCode, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TopicResponse))]
    [ModulePermissionAttribute(ModulePermissions.CaseManagement_Permission_Write)]
    public async Task<IActionResult> GetTopic(string currency, string language)
    {
        try
        {
            var result = await _chatbotService.GetTopicAsync(currency, language);
            return (result == null) ? StatusCode(200, new Object() { }) : StatusCode(result.First().ErrorCode, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubTopicResponse))]
    [ModulePermissionAttribute(ModulePermissions.CaseManagement_Permission_Write)]
    public async Task<IActionResult> GetSubTopic(int topicID, string currency, string language)
    {
        try
        {
            var result = await _chatbotService.GetSubTopicAsync(topicID, currency, language);
            return (result == null) ? StatusCode(200, new Object() { }) : StatusCode(result.First().ErrorCode, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Application.Responses;
using System.Net;
using MLAB.PlayerEngagement.Core.Models.Option.Request;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AgentSurveyWidgetController : ControllerBase
{
    private readonly ISurveyAgentWidgetService _surveyAgentWidgetService;
    private readonly IMessagePublisherService _messagePublisherService;

    public AgentSurveyWidgetController(ISurveyAgentWidgetService surveyAgentWidgetService, IMessagePublisherService messagePublisherService)
    {
        _surveyAgentWidgetService = surveyAgentWidgetService;
        _messagePublisherService = messagePublisherService;
    }
    [HttpPost]
    public async Task<IActionResult> ValidateUserAsync([FromBody] UserValidationRequest request, string platform)
    {
        try
        {
            var verifyResponse = await _surveyAgentWidgetService.UserValidationAsync(request, platform);
            if(verifyResponse != null)
            {
                if (!verifyResponse.IsValidUser)
                {
                    return NotFound(new { message = "Member not Found." });
                }
                else if (!verifyResponse.IsDataAccessible)
                {
                    return NotFound(new { message = "Insufficient permission to get Player's data." });
                }
                else if (!verifyResponse.IsValidBrand)
                {
                    return NotFound(new { message = "Skill or Brand not found." });
                }
            }
            return Ok(verifyResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAgentSurveyByConversationIdAsync(string conversationId, string platform)
    {
        try
        {
            var verifyResponse = await _surveyAgentWidgetService.GetAgentSurveyByConversationIdAsync(conversationId, platform);
           
            if(verifyResponse == null)
                return NotFound(new { message = "Conversation not Found." });

            return Ok(verifyResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBrandBySkillNameAsync(string skillName, string licenseId, string platform)
    {
        try
        {
            var result = await _surveyAgentWidgetService.GetBrandBySkillNameAsync(skillName, licenseId, platform);

            if (result == null)
                return NotFound(new { message = "Conversation not Found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetLanguageOptionAsync(string platform)
    {
        try
        {
            var verifyResponse = await _surveyAgentWidgetService.GetLanguageOptionAsync(platform);

            return Ok(verifyResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }        
    [HttpGet]
    public async Task<IActionResult>  GetSubtopicnameByIdAsync(long topicLanguageId, string currencyCode, long languageId, string platform)
    {
        try
        {
            var verifyResponse = await _surveyAgentWidgetService.GetSubtopicnameByIdAsync(topicLanguageId,  currencyCode, languageId, platform);

            return Ok(verifyResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetTopicNameByCodeAsync(string languageCode, string currencyCode, string platform)
    {
        try
        {
            var verifyResponse = await _surveyAgentWidgetService.GetTopicNameByCodeAsync(languageCode, currencyCode, platform);

            return Ok(verifyResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpsertAgentSurveyAsync([FromBody] AgentSurveyRequest request, string platform)
    {
        var result = await _messagePublisherService.UpserSertAgentSurveyAsync(request, platform);

        return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetASWFeedbackTypeOptionList(string platform)
    {
        var result = await _surveyAgentWidgetService.GetASWFeedbackTypeOptionList(platform);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetASWFeedbackCategoryOptionById(int feedbackTypeId, string platform)
    {
        var result = await _surveyAgentWidgetService.GetASWFeedbackCategoryOptionById(feedbackTypeId, platform);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetASWFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform)
    {
        var result = await _surveyAgentWidgetService.GetASWFeedbackAnswerOptionById(request, platform);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSkillDetailsBySkillID(string skillId, string licenseId, string platform)
    {
        try
        {
            var result = await _surveyAgentWidgetService.GetSkillDetailsBySkillIDAsync(skillId, licenseId, platform);

            if (result == null)
                return NotFound(new { message = "Skill or Brand not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetLiveChatLicenseID(string platform)
    {
        try
        {
            var result = await _surveyAgentWidgetService.GetLiveChatLicenseIDAsync(platform);

            if (result == null)
                return NotFound(new { message = "License ID not Found." });

            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllActiveCampaignByUsername(string username, string platform)
    {
        try
        {
            var result = await _surveyAgentWidgetService.GetAllActiveCampaignByUsername(username, platform);

            if (result == null)
                return NotFound(new { message = "Campaign Name not Found." });

            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

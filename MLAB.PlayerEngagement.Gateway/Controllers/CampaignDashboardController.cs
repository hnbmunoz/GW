using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CampaignDashboardController : BaseController
{
    private readonly ICampaignDashboardService _campaignDashboardService;
    private readonly IMessagePublisherService _messagePublisherService;
    public CampaignDashboardController(IMessagePublisherService messagePublisherService, ICampaignDashboardService campaignDashboardService)
    {
        _messagePublisherService = messagePublisherService;
        _campaignDashboardService = campaignDashboardService;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignSurveyAndFeedbackReportAsync([FromBody] CampaignSurveyAndFeedbackReportRequestModel request)
    {
        return await GetResultAsync(await _messagePublisherService.GetCampaignSurveyAndFeedbackReportAsync(request));
    }        
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCampaignSurveyAndFeedbackExcelReportAsync([FromBody] CampaignSurveyAndFeedbackReportRequestModel request)
    {
        return await GetResultAsync(await _messagePublisherService.GetCampaignSurveyAndFeedbackReportAsync(request));
    }
}

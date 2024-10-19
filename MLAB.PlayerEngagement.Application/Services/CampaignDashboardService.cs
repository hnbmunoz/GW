using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class CampaignDashboardService : ICampaignDashboardService
{
    private readonly ILogger<CampaignDashboardService> _logger;
    private readonly ICampaignDashboardFactory _campaignDashboardFactory;

    public CampaignDashboardService(ILogger<CampaignDashboardService> logger, ICampaignDashboardFactory campaignDashboardFactory)
    {
        _logger = logger;
        _campaignDashboardFactory = campaignDashboardFactory;
    }
    public async Task<CampaignSurveyAndFeedbackReportResponseModel> GetCampaignSurveyAndFeedbackReport(CampaignSurveyAndFeedbackReportRequestModel request)
    {
        var results = await _campaignDashboardFactory.GetCampaignSurveyAndFeedbackReport(request);
        return results;
    }
}

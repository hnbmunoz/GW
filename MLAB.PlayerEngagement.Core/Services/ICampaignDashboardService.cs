using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ICampaignDashboardService
{
    Task<CampaignSurveyAndFeedbackReportResponseModel> GetCampaignSurveyAndFeedbackReport(CampaignSurveyAndFeedbackReportRequestModel request);
}

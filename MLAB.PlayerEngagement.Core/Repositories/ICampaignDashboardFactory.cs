using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ICampaignDashboardFactory
{
    Task<CampaignSurveyAndFeedbackReportResponseModel> GetCampaignSurveyAndFeedbackReport(CampaignSurveyAndFeedbackReportRequestModel request);
}

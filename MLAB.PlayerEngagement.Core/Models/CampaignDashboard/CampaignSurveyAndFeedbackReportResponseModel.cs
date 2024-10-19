namespace MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

public class CampaignSurveyAndFeedbackReportResponseModel
{
    public SurveyAndFeedbackReportSummary ReportSummary { get; set; }
    public List<LookupModel> FeedbackResultSummary { get; set; }
    public List<CampaignFeedbackResultResponseModel> FeedbackResult { get; set; }
    public List<CampaignSurveyResultResponseModel> SurveyResult { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

public class CampaignSurveyResultResponseModel
{
    public int SurveyQuestionId { get; set; }
    public string SurveyQuestionName { get; set; }
    public string SurveyAnswer { get; set; }
    public int Count { get; set; }
    public int Deposited { get; set; }
    public decimal DepositedPercentage { get; set; }
}

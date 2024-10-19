namespace MLAB.PlayerEngagement.Core.Models.CampaignDashboard;

public class CampaignSurveyAndFeedbackReportRequestModel : BaseModel
{
    public int CampaignId { get; set; }
    public int CurrencyId { get; set; }
    public bool IncludeDiscardPlayerTo { get; set; }
    public DateTime? RegistrationDateStart { get; set; }
    public DateTime? RegistrationDateEnd { get; set; }
    public DateTime? TaggedDateStart { get; set; }
    public DateTime? TaggedDateEnd { get; set; }
    public DateTime? AddedToCampaignStart { get; set; }
    public DateTime? AddedToCampaignEnd { get; set; }
    public string ExportFormat { get; set; }
    public int? SurveyQuestionId { get; set; }
    public int? CampaignTypeId { get; set; }

}

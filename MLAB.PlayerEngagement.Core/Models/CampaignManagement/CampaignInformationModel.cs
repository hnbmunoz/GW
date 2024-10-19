namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignInformationModel
{
    public int? CampaignInformationId { get; set; }
    public int? CampaignId { get; set; }
    public int? CampaignTypeId { get; set; }
    public string CampaignDescription { get; set; }
    public string CampaignPeriodFrom { get; set; }
    public string CampaignPeriodTo { get; set; }
    public int? CampaignReportPeriod { get; set; }
    public int? SurveyTemplateId { get; set; }
    public int? BrandId { get; set; }
}

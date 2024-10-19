namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignModel : BaseModel
{
    public int CampaignId { get; set; }
    public int? CampaignStatusId { get; set; }
    public string CampaignName { get; set; }
    public int UpdatedBy { get; set; }
    public int CreatedBy { get; set; }
    public string HoldReason { get; set; }
    public string CampaignGuid { get; set; }
    public CampaignInformationModel  CampaignInformationModel { get; set; }
    public CampaignConfigurationModel CampaignConfigurationModel { get; set; }
    public List<CampaignConfigurationAutoTaggingModel>  CampaignConfigurationAutoTaggingModel { get; set; }
    public List<CampaignInformationCurrencyModel> CampaignInformationCurrencyModel { get; set; }
    public List<CampaignConfigurationExchangeRateModel> CampaignConfigurationExchangeRateModel { get; set; }
    public List<CampaignConfigurationGoalModel> CampaignConfigurationGoalModel { get; set; }
    public CampaignIncentiveDataSourceModel CampaignIncentiveDataSourceModel { get; set; }
    public CampaignConfigurationCommunicationModel CampaignConfigurationCommunicationModel { get; set; }
    public List<CampaignCustomEventCountryRequestModel> CampaignCustomEventCountryModel { get; set; } 
    public List<CampaignCommunicationCustomEventRequestModel> CampaignCommunicationCustomEventModel { get;set; }
}

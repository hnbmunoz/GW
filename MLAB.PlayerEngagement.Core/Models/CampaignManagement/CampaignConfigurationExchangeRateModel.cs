namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignConfigurationExchangeRateModel
{
    public int CampaignConfigurationId { get; set; }
    public int CampaignInformationCurrencyId { get; set; }
    public decimal? ExchangeRate { get; set; }
    public int CampaignConfigurationExchangeRateId { get; set; }
    public int CurrencyId { get; set; }
    public string CurrencyName { get; set; }

}

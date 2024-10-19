namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignIncentiveDataSourceModel
{
    public int CampaignIncentiveDataSourceId { get; set; }
    public string SettingName { get; set; }
    public int CampaignConfigurationGoalId { get; set; }
    public int?  CampaignSettingId { get; set; }
    public int AmountParameterId { get; set; }
    public int CountParameterId { get; set; }

}

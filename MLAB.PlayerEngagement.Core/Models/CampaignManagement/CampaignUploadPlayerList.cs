namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignUploadPlayerList
{
    public long CampaignId { get; set; }
    public long PlayerId { get; set; }
    public string Brand { get; set; }
    public string Username { get; set; }
    public string Status { get; set; }
    public string LastDepositDate { get; set; }
    public decimal LastDepositAmount { get; set; }
    public string BonusAbuser { get; set; }
    public string LastBetDate { get; set; }
    public string LastBetProduct { get; set; }


}

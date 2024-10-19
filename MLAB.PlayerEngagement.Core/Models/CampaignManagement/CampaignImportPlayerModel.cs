namespace MLAB.PlayerEngagement.Core.Models;

public class CampaignImportPlayerModel : BaseModel
{
    public List<CampaignCSVPlayerListModel> CampaignCSVPlayerListModel { get; set; }
    public int CampaignId { get; set; }
    public string GuidId { get; set; }
}

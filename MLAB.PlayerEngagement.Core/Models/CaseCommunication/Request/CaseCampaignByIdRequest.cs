namespace MLAB.PlayerEngagement.Core.Models;

public class CaseCampaignByIdRequest: BaseModel
{
    public string PlayerId { get; set; }
    public int CampaignId { get; set; }
    public string BrandName { get; set; }
}

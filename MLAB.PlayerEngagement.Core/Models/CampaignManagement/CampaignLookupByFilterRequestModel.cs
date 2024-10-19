namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignLookupByFilterRequestModel
{
    public int? CampaignId { get; set; }
    public string CampaignName { get; set; }
    public int? CampaignStatusId { get; set; }
    public int? CampaignTypeId { get; set; }
}

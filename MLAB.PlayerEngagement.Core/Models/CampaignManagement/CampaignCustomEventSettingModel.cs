namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignCustomEventSettingModel : BaseModel
{
    public int CampaignEventSettingId { get; set; }
    public string CustomEventName { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models;

public class AutoTaggingPointIncentiveFilterRequestModel : BaseModel
{
    public string CampaignSettingName { get; set; }
    public int? IsActive { get; set; }
    public int? CampaignSettingId { get; set; }
    public int? CampaignSettingTypeId { get; set; }

    public string CreatedDate { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int? SettingTypeId { get; set; }
}

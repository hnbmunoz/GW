namespace MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;

public class CampaignGoalSettingByFilterRequestModel : BaseModel
{
    public string CampaignSettingName { get; set; }
    public int? IsActive { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int OffsetValue { get; set; }
    public int PageSize { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

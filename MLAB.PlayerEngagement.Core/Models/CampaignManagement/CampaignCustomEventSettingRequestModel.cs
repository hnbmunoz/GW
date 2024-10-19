namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignCustomEventSettingRequestModel : BaseModel
{
    public string CustomEventName { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int OffsetValue { get; set; }
    public int PageSize { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

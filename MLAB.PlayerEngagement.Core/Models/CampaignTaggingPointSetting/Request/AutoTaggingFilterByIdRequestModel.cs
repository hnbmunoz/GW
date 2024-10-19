namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class AutoTaggingFilterByIdRequestModel : BaseModel
{
    public int? CampaignSettingId { get; set; }
    public int? CampaignSettingTypeId { get; set; }
}

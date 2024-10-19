namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;

public class AutoTaggingFilterByIdResponseModel : CampaignSettingModel
{
    public List<TaggingConfigurationModel> TaggingConfigurationList { get; set; }
    public List<UserTaggingRequestModel> UserTaggingList { get; set; }
    public List<CampaignPeriodDetails> CampaignPeriodList { get; set; }
}

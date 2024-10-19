
namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting.Response
{
    public  class PointIncentiveDetailsByIdResponseModel
    {
        public List<CampaignSettingModel> CampaignSetting{ get; set; }   
        public List<PointToIncentiveRangeConfigurationModel> PointToIncentiveRanges { get; set; }
        public List<GoalParameterRangeConfigurationModel> GoalParameterRanges {  get; set; }  
        public List<CampaignPeriodDetails> CampaignPeriodDetails { get; set; }
    }
}

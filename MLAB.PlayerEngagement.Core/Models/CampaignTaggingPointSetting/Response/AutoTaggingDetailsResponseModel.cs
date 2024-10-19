using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting.Response
{
    public class AutoTaggingDetailsResponseModel
    {
        public List<CampaignSettingModel> CampaignSettings { get; set; }    
        public List<TaggingConfigurationModel> TaggingConfigurations { get; set; }
        public List<UserTaggingModel> UserTaggings { get; set; }
        public List<CampaignPeriodDetails> CampaignPeriodDetails { get; set; }
    }
}

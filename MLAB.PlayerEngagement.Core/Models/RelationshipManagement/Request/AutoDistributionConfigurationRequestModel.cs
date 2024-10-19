namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request
{
    public class AutoDistributionConfigurationRequestModel: BaseModel
    {
        public long AutoDistributionSettingId { get; set; }
        public string ConfigurationName { get; set; }
        public List<CurrencyObject> AdsCurrencyType { get; set; }
        public List<CountryObject> AdsCountryType { get; set; }
        public List<VipLevelObject> AdsVipLevelType { get; set; }
        public List<RemAgentObject> AdsRemAgentType { get; set; }
    }

    public class CurrencyObject
    {
        public long CurrencyId { get; set; }
    }

    public class CountryObject
    {
        public long CountryId { get; set; }
    }

    public class VipLevelObject
    {
        public long VipLevelId { get; set; }
    }

    public class RemAgentObject
    {
        public long RemProfileId { get; set; }
    }
}

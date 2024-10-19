namespace MLAB.PlayerEngagement.Core.Response
{
    public class GetAppConfigSettingByApplicationIdResponseModel
    {
        public int AppConfigSettingId { get; set; }
        public int ApplicationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

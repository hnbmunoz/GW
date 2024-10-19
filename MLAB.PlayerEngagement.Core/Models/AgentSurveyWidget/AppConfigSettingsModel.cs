namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget
{
    public class AppConfigSettingsModel
    {
        public int MaxRetry { get; set; }
        public int TimeToWait { get; set; }
        public string BaseUrl { get; set; }
        public bool IsResync { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string License { get; set; }
        public string AppKey { get; set; }
        public string AccessToken { get; set; }
        public int DailyRunMinusDay { get; set; }
        public string RecipientEmail { get; set; }
    }
}

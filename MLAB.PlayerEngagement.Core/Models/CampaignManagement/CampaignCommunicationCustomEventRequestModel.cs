namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement
{
    public class CampaignCommunicationCustomEventRequestModel
    {
        public long CampaignCommunicationCustomEventId { get; set; }
        public long CurrencyId { get; set; }
        public long CampaignEventSettingId { get; set; }
        public long CampaignCommunicationSettingId { get; set; }
        public string CustomEventGuid { get; set; }
    }
}

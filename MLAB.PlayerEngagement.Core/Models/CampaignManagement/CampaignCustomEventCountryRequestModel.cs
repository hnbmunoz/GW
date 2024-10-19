namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement
{
    public class CampaignCustomEventCountryRequestModel
    {
        public long CampaignCustomEventCountryId { get; set; }
        public long CountryId { get; set; }
        public long CampaignCommunicationCustomEventId { get; set; }
        public string ParentCustomEventGuid { get; set; }

    }
}

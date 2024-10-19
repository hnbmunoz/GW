using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignJourney;

namespace MLAB.PlayerEngagement.Core.Services;

public interface ICampaignJourneyService
{
    Task<List<JourneyCampaignDetailsModel>> GetJourneyCampaignDetailsAsync(string campaignId);
    Task<List<LookupModel>> GetJourneyCampaignNamesAsync(string searchFilterField, int searchFilterType, int campaignType);
    Task<List<LookupModel>> GetJourneyNamesAsync();
    Task<List<LookupModel>> GetJourneyStatusAsync();
}

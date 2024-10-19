using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignJourney;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ICampaignJourneyFactory
{
    Task<List<JourneyCampaignDetailsModel>> GetJourneyCampaignDetailsAsync(string campaignId);
    Task<List<LookupModel>> GetJourneyCampaignNamesAsync(string searchFilterField, int searchFilterType, int campaignType);
    Task<List<LookupModel>> GetJourneyOptionsAsync(int modeType);
}

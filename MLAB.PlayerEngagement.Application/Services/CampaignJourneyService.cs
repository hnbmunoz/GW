using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignJourney;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class CampaignJourneyService : ICampaignJourneyService
{
    private readonly ICampaignJourneyFactory _campaignJourneyFactory;

    public CampaignJourneyService(ICampaignJourneyFactory campaignJourneyFactory)
    {
        _campaignJourneyFactory = campaignJourneyFactory;
    }

    public async Task<List<JourneyCampaignDetailsModel>> GetJourneyCampaignDetailsAsync(string campaignId)
    {
       var campaignDetails = await _campaignJourneyFactory.GetJourneyCampaignDetailsAsync(campaignId);
       return campaignDetails;
    }

    public async Task<List<LookupModel>> GetJourneyCampaignNamesAsync(string searchFilterField, int searchFilterType, int campaignType)
    {
        var campaigns = await _campaignJourneyFactory.GetJourneyCampaignNamesAsync(searchFilterField, searchFilterType, campaignType);
        return campaigns;
    }

    public async Task<List<LookupModel>> GetJourneyNamesAsync()
    {
        var journeyNames = await _campaignJourneyFactory.GetJourneyOptionsAsync(4);
        return journeyNames;
    }

    public async Task<List<LookupModel>> GetJourneyStatusAsync()
    {
        var journeyStatus = await _campaignJourneyFactory.GetJourneyOptionsAsync(5);
        return journeyStatus;
    }
}

using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignJourney;
using MLAB.PlayerEngagement.Core.Repositories;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CampaignJourneyFactory : ICampaignJourneyFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<CampaignJourneyFactory> _logger;

    public CampaignJourneyFactory (
        IMainDbFactory mainDbFactory, 
        ILogger<CampaignJourneyFactory> logger
        )
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }

    public async Task<List<JourneyCampaignDetailsModel>> GetJourneyCampaignDetailsAsync(string campaignId)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignJourneyFactory} | GetJourneyCampaignDetailsAsync - [campaignId: {campaignId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<JourneyCampaignDetailsModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignJourney,
                                new
                                {
                                    @Mode = 1,
                                    @CampaignIds = campaignId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignJourneyFactory} | GetJourneyCampaignDetailsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<JourneyCampaignDetailsModel>().ToList();
    }

    public async Task<List<LookupModel>> GetJourneyCampaignNamesAsync(string searchFilterField, int searchFilterType, int campaignType)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<JourneyCampaignDetailsModel>
                            (
                                DatabaseFactories.PlayerManagementDB,   
                                StoredProcedures.USP_GetJourneyCampaignNames,
                                new
                                {
                                    CampaignTypeId = campaignType,
                                    SearchActivityTypeId = searchFilterType,
                                }
                            ).ConfigureAwait(false);

            var campaignNames = new List<LookupModel>();

            foreach (var item in result)
            {
                campaignNames.Add(new LookupModel
                {
                    Label = item.CampaignName,
                    Value = item.CampaignId
                });
            }
            return campaignNames.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignJourneyFactory} | GetJourneyCampaignNamesAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<LookupModel>> GetJourneyOptionsAsync(int modeType)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<JourneyDetailsModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignJourney,
                                new
                                {
                                    @Mode = modeType
                                }
                            ).ConfigureAwait(false);

            var options = new List<LookupModel>();

            foreach (var item in result)
            {
                if (modeType == 4)
                {
                    options.Add(new LookupModel
                    {
                        Label = item.JourneyName,
                        Value = item.JourneyId
                    });

                } else if (modeType == 5)
                {
                    options.Add(new LookupModel
                    {
                        Label = item.JourneyStatus,
                        Value = item.JourneyStatusId   //No Journey Status Id
                    });
                }
            };
            return options.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignJourneyFactory} | GetJourneyCampaignNamesAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }
}

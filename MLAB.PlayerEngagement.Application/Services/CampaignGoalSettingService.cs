using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;
using MLAB.PlayerEngagement.Core.Logging;
using MediatR;
using MLAB.PlayerEngagement.Core.Repositories;

namespace MLAB.PlayerEngagement.Application.Services;

public class CampaignGoalSettingService : ICampaignGoalSettingService
{
    private readonly ILogger<CampaignGoalSettingService> _logger;
    private readonly ICampaignGoalSettingFactory _campaignGoalSettingFactory;
    

    public CampaignGoalSettingService(IMediator mediator, ILogger<CampaignGoalSettingService> logger, ICampaignGoalSettingFactory campaignGoalSettingFactory)
    {
        _campaignGoalSettingFactory = campaignGoalSettingFactory;
        _logger = logger;

    }
    public async Task<bool> CheckCampaignGoalSettingByNameExistAsync(CampaignGoalSettingNameRequestModel request)
    {
        var result = await _campaignGoalSettingFactory.CheckCampaignGoalSettingByNameExistAsync(request);
        return result;
    }
}

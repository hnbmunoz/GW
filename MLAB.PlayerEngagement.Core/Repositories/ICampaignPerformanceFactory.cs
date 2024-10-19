using MLAB.PlayerEngagement.Core.Models.CampaignPerformance;

namespace MLAB.PlayerEngagement.Core.Repositories;

public  interface ICampaignPerformanceFactory
{
    Task<Tuple<List<CampaignActiveAndEndedResponseModel>, List<CampaignGoalResponseModel>>> GetCampaignPerformanceFilterAsync(int campaignTypeId);
}

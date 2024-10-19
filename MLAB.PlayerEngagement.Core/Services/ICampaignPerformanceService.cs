using MLAB.PlayerEngagement.Core.Models.CampaignPerformance;

namespace MLAB.PlayerEngagement.Core.Services;

public  interface ICampaignPerformanceService
{
    Task<CampaignPerformanceFilterResponseModel> GetCampaignPerformanceFilterAsync(int campaignTypeId);
}

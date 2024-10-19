using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Administrator;
using MLAB.PlayerEngagement.Core.Response;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IAdminstratorService
{
    Task<QueueRequestResponse> GetQueueRequestAsync(QueueFilterRequestModel queueFilter);
    Task<QueueHistoryResponse> GetQueueHistoryAsync(QueueFilterRequestModel queueFilter);
    Task<List<QueueStatusResponse>> GetDistinctQueueStatus();
    Task<List<QueueActionResponse>> GetDistinctQueueActions();
    Task<QueueCountResponse> DeleteQueueByCreatedDateRange(DeleteQueueRequestModel queueFilter);
}

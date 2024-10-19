using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Administrator;
using MLAB.PlayerEngagement.Core.Response;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface IAdministratorFactory
{
    Task<QueueRequestResponse> GetQueueRqstLstAsync(QueueFilterRequestModel queueFilter);
    Task<QueueHistoryResponse> GetQueueHstryLst(QueueFilterRequestModel queueFilter);
    Task<List<QueueStatusResponse>> GetDistinctQueueStatus();
    Task<List<QueueActionResponse>> GetDistinctQueueActions();
    Task<QueueCountResponse> DeleteQueueByCreatedDateRange(DeleteQueueRequestModel queueFilter);
}

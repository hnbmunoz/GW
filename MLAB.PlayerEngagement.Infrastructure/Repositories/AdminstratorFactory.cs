using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Administrator;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;


namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class AdminstratorFactory : IAdministratorFactory
{
    private readonly ILogger<AdminstratorFactory> _logger;
    private readonly IMainDbFactory _mainDbFactory;
    #region Constructor
    public AdminstratorFactory(IMainDbFactory mainDbFactory, ILogger<AdminstratorFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }
    #endregion

    //GET: Return data from QueueResult tbl between two dates
    public async Task<QueueRequestResponse> GetQueueRqstLstAsync(QueueFilterRequestModel queueFilter)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<int, QueueRequests>
                            (
                                DatabaseFactories.MicroDb,
                                StoredProcedures.USP_GetQueueRqstByFiltr, new
                                {
                                    DateFrom = string.IsNullOrEmpty(queueFilter.CreatedFrom) ? null : DateTime.Parse(queueFilter.CreatedFrom).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                                    DateTo = string.IsNullOrEmpty(queueFilter.CreatedTo) ? null : DateTime.Parse(queueFilter.CreatedTo).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                                    CreatedBy = string.IsNullOrEmpty(queueFilter.CreatedBy) ? null : queueFilter.CreatedBy,
                                    Action = string.IsNullOrEmpty(queueFilter.Action) ? null : queueFilter.Action,
                                    Status = string.IsNullOrEmpty(queueFilter.Status) ? null : queueFilter.Status,
                                    PageSize = queueFilter.PageSize,
                                    OffsetValue = queueFilter.OffsetValue,
                                    SortColumn = string.IsNullOrEmpty(queueFilter.SortColumn) ? "CreatedDate" : queueFilter.SortColumn,
                                    SortOrder = string.IsNullOrEmpty(queueFilter.SortOrder) ? "ASC" : queueFilter.SortOrder
                                }

                            ).ConfigureAwait(false);
            return new QueueRequestResponse {
                RecordCount = result.Item1.First(),
                QueueRequests = result.Item2.ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Factory | Administrator.GetQueueRqstLstAsync: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }
        return Enumerable.Empty<QueueRequestResponse>().First();
    }

    //GET: Return data from QueueHistory tbl between two dates
    public async Task<QueueHistoryResponse> GetQueueHstryLst(QueueFilterRequestModel queueFilter)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<int, QueueHistory>
                            (
                                DatabaseFactories.MicroDb,
                                StoredProcedures.USP_GetQueueHstryByFiltr, new
                                {
                                    DateFrom = string.IsNullOrEmpty(queueFilter.CreatedFrom) ? null : DateTime.Parse(queueFilter.CreatedFrom).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                                    DateTo = string.IsNullOrEmpty(queueFilter.CreatedTo) ? null : DateTime.Parse(queueFilter.CreatedTo).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                                    CreatedBy = string.IsNullOrEmpty(queueFilter.CreatedBy) ? null : queueFilter.CreatedBy,
                                    Action = string.IsNullOrEmpty(queueFilter.Action) ? null : queueFilter.Action,
                                    Status = string.IsNullOrEmpty(queueFilter.Status) ? null : queueFilter.Status,
                                    PageSize = queueFilter.PageSize,
                                    OffsetValue = queueFilter.OffsetValue,
                                    SortColumn = string.IsNullOrEmpty(queueFilter.SortColumn) ? "CreatedDate" : queueFilter.SortColumn,
                                    SortOrder = string.IsNullOrEmpty(queueFilter.SortOrder) ? "ASC" : queueFilter.SortOrder
                                }

                            ).ConfigureAwait(false);
            return new QueueHistoryResponse
            {
                RecordCount = result.Item1.First(),
                QueueHistory = result.Item2.ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Factory | Administrator.GetQueueHstryLst: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }
        return Enumerable.Empty<QueueHistoryResponse>().First();
    }

    public async Task<List<QueueStatusResponse>> GetDistinctQueueStatus()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<QueueStatusResponse>
                            (
                                DatabaseFactories.MicroDb,
                                StoredProcedures.USP_GetDstinctQueueStatus,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Factory | Administrator.GetDistinctQueueStatus: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }
        return Enumerable.Empty<QueueStatusResponse>().ToList();
    }

    public async Task<List<QueueActionResponse>> GetDistinctQueueActions()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<QueueActionResponse>
                            (
                                DatabaseFactories.MicroDb,
                                StoredProcedures.USP_GetDstinctQueueActions,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Factory | Administrator.GetDistinctQueueActions: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }
        return Enumerable.Empty<QueueActionResponse>().ToList();
    }

    public async Task<QueueCountResponse> DeleteQueueByCreatedDateRange(DeleteQueueRequestModel queueFilter)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<QueueCountResponse>
                            (
                                DatabaseFactories.MicroDb,
                                StoredProcedures.USP_DeleteQueueByCreatedDateRange, new
                                {
                                    DateFrom = string.IsNullOrEmpty(queueFilter.CreatedFrom) ? null : DateTime.Parse(queueFilter.CreatedFrom).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                                    DateTo = string.IsNullOrEmpty(queueFilter.CreatedTo) ? null : DateTime.Parse(queueFilter.CreatedTo).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                                    CreatedBy = string.IsNullOrEmpty(queueFilter.UserId) ? null : queueFilter.UserId
                                }

                            ).ConfigureAwait(false);
            return new QueueCountResponse
            {
                TotalHstoryRecordCnt = result.First().TotalHstoryRecordCnt,
                TotalRqstRecordCnt = result.First().TotalRqstRecordCnt
            };
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Factory | Administrator.GetQueueRqstLstAsync: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }
        return Enumerable.Empty<QueueCountResponse>().FirstOrDefault();
    }
}

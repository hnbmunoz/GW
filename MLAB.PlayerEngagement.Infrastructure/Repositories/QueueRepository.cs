using MLAB.PlayerEngagement.Core.Entities;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Constants;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class QueueRepository : IQueueFactory
{

    private readonly IMainDbFactory _mainDbFactory;

    #region Constructor
    public QueueRepository(IMainDbFactory mainDbFactory)
    {
        _mainDbFactory = mainDbFactory;
    }
    #endregion

    public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
    {
        var result = await _mainDbFactory
           .ExecuteQueryAsync<Currency>
               ( DatabaseFactories.MicroDb,
                   StoredProcedures.Usp_GetCurrencies,
                   null
               ).ConfigureAwait(false);
        return result.ToList();
    }
    public async Task<IEnumerable<Operator>> GetOperatorsAsync()
    {
        var result = await _mainDbFactory
           .ExecuteQueryAsync<Operator>
               (   DatabaseFactories.MicroDb,
                   StoredProcedures.Usp_GetOperators,
                   null
               ).ConfigureAwait(false);
        return result.ToList();
    }
    public async Task<bool> InsertQueueRequestAsync(Queue queueRequest)
    {
        await _mainDbFactory
               .ExecuteAsync(DatabaseFactories.MicroDb,StoredProcedures.Usp_InsertQueueRequest,
               new
               {
                   queueId = queueRequest.QueueId,
                   queueName = queueRequest.QueueName,
                   userId = queueRequest.UserId,
                   createdBy = queueRequest.CreatedBy,
                   redisCacheRequestId = queueRequest.RedisCacheRequestId,
                   QueueStatus = queueRequest.QueueStatus,
                   serviceTypeId = queueRequest.ServiceTypeId,
                   remarks = queueRequest.Remarks,
                   action = queueRequest.Action
               }).ConfigureAwait(false);

        return true;
    }
}

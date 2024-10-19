using MLAB.PlayerEngagement.Core.Entities;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface IQueueFactory
{
    Task<IEnumerable<Currency>> GetCurrenciesAsync();
    Task<IEnumerable<Operator>> GetOperatorsAsync();
    Task<bool> InsertQueueRequestAsync(Queue queueRequest);
}

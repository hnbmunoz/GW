using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;

namespace MLAB.PlayerEngagement.Core.Repositories
{
    public interface IMainDbFactory
    {
        Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(DatabaseFactories factory,string storedproc, object param);
        Task<int> ExecuteQueryAsync(DatabaseFactories factory,string storedproc, object param);
        Task<int> ExecuteAsync(DatabaseFactories factory,string storedproc, object param);
        Task<T> ExecuteQuerySingleOrDefaultAsync<T>(DatabaseFactories factory,string storedproc, object param);
        Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> ExecuteQueryMultipleAsync<T1, T2>(DatabaseFactories factory,string storeproc, object param);
        Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> ExecuteQueryMultipleAsync<T1, T2, T3>(DatabaseFactories factory,string storeproc, object param);
        Task<List<IEnumerable<T1>>> ExecuteQueryMultipleAsync<T1>(DatabaseFactories factory, string storeproc, object param, int resultCount);
        Task<CustomerCaseChatStatisticsModel> ExecuteQueryMultipleParamAsync(DatabaseFactories factory, string storedproc, object param);
    }
}
namespace MLAB.PlayerEngagement.Core.Repositories.Base;

public interface IBaseFactory
{
    IEnumerable<TResult> ExecuteSp<TResult>(string spName, object param = null);
    TResult ExecuteSpSingleOrDefault<TResult>(string spName, object param = null);
    IEnumerable<TResult> ExecuteQuery<TResult>(string query);
    TResult ExecuteQuerySingleOrDefault<TResult>(string query);
    Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>> ExecuteSpMultiReturn<TResult1, TResult2>(string spName, object parameters);
    Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>> ExecuteQueryMultiReturn<TResult1, TResult2>(string spName, object parameters);
    Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>, IEnumerable<TResult3>> ExecuteSpMultiReturn<TResult1, TResult2, TResult3>(string spName, object parameters);
    int ExecuteSp(string spName, ref IDictionary<string, object> criteria);
    int ExecuteSp(string spName, object criteria);
    Tuple<TResult1, IEnumerable<TResult2>> ExecuteSpCustomMultiReturn<TResult1, TResult2>(string spName, object parameters) where TResult1 : class;
}

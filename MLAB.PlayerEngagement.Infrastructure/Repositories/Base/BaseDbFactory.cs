using Dapper;
using Microsoft.Extensions.Options;
using MLAB.PlayerEngagement.Infrastructure.Config;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories.Base;

public abstract class BaseDbFactory
{
    #region Abstract Properties
    public abstract string ConnectionString { get; }
    #endregion

    public readonly ConnectionString Config;



    #region Constructor
    protected BaseDbFactory(IOptions<ConnectionString> config)
    {
        Config = config.Value;
    }
    #endregion

    #region Implementation
    /// <summary>
    /// Executes Sp Script and Returns the parameter output
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="spName"></param>
    /// <param name="criteria"></param>
    /// <returns>int</returns>
    public int ExecuteSp(string spName, ref IDictionary<string, object> criteria)
    {
        var parameters = new DynamicParameters();
        var outputParamKey = String.Empty;
        var outputKeyList = new List<string>();

        foreach (var pair in criteria)
        {
            if (pair.Value.GetType() == typeof(DbType))
            {
                parameters.Add($"@{pair.Key}", dbType: (DbType)pair.Value, direction: ParameterDirection.Output);
                outputKeyList.Add(pair.Key);
            }
            else
            {
                parameters.Add($"@{pair.Key}", pair.Value);
            }
        }

        int result = ExecuteScriptReturnParam(spName, ref parameters, true);

        //Assigns output parameter on the list involved.
        foreach (string key in outputKeyList)
            criteria[key] = parameters.Get<object>($"@{key}");

        return result;
    }

    /// <summary>
    /// Execute Stored Procedure and returns list of objects
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="spName"></param>
    /// <param name="criteria"></param>
    /// <returns>IEnumerable<T></returns>
    public IEnumerable<TResult> ExecuteSp<TResult>(string spName, object criteria = null)
    {
        return ExecuteScript<TResult>(spName, criteria, true);
    }

    /// <summary>
    /// Executes Stored Procedures with non query result.
    /// </summary>
    /// <param name="spName"></param>
    /// <param name="criteria"></param>
    /// <returns>int</returns>
    public int ExecuteSp(string spName, object criteria)
    {
        return ExecuteSpNonQuery(spName, criteria, true);
    }

    /// <summary>
    /// Execute Stored Procedure and returns single object
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="spName"></param>
    /// <param name="criteria"></param>
    /// <returns>IEnumerable<T></returns>
    public TResult ExecuteSpSingleOrDefault<TResult>(string spName, object criteria = null)
    {
        return ExecuteScript<TResult>(spName, criteria, true).FirstOrDefault();
    }

    /// <summary>
    /// Execute query text and returns single object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryText"></param>
    /// <returns>Single Object</returns>
    public T ExecuteQuerySingleOrDefault<T>(string queryText)
    {
        return ExecuteScript<T>(queryText, null).FirstOrDefault();
    }

    /// <summary>
    /// Execute query text and returns list of objects.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="spName"></param>
    /// <param name="criteria"></param>
    /// <returns>IEnumerable<T></returns>
    public IEnumerable<TResult> ExecuteQuery<TResult>(string queryText)
    {
        return ExecuteScript<TResult>(queryText, null);
    }

    /// <summary>
    /// Execute Stored Procedure with 2 IEnumerable object as return
    /// </summary>
    /// <typeparam name="TResult1"></typeparam>
    /// <typeparam name="TResult2"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>Two list objects</returns>
    /// ExecuteSpMultiReturn<Foo, Bar>(spn)
    public Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>> ExecuteSpMultiReturn<TResult1, TResult2>(string spName, object parameters)
    {
        Func<GridReader, IEnumerable<TResult1>> func1 = gr => gr.Read<TResult1>();
        Func<GridReader, IEnumerable<TResult2>> func2 = gr => gr.Read<TResult2>();

        var objs = ExecuteMultiReturnScript(spName, parameters, true, func1, func2);
        return Tuple.Create(objs[0] as IEnumerable<TResult1>, objs[1] as IEnumerable<TResult2>);
    }

    /// <summary>
    /// Execute Query with 2 IEnumerable object as return
    /// </summary>
    /// <typeparam name="TResult1"></typeparam>
    /// <typeparam name="TResult2"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>Two list objects</returns>
    /// ExecuteSpMultiReturn<Object1, Object2>(spName, object)
    public Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>> ExecuteQueryMultiReturn<TResult1, TResult2>(string spName, object parameters)
    {
        Func<GridReader, IEnumerable<TResult1>> func1 = gr => gr.Read<TResult1>();
        Func<GridReader, IEnumerable<TResult2>> func2 = gr => gr.Read<TResult2>();

        var objs = ExecuteMultiReturnScript(spName, parameters, false, func1, func2);

        return Tuple.Create(objs[0] as IEnumerable<TResult1>, objs[1] as IEnumerable<TResult2>);
    }

    /// <summary>
    /// Execute Stored Procedure with 3 IEnumerable object as return
    /// </summary>
    /// <typeparam name="TResult1"></typeparam>
    /// <typeparam name="TResult2"></typeparam>
    /// <typeparam name="TResult3"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>Two list objects</returns>
    /// ExecuteSpMultiReturn<Object1, Object2, Object3>(spName, object)
    public Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>, IEnumerable<TResult3>> ExecuteSpMultiReturn<TResult1, TResult2, TResult3>(string spName, object parameters)
    {
        Func<GridReader, IEnumerable<TResult1>> func1 = gr => gr.Read<TResult1>();
        Func<GridReader, IEnumerable<TResult2>> func2 = gr => gr.Read<TResult2>();
        Func<GridReader, IEnumerable<TResult3>> func3 = gr => gr.Read<TResult3>();

        var objs = ExecuteMultiReturnScript(spName, parameters, true, func1, func2, func3);
        return Tuple.Create(objs[0] as IEnumerable<TResult1>, objs[1] as IEnumerable<TResult2>, objs[2] as IEnumerable<TResult3>);
    }

    public Tuple<TResult1, IEnumerable<TResult2>> ExecuteSpCustomMultiReturn<TResult1, TResult2>(string spName, object parameters) where TResult1 : class
    {
        Func<GridReader, TResult1> func1 = gr => gr.ReadSingle<TResult1>();
        Func<GridReader, IEnumerable<TResult2>> func2 = gr => gr.Read<TResult2>();


        var objs = ExecuteMultiReturnScript(spName, parameters, true, func1, func2);
        return Tuple.Create(objs[0] as TResult1, objs[1] as IEnumerable<TResult2>);
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// Executes Script using dapper.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="query"></param>
    /// <param name="criteria"></param>
    /// <param name="isStoredProcedure"></param>
    /// <returns></returns>
    private IEnumerable<TResult> ExecuteScript<TResult>(string query, object criteria = null, bool isStoredProcedure = false)
    {
        CommandType cmdType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
        using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
        {
            return dbConnection.Query<TResult>(query, criteria, commandType: cmdType);
        }
    }

    /// <summary>
    /// Executes script with non query using Dapper
    /// </summary>
    /// <param name="query"></param>
    /// <param name="criteria"></param>
    /// <param name="isStoredProcedure"></param>
    /// <returns>int</returns>
    private int ExecuteSpNonQuery(string query, object criteria = null, bool isStoredProcedure = false)
    {
        CommandType cmdType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
        using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
        {
            return dbConnection.Execute(query, criteria, commandType: cmdType);
        }
    }

    /// <summary>
    /// Executes Script with output parameter
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="isStoredProcedure"></param>
    /// <param name="readerFuncs"></param>
    /// <returns></returns>
    private int ExecuteScriptReturnParam(string query, ref DynamicParameters parameters, bool isStoredProcedure = false)
    {
        CommandType cmdType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
        var returnList = new List<object>();
        using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
        {
            return dbConnection.Execute(query, parameters, commandType: cmdType);
        }
    }

    /// <summary>
    /// Executes Script with multiple datatable/objects returns
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="isStoredProcedure"></param>
    /// <param name="readerFuncs"></param>
    /// <returns></returns>
    private List<object> ExecuteMultiReturnScript(string query, object parameters, bool isStoredProcedure = false, params Func<GridReader, object>[] readerFuncs)
    {
        CommandType cmdType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
        var returnList = new List<object>();
        using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
        {
            using (var gridReader = dbConnection.QueryMultiple(query, parameters, commandType: cmdType))
            {
                foreach (var readerFunc in readerFuncs)
                {
                    var obj = readerFunc(gridReader);
                    returnList.Add(obj);
                }
            }
        }

        return returnList;
    }
    #endregion

}

using Dapper;
using Microsoft.Extensions.Options;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Config;
using System.Data.SqlClient;
using System.Data;
using MLAB.PlayerEngagement.Core.Constants;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using static MassTransit.ValidationResultExtensions;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories
{
    public class MainDbFactory : IMainDbFactory
    {
        public readonly ConnectionString Config;
        public MainDbFactory(IOptions<ConnectionString> config)
        {
            Config = config.Value;
        }

        /// <summary>
        /// Will Return affected row
        /// Make sure your sp NOCOUNT is set to OFF
        /// </summary>
        /// <param name="storedproc"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(DatabaseFactories factory,string storedproc, object param)
        {
            using (var conn = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                return await conn.QueryAsync<TResult>(storedproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }
        public async Task<int> ExecuteQueryAsync(DatabaseFactories factory,string storedproc, object param)
        {
            await using var conn = new SqlConnection(GetDatabaseConfigValue(factory));
            return await conn.ExecuteAsync(storedproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }

        public async Task<int> ExecuteAsync(DatabaseFactories factory,string storedproc, object param)
        {
            using (var conn = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                return await conn.ExecuteAsync(storedproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<T> ExecuteQuerySingleOrDefaultAsync<T>(DatabaseFactories factory,string storedproc, object param)
        {
            using (var connQuerySingle = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                return await connQuerySingle.QuerySingleOrDefaultAsync<T>(storedproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<SqlMapper.GridReader> ExecuteQueryMultipleAsync(DatabaseFactories factory,string storeproc, object param)
        {
            using (var conn = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                return await conn.QueryMultipleAsync(storeproc, param).ConfigureAwait(false);
            }
        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> ExecuteQueryMultipleAsync<T1, T2>(DatabaseFactories factory,string storeproc, object param)
        {
            using (var connectionQuery = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                using (var multi = await connectionQuery.QueryMultipleAsync(storeproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(
                            await multi.ReadAsync<T1>().ConfigureAwait(false),
                            await multi.ReadAsync<T2>().ConfigureAwait(false)
                        );
                }
            }
        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> ExecuteQueryMultipleAsync<T1, T2, T3>(DatabaseFactories factory,string storeproc, object param)
        {
            using (var connQueryMultipleThree = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                using (var multi = await connQueryMultipleThree.QueryMultipleAsync(storeproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(
                            await multi.ReadAsync<T1>().ConfigureAwait(false),
                            await multi.ReadAsync<T2>().ConfigureAwait(false),
                            await multi.ReadAsync<T3>().ConfigureAwait(false)
                        );
                }
            }
        }
       
        public async Task<List<IEnumerable<T1>>> ExecuteQueryMultipleAsync<T1>(DatabaseFactories factory, string storeproc, object param, int resultCount)
        {
            using (var conn = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                using (var multi = await conn.QueryMultipleAsync(storeproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                   
                    List<IEnumerable<T1>> queryResults = new List<IEnumerable<T1>>();

                 
                    for (int i = 0; i < resultCount; i++)
                    {
                        IEnumerable<T1> queryResult = await multi.ReadAsync<T1>().ConfigureAwait(false);
                        queryResults.Add(queryResult);

                    }

                    return queryResults;
                   
                }
            }
        }

        public async Task<CustomerCaseChatStatisticsModel> ExecuteQueryMultipleParamAsync(DatabaseFactories factory, string storedproc, object param)
        {
            using (var connQueryMultiple = new SqlConnection(GetDatabaseConfigValue(factory)))
            {
                using (var multi = await connQueryMultiple.QueryMultipleAsync(storedproc, param, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    // Populate the CustomerCaseChatStatisticsModel with data from multiple result sets
                    var customerCaseChatStatistics = new CustomerCaseChatStatisticsModel
                    {
                        chatStatisticsCaseCommDetailsModel = (await multi.ReadAsync<ChatStatisticsCaseCommDetailsModel>()).FirstOrDefault(),
                        chatInformationModel = (await multi.ReadAsync<ChatStatisticsChatInformationModel>()).FirstOrDefault(),
                        chatAbandonmentModel = (await multi.ReadAsync<ChatStatisticsAbandonmentModel>()).FirstOrDefault(),
                        chatAgentSegmentModel = (await multi.ReadAsync<ChatStatisticsAgentSegmentModel>()).ToList(),
                        chatStatisticsAgentSegmentRecordCountModel = (await multi.ReadAsync<ChatStatisticsAgentSegmentRecordCountModel>()).FirstOrDefault(),
                        chatStatisticsSkillSegmentModel = (await multi.ReadAsync<ChatStatisticsSkillSegmentModel>()).ToList(),
                        chatStatisticsSkillSegmentRecordCountModel = (await multi.ReadAsync<ChatStatisticsSkillSegmentRecordCountModel>()).FirstOrDefault()
                    };

                    return customerCaseChatStatistics;
                }
            }
        }


        public string GetDatabaseConfigValue(DatabaseFactories factory)
        {
            switch (factory)
            {
                case DatabaseFactories.PlayerManagementDB:
                    // Use the PlayerManagementDb connection string from the config
                    return Config.PlayerManagementDb;

                case DatabaseFactories.MLabDB:
                    // Use the MLabDb connection string from the config
                    return Config.MlabDb;

                case DatabaseFactories.IntegrationDb:
                    // Use the IntegrationDb connection string from the config
                    return Config.IntegrationDb;

                case DatabaseFactories.MicroDb:
                    // Use the MicroDb connection string from the config
                    return Config.MicroDb;

                case DatabaseFactories.UserManagementDb:
                    // Use the UserManagementDb connection string from the config
                    return Config.UserManagementDb;

                case DatabaseFactories.TicketManagementDb:
                    // Use the TicketManagementDb connection string from the config
                    return Config.TicketManagementDb;

                //Secondary server connections
                case DatabaseFactories.PlayerManagementDBSecondary:
                    // Use the PlayerManagementDB with secondary server connection string from the config
                    return Config.PlayerManagementDbSecondary;
                case DatabaseFactories.MLabDBSecondary:
                    // Use the PlayerManagementDB with secondary server connection string from the config
                    return Config.MlabDbSecondary;


                default:
                    throw new ArgumentOutOfRangeException(nameof(factory), "Invalid database factory specified.");
            }

        }
    }

}

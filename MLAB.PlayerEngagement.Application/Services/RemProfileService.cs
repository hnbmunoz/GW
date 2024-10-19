using System.Data;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Response;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Infrastructure.Repositories;

namespace MLAB.PlayerEngagement.Application.Services;

public class RelationshipManagementService : IRelationshipManagementService
{
    private readonly ILogger<RelationshipManagementService> _logger;
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ISecondaryServerConnectionFactory _secondaryServerConnectionFactory;

    public RelationshipManagementService(ILogger<RelationshipManagementService> logger, IMainDbFactory mainDbFactory, ISecondaryServerConnectionFactory secondaryServerConnectionFactory)
    {
        _logger = logger;
        _mainDbFactory = mainDbFactory;
        _secondaryServerConnectionFactory = secondaryServerConnectionFactory;
    }
    #region RemProfile

    public async Task<bool> UpdateRemProfileStatus(UpdateRemProfileRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | UpdateRemProfileStatus - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<int>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_UpdateRemProfileStatus, new
                    {
                        RemProfileID = request.RemProfileID,
                        UserId = request.UserId,
                        OnlineStatusId = request.OnlineStatusId,
                        AgentConfigStatusId = request.AgentConfigStatusId
                    }

                );
            
            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | SavePlayerContactAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ValidateRemProfileNameAsync(ValidateRemProfileNameRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | ValidateRemProfileNameAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_ValidateRemProfileName, new
                                {
                                    RemProfileName = request.RemProfileName,
                                    RemProfileId = request.RemProfileId
                                }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | ValidateRemProfileNameAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<List<RemProfileReusableResponseModel>> GetReusableRemProfileDetails()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<RemProfileReusableResponseModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetReusableRemProfileDetails,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllBrandAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<RemProfileReusableResponseModel>().ToList();
    }

    public async Task<bool> ValidateRemProfileIfHasPlayer(int remProfileId)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | ValidateRemProfileIfHasPlayer - {remProfileId}");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<int>
                (
                    DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_ValidateRemProfileIfHasPlayer, new
                    {
                        RemProfileID = remProfileId,
                        
                    }

                );

            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | ValidateRemProfileIfHasPlayer : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<Tuple<int, List<RemProfileFilterModel>>> GetRemProfileByFilterAsync(RemProfileFilterRequestModel request)
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<Int32, RemProfileFilterModel>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetRemProfileByFilter, new
                    {
                        RemProfileID = request.RemProfileID,
                        RemProfileName = request.RemProfileName,
                        AgentIds = request.AgentNameIds,
                        PseudoNamePP = request.PseudoNamePP,
                        OnlineStatusId = request.OnlineStatusId,
                        AgentConfigStatusId = request.AgentConfigStatusId,
                        ScheduleTemplateSettingId = request.ScheduleTemplateSettingId,
                        PageSize = request.PageSize,
                        OffsetValue = request.OffsetValue,
                        SortColumn = string.IsNullOrEmpty(request.SortColumn) ? "a.CreatedDate" : request.SortColumn,
                        SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "Asc" : request.SortOrder
                    }

                ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.FirstOrDefault(), result.Item2.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | GetRemProfileByFilterAsync : [Exception] - {ex.Message}");
        }
        return Tuple.Create(Enumerable.Empty<Int32>().FirstOrDefault(), Enumerable.Empty<RemProfileFilterModel>().ToList());
    }

    #endregion

    #region Rem Distribution
    public async Task<RemDistributionPlayerResponseModel> GetRemDistributionPlayerAsync(RemProfileFilterRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | GetRemDistributionPlayerAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<RemDistributionPlayerResponseModel>
                            (DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetRemDistributionByPlayerId, new
                                {
                                    MlabPlayerId = request.MlabPlayerId
                                }

                            ).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetRemDistributionPlayerAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<RemDistributionPlayerResponseModel>().FirstOrDefault();
    }

    public async Task<RemDistributionFilterResponseModel> GetRemDistributionByFilterAsync(RemDistributionFilterRequestModel request)
    {
        try
        {
            _logger.LogError($"{Factories.RemProfileFactory} | GetRemDistributionByFilterAsync in gateway: request: {request}");
            var result = await _mainDbFactory
                                .ExecuteQueryMultipleAsync<RemDistributionModel, int>
                                    (DatabaseFactories.PlayerManagementDB,
                                        StoredProcedures.USP_GetRemDistributionByFilter, new
                                        {
                                            @RemProfileId = request.RemProfileId == 0 ? null : request.RemProfileId.ToString(),
                                            @AgentIds = String.IsNullOrWhiteSpace(request.AgentIds) ? null : request.AgentIds,
                                            @PseudoNames = String.IsNullOrWhiteSpace(request.PseudoNames) ? null : request.PseudoNames,
                                            @PlayerId = String.IsNullOrWhiteSpace(request.PlayerId) ? null : request.PlayerId,
                                            @Username = String.IsNullOrWhiteSpace(request.UserName) ? null : request.UserName,
                                            @StatusId = request.StatusId == 0 ? null : request.StatusId.ToString(),
                                            @CurrencyIds = String.IsNullOrWhiteSpace(request.CurrencyIds) ? null : request.CurrencyIds,
                                            @BrandId = request.BrandId,
                                            @VIPLevelIds = String.IsNullOrWhiteSpace(request.VipLevelIds) ? null : request.VipLevelIds,
                                            @AssignStatus = request.AssignStatus,
                                            @DistributionDateStart = String.IsNullOrWhiteSpace(request.DistributionDateStart) ? null : request.DistributionDateStart.ToLocalDateTime(),
                                            @DistributionDateEnd = String.IsNullOrWhiteSpace(request.DistributionDateEnd) ? null : request.DistributionDateEnd.ToLocalDateTime(),
                                            @AssignedByIds = String.IsNullOrWhiteSpace(request.AssignedByIds) ? null : request.AssignedByIds,
                                            @PageSize = request.PageSize,
                                            @OffsetValue = request.OffsetValue,
                                            @SortColumn = String.IsNullOrWhiteSpace(request.SortColumn) ? null : request.SortColumn,
                                            @SortOrder = String.IsNullOrWhiteSpace(request.SortOrder) ? null : request.SortOrder,
                                            @UserId = request.UserId,
                                        }

                                    ).ConfigureAwait(false);

            return new RemDistributionFilterResponseModel
            {
                RemDistributionList = result.Item1.ToList(),
                RecordCount = result.Item2.First()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | GetRemDistributionByFilterAsync : [Exception] - {ex.Message}");
            return Enumerable.Empty<RemDistributionFilterResponseModel>().FirstOrDefault();
        }
    }

    #endregion

    #region Rem History
    public async Task<RemHistoryFilterResponseModel> GetRemHistoryByFilterAsync(RemHistoryFilterRequestModel request)
    {
        try
        {
            var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("ReMDistributionPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

            _logger.LogInfo($"{Factories.PlayerFactor} | GetRemHistoryByFilterAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.PlayerFactor} | GetRemHistoryByFilterAsync | Start Time : {DateTime.Now}");
            _logger.LogInfo($"{Factories.PlayerFactor} | GetRemHistoryByFilterAsync | ConnectionStringDb - [{connectionString}]");

            var result = await _mainDbFactory
                                            .ExecuteQueryMultipleAsync<int, RemHistoryModel>
                                                (connectionString,
                                                    StoredProcedures.USP_GetRemHistoryByFilter, new
                                                    {
                                                        @ActionTypeIds = request.ActionTypeIds,
                                                        @RemProfileIds = request.RemProfileIds,
                                                        @AgentIds = request.AgentIds,
                                                        @PseudoName = request.PseudoName,
                                                        AssignmentDateFrom = String.IsNullOrWhiteSpace(request.AssignmentDateStart) ? null : request.AssignmentDateStart.ToLocalDateTime(),
                                                        @AssignmentDateTo = String.IsNullOrWhiteSpace(request.AssignmentDateEnd) ? null : request.AssignmentDateEnd.ToLocalDateTime(),
                                                        @MlabPlayerId = request.MlabPlayerId,
                                                        @PageSize = request.PageSize,
                                                        @OffsetValue = request.OffsetValue,
                                                        @SortColumn = request.SortColumn,
                                                        @SortOrder = request.SortOrder
                                                    }

                                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.PlayerFactor} | GetRemHistoryByFilterAsync | End Time : {DateTime.Now}");

            return new RemHistoryFilterResponseModel
            {
                RemHistoryList = result.Item2.ToList(),
                RecordCount = result.Item1.First()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetRemHistoryByFilterAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<RemHistoryFilterResponseModel>().FirstOrDefault();
    }
    #endregion

    #region RemDistribution
    public async Task<bool> RemoveRemDistributionAsync(long remDistributionId, long userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | RemoveRemDistributionAsync - [Received] RemDistributionId: {JsonConvert.SerializeObject(remDistributionId)}");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<bool>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_DeleteRemDistribution, new
                    {
                        RemDistributionId = remDistributionId,
                        DeletedBy = userId
                    }

                );

            _logger.LogInfo($"{Factories.PlayerFactor} | RemoveRemDistributionAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | RemoveRemDistributionAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> UpsertRemDistributionAsync(UpsertRemDistributionRequestModel[] request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | UpsertRemDistributionAsync - [Received] RemDistributionId: {JsonConvert.SerializeObject(request)}");

            DataTable templateData = null;
            long requestId = (request != null && request.Any()) ? request[0].RemDistributionId : 0;

            if (request != null && request.Any())
                templateData = DataTableConverter.ToDataTable(request.ToList());

            // List of column names to remove
            List<string> columnsToRemove = new List<string> { "PlayerId", "HasIntegration" };

            // Remove the columns
            foreach (string columnName in columnsToRemove)
            {
                if (templateData.Columns.Contains(columnName))
                {
                    templateData.Columns.Remove(columnName);
                }
            }

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<bool>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_UpsertRemDistribution, new
                    {
                        @RemDistributionId = requestId,
                        @RemDistributionType = templateData
                    }

                );

            _logger.LogInfo($"{Factories.PlayerFactor} | UpsertRemDistributionAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | UpsertRemDistributionAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ValidateTemplateSettingAsync(ValidateTemplateRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | ValidateTemplateSettingAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<bool>
                (   DatabaseFactories .PlayerManagementDB,
                    StoredProcedures.USP_ValidateTemplateSetting, new
                    {
                        ScheduleTemplateSettingId = request.ScheduleTemplateSettingId,
                        ScheduleTemplateName = request.ScheduleTemplateName,
                        IsAdd = request.IsAdd,
                    }

                );

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | ValidateTemplateSettingAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    #endregion

    #region Rem Setting
    public async Task<ScheduleTemplateListResponseModel> GetScheduleTemplateSettingListAsync(ScheduleTemplateListRequestModel request)
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<int, ScheduleTemplateResponseModel>
                (   
                DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetScheduleTemplateSettingList, new
                    {
                        @ScheduleTemplateName = request.ScheduleTemplateName,
                        @CreatedBy = string.IsNullOrEmpty(request.CreatedBy) ? (int?)null : Int32.Parse(request.CreatedBy),
                        @UpdatedBy = string.IsNullOrEmpty(request.UpdatedBy) ? (int?)null : Int32.Parse(request.UpdatedBy),
                        @PageSize = request.PageSize,
                        @OffsetValue = request.OffsetValue,
                        @SortColumn = string.IsNullOrEmpty(request.SortColumn) ? "IsNull(a.CreatedDate,a.UpdatedDate)" : request.SortColumn,
                        @SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "desc" : request.SortOrder,
                    }

                ).ConfigureAwait(false);

            return new ScheduleTemplateListResponseModel
            {
                TotalRecordCount = result.Item1.FirstOrDefault(),
                ScheduleTemplateResponseList = result.Item2.ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetScheduleTemplateSettingListAsync - ExceptionMessage:{ex.Message} Stack Trace: {ex.StackTrace}");
            return new ScheduleTemplateListResponseModel()
            {
                TotalRecordCount = Enumerable.Empty<Int32>().FirstOrDefault(),
                ScheduleTemplateResponseList = Enumerable.Empty<ScheduleTemplateResponseModel>().ToList()
            };
        }
    }
    #endregion

    #region AutoDistributionSetting
    public async Task<bool> UpdateMaxPlayerCountConfigAsync(UpdateMaxPlayerCountConfigRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.RemProfileFactory} | UpdateMaxPlayerCountConfigAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<bool>
                (DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_UpdateMaxPlayerCountConfig, new
                    {
                        AgentNameId = request.AgentNameId,
                        MaxPlayerCount = request.MaxPlayerCount,
                        @UserId = request.UserId,
                    }

                );

            _logger.LogInfo($"{Factories.RemProfileFactory} | UpdateMaxPlayerCountConfigAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | UpdateMaxPlayerCountConfigAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> RemoveDistributionbyVipLevelAsync(RemoveDistributionByVIPLevelRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.RemProfileFactory} | RemoveDistributionbyVipLevelAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<bool>
                (DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_RemoveDistributionbyVipLevel, new
                    {
                        VipLevelIds = request.VipLevelIds,
                        UserId = request.UserId
                    }

                );

            _logger.LogInfo($"{Factories.RemProfileFactory} | RemoveDistributionbyVipLevelAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | RemoveDistributionbyVipLevelAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> UpdateAutoDistributionSettingPriorityAsync(UpdateAutoDistributionSettingPriorityRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.RemProfileFactory} | UpdateAutoDistributionSettingPriorityAsync - {JsonConvert.SerializeObject(request)}");

            var AutoConfigurationsTypeUDT = DataTableConverter.ToDataTable(request.AutoConfigurations);

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<bool>
                (DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_UpdateAutoDistributionSettingPriority, new
                    {
                        @AutoConfigurations = AutoConfigurationsTypeUDT,
                        @UserId = request.UserId
                    }

                );

            _logger.LogInfo($"{Factories.RemProfileFactory} | UpdateAutoDistributionSettingPriorityAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | UpdateAutoDistributionSettingPriorityAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> UpdateAutoDistributionConfigurationStatusAsync(UpdateAutoDistributionConfigStatusRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.RemProfileFactory} | UpdateAutoDistributionConfigurationStatusAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<bool>
                (DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_UpdateAutoDistributionConfigurationStatus, new
                    {
                        @AutoDistributionSettingId = request.AutoDistributionSettingId,
                        @StatusId = request.StatusId,
                        @UserId = request.Userid
                    }
                );

            _logger.LogInfo($"{Factories.RemProfileFactory} | UpdateAutoDistributionConfigurationStatusAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | UpdateAutoDistributionConfigurationStatusAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> DeleteAutoDistributionConfigurationByIdAsync(int autoDistributionSettingId)
    {
        try
        {
            _logger.LogInfo($"{Factories.RemProfileFactory} | DeleteAutoDistributionConfigurationByIdAsync - AutoDistributionSettingId = {autoDistributionSettingId}");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<bool>
                (DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_DeleteAutoDistributionConfigurationById, new
                    {
                        @AutoDistributionSettingId = autoDistributionSettingId,
                    }

                );

            _logger.LogInfo($"{Factories.RemProfileFactory} | DeleteAutoDistributionConfigurationByIdAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | DeleteAutoDistributionConfigurationByIdAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ValidateAutoDistributionConfigurationNameAsync(int autoDistributionSettingId, string configurationName)
    {
        try
        {
            _logger.LogInfo($"{Factories.RemProfileFactory} | ValidateAutoDistributionConfigurationNameAsync - AutoDistributionSettingId = {autoDistributionSettingId}");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<bool>
                (DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_ValidateAutoDistributionConfigurationName, new
                    {
                        @AutoDistributionSettingId = autoDistributionSettingId,
                        @ConfigurationName = configurationName
                    }

                );

            _logger.LogInfo($"{Factories.RemProfileFactory} | ValidateAutoDistributionConfigurationNameAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | ValidateAutoDistributionConfigurationNameAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<AutoDistributionConfigurationCountResponseModel> GetAutoDistributionConfigurationCountAsync(int userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.RemProfileFactory} | GetAutoDistributionConfigurationCountAsync - UserId = {userId}");

            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<int, int>
                (DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetAutoDistributionConfigurationCount, new
                    {
                        @UserId = userId,
                    }

                );

            _logger.LogInfo($"{Factories.RemProfileFactory} | GetAutoDistributionConfigurationCountAsync - [Response]: {JsonConvert.SerializeObject(result)}");
            return new AutoDistributionConfigurationCountResponseModel
            {
                UserConfigurationTotalCount = result.Item1.FirstOrDefault(),
                ConfigurationTotalCount = result.Item2.SingleOrDefault()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.RemProfileFactory} | GetAutoDistributionConfigurationCountAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<AutoDistributionConfigurationCountResponseModel>().FirstOrDefault();
    }

    public async Task<List<DistributionRemovedVipLevelsResponseModel>> GetRemovedVipLevels()
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | GetRemovedVipLevels");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<DistributionRemovedVipLevelsResponseModel>
                            (DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetRemovedVipLevels, null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetRemovedVipLevels : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<DistributionRemovedVipLevelsResponseModel>().ToList();
    }

    public async Task<List<AutoDistributionSettingConfigsListOrder>> GetAllAutoDistributionConfigListOrderAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.PlayerFactor} | GetAllAutoDistributionConfigListOrderAsync");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<AutoDistributionSettingConfigsListOrder>
                            (DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.Usp_GetRemAutoDistributionSettingConfigsListOrder, null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerFactor} | GetAllAutoDistributionConfigListOrderAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<AutoDistributionSettingConfigsListOrder>().ToList();
    }
    #endregion

    #region Shared
    public async Task<RemLookupsResponseModel> GetRemLookupsAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetRemLookupsAsync | Start Time : {DateTime.Now}");
            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<
                LookupModel // Standard look up model
                >(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_GetRemLookups, null, 7).ConfigureAwait(false);

            var resultList = result.ToList();

            _logger.LogInfo($"{Factories.SystemFactory} | GetRemLookupsAsync | End Time : {DateTime.Now}");
            return new RemLookupsResponseModel()
            {
                RemProfileNames = resultList[0].ToList(),
                RemAgentNames = resultList[1].ToList(),
                RemPseudoNames = resultList[2].ToList(),
                RemAssignedBys = resultList[3].ToList(),
                RemActionTypes = resultList[4].ToList(),
                Users = resultList[5].ToList(),
                ActiveRemProfileNames = resultList[6].ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetRemLookupsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<RemLookupsResponseModel>().FirstOrDefault();
    }

    public async Task<List<ScheduleTemplateModel>> GetAllScheduleTemplateListAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<ScheduleTemplateModel
                >(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_GetAllScheduleTemplateList, null).ConfigureAwait(false);

            return result.ToList();
           
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllScheduleTemplateListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<ScheduleTemplateModel>().ToList();
    }

    public async Task<List<MessageTypeContactDetailListResponseModel>> GetMessageTypeChannelListAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<MessageTypeContactDetailListResponseModel
                >(DatabaseFactories.MLabDB, StoredProcedures.USP_GetMessageTypeChannelList,null).ConfigureAwait(false);

            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetMessageTypeChannelListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<MessageTypeContactDetailListResponseModel>().ToList();
    }

    #endregion
}

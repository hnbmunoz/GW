using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;
using MLAB.PlayerEngagement.Core.Models.AppConfigSettings;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class AgentWorkspaceFactory : IAgentWorkspaceFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<AgentWorkspaceFactory> _logger;
    private readonly ISecondaryServerConnectionFactory _secondaryServerConnectionFactory;

    public AgentWorkspaceFactory(IMainDbFactory mainDbFactory, ILogger<AgentWorkspaceFactory> logger, ISecondaryServerConnectionFactory secondaryServerConnectionFactory)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
        _secondaryServerConnectionFactory = secondaryServerConnectionFactory;   
    }

    //check what DB to use
    private async Task<bool> LocalIsSecondaryDbEnabledAsync(string appConfigSettingKey)
    {
        try
        {
            var appConfigSettingFilterResult = await _mainDbFactory.ExecuteQueryMultipleAsync<AppConfigSettingResponseModel, int>(
                                                DatabaseFactories.MLabDB,
                                                StoredProcedures.Usp_GetAppConfigSettingByFilter,
                                                new
                                                {
                                                    ApplicationId = 383
                                                }).ConfigureAwait(false);

            return appConfigSettingFilterResult?.Item1?.Any(setting => setting.Key.ToString() == appConfigSettingKey && setting.Value == "false") ?? false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(AgentWorkspaceFactory)} | {nameof(LocalIsSecondaryDbEnabledAsync)} : [Exception] - {ex.Message}");
            throw;
        }
    }


    public async Task<CampaignPlayerFilterResponseModel> GetCampaignPlayerListByFilterAsync(CampaignPlayerFilterRequestModel request)
    {
        try
        {

            var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("AgentWorkspacePrimaryDbOn", DatabaseFactories.PlayerManagementDB);

            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetCampaignPlayerListByFilterAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetCampaignPlayerListByFilterAsync - DBConnectionString - [{connectionString}]");

            var result = await _mainDbFactory
                                    .ExecuteQueryAsync<CampaignPlayerModel>
                                        (
                                            connectionString,
                                            StoredProcedures.USP_GetCampaignPlayerListByFilter, new
                                            {
                                                @CampaignId = GetValueOrNull(request.CampaignId),
                                                @CampaignTypeId = GetValueOrNull(request.CampaignTypeId), 
                                                @CampaignStatus = GetValueOrNull(request.CampaignStatus),
                                                @AgentIds = GetValueOrNullIfWhiteSpace(request.AgentId), 
                                                @PlayerIds = GetValueOrNullIfWhiteSpace(request.PlayerId),
                                                @Usernames = GetValueOrNullIfWhiteSpace(request.Username),
                                                @PlayerStatusId = GetValueOrNull(request.PlayerStatus), 
                                                @MarketingSources = GetValueOrNullIfWhiteSpace(request.MarketingSource), 
                                                @CurrencyIds = GetValueOrNullIfWhiteSpace(request.Currency), 
                                                @RegisteredDateFrom = GetValueOrNullIfWhiteSpace(request.RegisteredDateStart), 
                                                @RegisteredDateTo = GetValueOrNullIfWhiteSpace(request.RegisteredDateEnd), 
                                                @LastLoginDateFrom = GetValueOrNullIfWhiteSpace(request.LastLoginDateStart), 
                                                @LastLoginDateTo = GetValueOrNullIfWhiteSpace(request.LastLoginDateEnd),
                                                @Deposited = GetValueOrNullIfWhiteSpace(request.Deposited),
                                                @FTDAmountFrom = request.FtdAmountFrom == null ? null : request.FtdAmountFrom.ToString(),
                                                @FTDAmountTo = request.FtdAmountTo == null ? null : request.FtdAmountTo.ToString(),
                                                @FTDDateFrom = GetValueOrNullIfWhiteSpace(request.FtdDateStart),
                                                @FTDDateTo = GetValueOrNullIfWhiteSpace(request.FtdDateEnd), 
                                                @TaggedDateFrom = GetValueOrNullIfWhiteSpace(request.TaggedDateStart),
                                                @TaggedDateTo = GetValueOrNullIfWhiteSpace(request.TaggedDateEnd),
                                                @PrimaryGoalReached = GetValueOrNullIfWhiteSpace(request.PrimaryGoalReached), 
                                                @PrimaryGoalCountFrom = request.PrimaryGoalCountFrom == null ? null : request.PrimaryGoalCountFrom.ToString(),
                                                @PrimaryGoalCountTo = request.PrimaryGoalCountTo == null ? null : request.PrimaryGoalCountTo.ToString(),
                                                @PrimaryGoalAmountFrom = request.PrimaryGoalAmountFrom == null ? null : request.PrimaryGoalAmountFrom.ToString(),
                                                @PrimaryGoalAmountTo = request.PrimaryGoalAmountTo == null ? null : request.PrimaryGoalAmountTo.ToString(),
                                                @CallListNote = GetValueOrNullIfWhiteSpace(request.CallListNotes), 
                                                @MobilePhone = GetValueOrNullIfWhiteSpace(request.MobileNumber),
                                                @MessageStatusResponseIds = GetValueOrNullIfWhiteSpace(request.MessageResponseAndStatus), 
                                                @CallCaseCreatedDateFrom = GetValueOrNullIfWhiteSpace(request.CallCaseCreatedDateStart), 
                                                @CallCaseCreatedDateTo = GetValueOrNullIfWhiteSpace(request.CallCaseCreatedDateEnd), 
                                                @InitialDepositAmount = request.InitialDepositAmount == null ? null : request.InitialDepositAmount.ToString(),
                                                @InitialDepositDateFrom = GetValueOrNullIfWhiteSpace(request.InitialDepositDateStart), 
                                                @InitialDepositDateTo = GetValueOrNullIfWhiteSpace(request.InitialDepositDateEnd), 
                                                @InitialDepositMethodIds = GetValueOrNullIfWhiteSpace(request.IntialDepositMethod), 
                                                @InitialDeposited = GetValueOrNullIfWhiteSpace(request.InitialDeposited),
                                                @MobileVerificationId = request.MobileVerificationStatusId,
                                                @PageSize = request.PageSize,
                                                @OffsetValue = request.OffsetValue,
                                                @SortColumn = request.SortColumn,
                                                @SortOrder = request.SortOrder,
                                                @UserId = request.UserId,
                                            }

                                        ).ConfigureAwait(false);

            return new CampaignPlayerFilterResponseModel
            {
                CampaignPlayers = result.ToList(),
                RecordCount = result.FirstOrDefault()?.TotalRecordCount ?? 0
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | GetCampaignPlayerListByFilterAsync : [Exception] - {ex.Message}");
            return Enumerable.Empty<CampaignPlayerFilterResponseModel>().FirstOrDefault();
        }

    }

    public async Task<CallListNoteResponseModel> GetCallListNote(int callListNoteId)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetCallListNote - [callListNoteId: {callListNoteId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<CallListNoteResponseModel>(
                DatabaseFactories.PlayerManagementDB,
                StoredProcedures.USP_GetCallListNote, new
                {
                    @CallListNoteId = callListNoteId
                }
                ).ConfigureAwait(false);

            return result.FirstOrDefault();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | GetCallListNote : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CallListNoteResponseModel>().FirstOrDefault();
    }

    public async Task<bool> SaveCallListNotes(CallListNoteRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | SaveCallListNotes - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_SaveCallListNote, new
                                {
                                    @CallListNoteId = request.CallListNoteId,
                                    @CampaignPlayerId = request.CampaignPlayerId,
                                    @Note = request.Note,
                                    @UserId = request.UserId
                                }

                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | SaveCallListNotes : [Exception] - {ex.Message}");
        }

        return false;
    }
    public async Task<bool> TagAgentAsync(TagAgentRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | TagAgentAsync - {JsonConvert.SerializeObject(request)}");

            DataTable templateData = null;

            if (request.TagList != null && request.TagList.Any())
                templateData = DataTableConverter.ToDataTable(request.TagList.ToList());

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (
                            DatabaseFactories.PlayerManagementDB,
                            StoredProcedures.USP_TagPlayer, new
                            {
                                @CampaignPlayerAgentType = templateData
                            }

                        ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | TagAgentAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> DiscardPlayerAsync(DiscardAgentRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | DiscardPlayerAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (
                            DatabaseFactories.PlayerManagementDB,
                            StoredProcedures.USP_DiscardPlayer, new
                            {
                                @CampaignPlayerIds = request.CampaignPlayerIds,
                                @UserId = request.UserId
                            }  

                        ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | DiscardPlayerAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<List<LookupModel>> GetAllCampaignAsync(int campaignType)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LookupModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaign, new
                                {
                                    @Campaigntype = campaignType
                                }
                                
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | GetAllCampaignAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<LookupModel>> GetCampaignAgentsAsync(int campaignId)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetCampaignAgentsAsync - [campaignId: {campaignId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LookupModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignAgents, new
                                {
                                    @CampaignId = campaignId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | GetCampaignAgentsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<MessageStatusResponseModel>> GetMessageStatusResponseListAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<MessageStatusResponseModel>
                            (DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetMessageStatusMessageResponseList, 
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | GetMessageStatusResponseListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<MessageStatusResponseModel>().ToList();
    }

    public async Task<bool> ValidateTaggingAsync(ValidateTagAgentRequestModel request)
    {
        try
        {

            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | ValidateTaggingAsync - {JsonConvert.SerializeObject(request)}");

            foreach (var item in request.TagList)
            {
                var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<bool>(DatabaseFactories.PlayerManagementDB,
                                   StoredProcedures.USP_ValidateTaggingById, new
                                   {
                                       @CampaignPlayerId = item.CampaignPlayerId,
                                       @UserId = item.UserId,
                                       @IsLeader = item.IsLeader
                                   }
                            ).ConfigureAwait(false);

                if (!result)
                {
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | ValidateTaggingAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<List<PlayerDepositAttemptsResponseModel>> GetPlayerDepositAttemptsAsync(int campaignPlayerId)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetPlayerDepositAttemptsAsync - [campaignId: {campaignPlayerId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<PlayerDepositAttemptsResponseModel>
                            (DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetPlayerDepositAttempts, new
                                {
                                    @CampaignPlayerId = campaignPlayerId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | GetPlayerDepositAttemptsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<PlayerDepositAttemptsResponseModel>().ToList();
    }

    private static string GetValueOrNull(int value)
    {
        return value == 0 ? null : value.ToString();
    }

    private static string GetValueOrNullIfWhiteSpace(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }

    public async Task<ServiceCommunicationHistoryResponseModel> USP_GetCommunicationHistoryByFilter(ServiceCommunicationHistoryFilterRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | USP_GetCommunicationHistoryByFilter : [Param] - {JsonConvert.SerializeObject(request)}");
            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<ServiceCommunicationHistoryModel,int>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCommunicationHistoryByFilter, request

                            ).ConfigureAwait(false);

            return new ServiceCommunicationHistoryResponseModel 
            {
                CampaignServiceCommunications = result.Item1.ToList(),
                RecordCount = result.Item2.FirstOrDefault()
            };

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | USP_GetCommunicationHistoryByFilter : [Exception] - {ex.Message}");
        }
        return new ServiceCommunicationHistoryResponseModel { CampaignServiceCommunications = new List<ServiceCommunicationHistoryModel>(),RecordCount = 0 };
    }
}

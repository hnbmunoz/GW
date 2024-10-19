using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Models.CallListValidation;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;
using MassTransit.Internals.GraphValidation;
using System;
using RabbitMQ.Client;
using MLAB.PlayerEngagement.Infrastructure.Communications;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CallListValidationFactory : ICallListValidationFactory
{

    private readonly IMainDbFactory _mainDbFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CallListValidationFactory> _logger;
    private readonly ISecondaryServerConnectionFactory _secondaryServerConnectionFactory;

    #region Constructor
    public CallListValidationFactory(IMainDbFactory mainDbFactory, IConfiguration configuration, ILogger<CallListValidationFactory> logger, ISecondaryServerConnectionFactory secondaryServerConnectionFactory)
    {
        _mainDbFactory = mainDbFactory;
        _configuration = configuration;
        _logger = logger;
        _secondaryServerConnectionFactory = secondaryServerConnectionFactory;   
    }
    #endregion


    public async Task<CallValidationFilterResponseModel> GetCallListValidationFilterAsync(int campaignId)
    {
        
        try
        {
            _logger.LogInfo($"{Factories.CallListValidationFactory} | GetCallListValidationFilterAsync - [campaignId: {campaignId}]");
            _logger.LogInfo($"{Factories.CallListValidationFactory}| GetCallListValidationFilterAsync | Start Time : {DateTime.Now}");
            
            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<dynamic>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.Usp_GetCallListValidationFilter, new
                                {
                                    CampaignId = campaignId
                                },5

                            ).ConfigureAwait(false);

            var resultList = result.ToList();
            List<string> messageStatusAndResponseResult = new List<string>();
            List<string> playerIds = new List<string>();
            List<AgentFilterResponseModel> agentNames = new List<AgentFilterResponseModel>();
            List<string> userNames = new List<string>();
            List<LeaderJustificationFilterResponseModel> justifications = new List<LeaderJustificationFilterResponseModel>();


            for (int i = 0; i < resultList.Count; i++)
            {
                
                var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));
                List<Action> actions = new List<Action>
                {
                    () => messageStatusAndResponseResult.AddRange(resultList[i].Select(item => (string)item.MessageStatusAndResponseResult)),
                    () => playerIds.AddRange(resultList[i].Select(item => (string)item.PlayerId.ToString())),
                    () =>
                    {
                        var agentModels = DynamicConverter.ConvertToModels<AgentFilterResponseModel>(dictionaries);
                        agentNames.AddRange(agentModels);
                    },
                    () => userNames.AddRange(resultList[i].Select(item => (string)item.Username)),
                    () =>
                    {
                        var justificationModels = DynamicConverter.ConvertToModels<LeaderJustificationFilterResponseModel>(dictionaries);
                        justifications.AddRange(justificationModels);
                    },
                    // Add more actions as needed for other resultList indices
                };

                // Execute the action based on the index
                actions[i].Invoke();
            }
            _logger.LogInfo($"{Factories.CallListValidationFactory}| GetCallListValidationFilterAsync | End Time : {DateTime.Now}");
            return new CallValidationFilterResponseModel()
            {
                CallCaseStatusOutcomes = messageStatusAndResponseResult,
                PlayerIds = playerIds,
                AgentNames = agentNames,
                UserNames = userNames,
                Justifications = justifications
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CallListValidationFactory} | GetCallListValidationFilterAsync : [Exception] - {ex.Message}");

            return new CallValidationFilterResponseModel();
        }
    }

    public async Task<CallValidationListResponseModel> GetCallValidationListAsync(CallValidationListRequestModel request)
    {

        try
        {
            var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("CallListValidationPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

            _logger.LogInfo($"{Factories.CallListValidationFactory} | GetCallValidationListAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.CallListValidationFactory} | GetCallValidationListAsync | Start Time : {DateTime.Now}");
            _logger.LogInfo($"{Factories.CallListValidationFactory} | GetCallValidationListAsync | DBConnectionString - [{connectionString}]");

            
            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<dynamic>
                (
                    //DatabaseFactories.PlayerManagementDB,
                    connectionString,
                    StoredProcedures.Usp_GetCallValidationList, new
                    {
                        CampaignId = request.CampaignId,
                        PlayerIds = request.PlayerId,
                        Usernames = request.Username,
                        AgentIds = request.AgentName,
                        PlayerStatusIds = request.Status,
                        CurrencyIds = request.Currency,
                        RegistrationStartDate = request.RegistrationStartDate?.ToLocalDateTimeString(),
                        RegistrationEndDate = request.RegistrationEndDate?.ToLocalDateTimeString(),
                        Deposited = request.Deposited,
                        FTDAmountFrom = request.FtdStartAmount,
                        FTDAmountTo = request.FtdEndAmount,
                        FTDDateFrom = request.FtdStartDate?.ToLocalDateTimeString(),
                        FTDDateTo = request.FtdEndDate?.ToLocalDateTimeString(),
                        TaggedDateFrom = request.TaggedStartDate?.ToLocalDateTimeString(),
                        TaggedDateTo = request.TaggedEndDate?.ToLocalDateTimeString(),
                        PrimaryGoalReached = request.PrimaryGoalReached,
                        PrimaryGoalCount = request.PrimaryGoalCount,
                        PrimaryGoalAmount = request.PrimaryGoalAmount,
                        CallListNote = request.CallListNotes,
                        MobilePhone = request.MobileNumber,
                        MessageStatusResponseIds = request.MessageStatusAndResponse,
                        CallCaseCreatedDateFrom = request.CallCaseCreatedStartDate?.ToLocalDateTimeString(),
                        CallCaseCreatedDateTo = request.CallCaseCreatedEndDate?.ToLocalDateTimeString(),
                        SystemValidations = request.SystemValidation,
                        AgentValidationStatuses = request.AgentValidation,
                        LeaderValidationStatuses = request.LeaderValidation,
                        LeaderJustificationIds = request.LeaderJustification,
                        CallEvaluationStartPoint = request.CallEvaluationStartPoint,
                        CallEvaluationEndPoint = request.CallEvaluationEndPoint,
                        CallEvaluationNotes = request.CallEvaluationNotes,
                        HighDeposit = request.HighDepositAmount,
                        AgentValidationNotes = request.AgentValidationNotes,
                        LeaderValidationNotes = request.LeaderValidationNotes,
                        PageSize = request.PageSize,
                        OffsetValue = request.OffsetValue,
                        SortColumn = request.SortColumn,
                        SortOrder = request.SortOrder
                    }, 5

                ).ConfigureAwait(false);

            var resultList = result.ToList();
            List<CallValidationResponseModel> callValidations = new List<CallValidationResponseModel>();
            List<AgentValidationsResponseModel> agentValidations = new List<AgentValidationsResponseModel>();
            List<LeaderValidationsResponseModel> leaderValidations = new List<LeaderValidationsResponseModel>();
            List<CallEvaluationResponseModel> callEvaluations = new List<CallEvaluationResponseModel>();
            int recordCount = 0;

                
            for (int i = 0; i < resultList.Count; i++)
            {
                var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));
                switch (i)
                {
                    case 0:
                        var callValidationsModels = DynamicConverter.ConvertToModels<CallValidationResponseModel>(dictionaries);
                        callValidations.AddRange(callValidationsModels);
                        break;
                    case 1:
                        var agentValidationsModels = DynamicConverter.ConvertToModels<AgentValidationsResponseModel>(dictionaries);
                        agentValidations.AddRange(agentValidationsModels);
                        break;
                    case 2:
                        var leaderValidationsModels = DynamicConverter.ConvertToModels<LeaderValidationsResponseModel>(dictionaries);
                        leaderValidations.AddRange(leaderValidationsModels);
                        break;
                    case 3:
                        var callEvaluationsModels = DynamicConverter.ConvertToModels<CallEvaluationResponseModel>(dictionaries);
                        callEvaluations.AddRange(callEvaluationsModels);
                        break;
                    case 4:
                        recordCount = resultList[4].FirstOrDefault().TotalRecordCount;
                        break;
                        // Add more cases if needed for other resultList indices
                    default:
                        // Handle other cases here
                        break;
                }
            }
            _logger.LogInfo($"{Factories.CallListValidationFactory} | GetCallValidationListAsync | End Time : {DateTime.Now}");
            return new CallValidationListResponseModel()
            {
                CallValidations = callValidations,
                AgentValidations = agentValidations,
                LeaderValidations = leaderValidations,
                CallEvaluations = callEvaluations,
                RecordCount = recordCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CallListValidationFactory} | GetCallValidationListAsync : [Exception] - {ex.Message}");

            return new CallValidationListResponseModel();
        }

    }

    public async Task<List<LeaderJustificationListResponseModel>> GetLeaderJustificationListAsync()
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryAsync<LeaderJustificationListResponseModel>
                (
                    DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.Usp_GetLeaderJustificationList,
                    null
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CallListValidationFactory} | GetLeaderJustificationListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LeaderJustificationListResponseModel>().ToList();
    }
}

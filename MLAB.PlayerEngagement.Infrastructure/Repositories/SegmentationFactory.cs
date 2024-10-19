using System.Data;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Segmentation;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class SegmentationFactory : ISegmentationFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<SegmentationFactory> _logger;
    private readonly ISecondaryServerConnectionFactory _secondaryServerConnectionFactory;

    public SegmentationFactory(IMainDbFactory mainDbFactory, ILogger<SegmentationFactory> logger, ISecondaryServerConnectionFactory secondaryServerConnectionFactory)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
        _secondaryServerConnectionFactory = secondaryServerConnectionFactory;
    }

    public async Task<bool> DeactivateSegmentAsync(int SegmentId, int userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | DeactivateSegmentAsync - [SegmentId: {SegmentId}, userId: {userId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (       DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_DeactivateSegment, new
                                {
                                    Id = SegmentId,
                                    userId = userId
                                }

                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | DeactivateSegmentAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<List<SegmentationFilterFieldModel>> GetSegmentConditionFieldsAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<SegmentationFilterFieldModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetSegmentConditionFields,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetSegmentConditionFieldsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<SegmentationFilterFieldModel>().ToList();
    }

    public async Task<List<SegmentationFilterOperatorModel>> GetSegmentConditionOperatorsAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<SegmentationFilterOperatorModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetRelationalOperators,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetSegmentConditionOperatorsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<SegmentationFilterOperatorModel>().ToList();
    }

    public async Task<Tuple<SegmentationModel, List<SegmentationConditionModel>, List<SegmentVarianceModel>>> GetSegmentByIdAsync(int segmentId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetSegmentByIdAsync - [segmentId: {segmentId}]");

            var result = await _mainDbFactory
                                    .ExecuteQueryMultipleAsync<SegmentationModel, SegmentationConditionModel, SegmentVarianceModel>
                                        (DatabaseFactories.PlayerManagementDB,
                                            StoredProcedures.USP_GetSegmentById, new
                                            {
                                                @SegmentId = segmentId
                                            }

                                        ).ConfigureAwait(false);

            return Tuple.Create(result.Item1.First(), result.Item2.ToList(), result.Item3.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetSegmentByIdAsync : [Exception] - {ex.Message}");
            return Tuple.Create(Enumerable.Empty<SegmentationModel>().First(),
                                Enumerable.Empty<SegmentationConditionModel>().ToList(),
                                Enumerable.Empty<SegmentVarianceModel>().ToList());
        }

    }

    public async Task<bool> ValidateSegmentAsync(int? segmentId, string segmentName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | ValidateSegmentNameAsync - [segmentName: {segmentName}]");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<int>(DatabaseFactories.PlayerManagementDB,
                                   StoredProcedures.USP_ValidateSegment, new
                                   {
                                       @SegmentId = segmentId,
                                       @SegmentName = segmentName
                                   }
                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | ValidateSegmentNameAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<List<SegmentPlayer>> TestSegmentAsync(SegmentationTestModel request)
    {
        try
        {
            var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("SegmentationPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

            _logger.LogInfo($"{Factories.SegmentationFactory} | TestSegmentAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.SegmentationFactory} | TestSegmentAsync | Start Time : {DateTime.Now}");
            _logger.LogInfo($"{Factories.SegmentationFactory} | TestSegmentAsync | DBConnectionString - [{connectionString}]");

            var result = await _mainDbFactory
                            .ExecuteQueryAsync<SegmentPlayer>
                                (connectionString,
                                    StoredProcedures.USP_TestSegment, new
                                    {
                                        @QueryForm = request.QueryForm,
                                        @PageSize = request.PageSize,
                                        @OffsetValue = request.OffsetValue,
                                        @SortColumn = request.SortColumn,
                                        @SortOrder = request.SortOrder,
                                        @SegmentId = request.SegmentId,
                                        @QueryFormJoinTables = request.QueryJoins,
                                        @UserId = request.UserId
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | TestSegmentAsync | End Time : {DateTime.Now}");

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | TestSegmentAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<SegmentPlayer>().ToList();
    }

    public async Task<SegmentationTestResponseModel> TestStaticSegmentAsync(SegmentationTestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | TestSegmentAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                            .ExecuteQueryMultipleAsync<SegmentPlayer, int>
                                (DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_TestSegment, new
                                    {
                                        @QueryForm = request.QueryForm,
                                        @PageSize = request.PageSize,
                                        @OffsetValue = request.OffsetValue,
                                        @SortColumn = request.SortColumn,
                                        @SortOrder = request.SortOrder
                                    }

                                ).ConfigureAwait(false);


            return new SegmentationTestResponseModel
            {
                Players = result.Item1.ToList(),
                RecordCount = result.Item2.First()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | TestSegmentAsync : [Exception] - {ex.Message}");
        }

        return new SegmentationTestResponseModel();
    }

    public async Task<SegmentationToStaticResponseModel> ToStaticSegmentation(SegmentationToStaticModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | ToStaticSegmentation - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                                        .ExecuteQueryMultipleAsync<SegmentationModel, SegmentPlayer, string>
                                            (DatabaseFactories.PlayerManagementDB,
                                                StoredProcedures.USP_GetStaticSegment, new
                                                {
                                                    @SegmentId = request.SegmentId
                                                }

                                            ).ConfigureAwait(false);

            return new SegmentationToStaticResponseModel
            {
                Segmentation = result.Item1.First(),
                Players = result.Item2.ToList(),
                PlayerIdList = result.Item3.First()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | ToStaticSegmentation : [Exception] - {ex.Message}");
            return new SegmentationToStaticResponseModel
            {
                PlayerIdList = "",
                Players = Enumerable.Empty<SegmentPlayer>().ToList()
            };
        }
    }

    public async Task<List<SegmentConditionSetResponseModel>> GetSegmentConditionSetByParentIdAsync(int ParentSegmentConditionFieldId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetSegmentConditionSetByParentIdAsync : [Request] - {ParentSegmentConditionFieldId}");

            var result = await _mainDbFactory
                            .ExecuteQueryAsync<SegmentConditionSetResponseModel>
                                (DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetSegmentConditionSetByParentId, new
                                    {
                                        @ParentSegmentConditionFieldId = ParentSegmentConditionFieldId
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | GetSegmentConditionSetByParentIdAsync : [Response] - {result.ToList().Count()}");
            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetSegmentConditionSetByParentIdAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<SegmentConditionSetResponseModel>().ToList();
    }

    public async Task<List<LookupModel>> GetCampaignGoalNamesByCampaignIdAsync(int CampaignId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetCampaignGoalNamesByCampaignIdAsync : [Request] - {CampaignId}");

            var result = await _mainDbFactory
                            .ExecuteQueryAsync<LookupModel>
                                (DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCampaignGoalNamesByCampaignId, new
                                    {
                                        @CampaignId = CampaignId
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | GetCampaignGoalNamesByCampaignIdAsync : [Response] - {result.ToList().Count()}");
            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetCampaignGoalNamesByCampaignIdAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<LookupModel>> GetVariancesBySegmentIdAsync(int SegmentId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetVariancesBySegmentIdAsync : [Request] - {SegmentId}");

            var result = await _mainDbFactory
                            .ExecuteQueryAsync<LookupModel>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetVariancesBySegmentId, new
                                    {
                                        @SegmentId = SegmentId
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | GetVariancesBySegmentIdAsync : [Response] - {result.ToList().Count()}");
            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetVariancesBySegmentIdAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<Tuple<List<SegmentationFilterFieldModel>, List<SegmentationFilterOperatorModel>, List<SegmentFieldLookupModel>>> GetSegmentLookupsAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetSegmentLookupsAsync");

            var result = await _mainDbFactory
                                    .ExecuteQueryMultipleAsync<SegmentationFilterFieldModel, SegmentationFilterOperatorModel, SegmentFieldLookupModel>
                                        (   DatabaseFactories.PlayerManagementDB,
                                            StoredProcedures.USP_GetSegmentLookups,
                                            null

                                        ).ConfigureAwait(false);

            return Tuple.Create(result.Item1.ToList(), result.Item2.ToList(), result.Item3.ToList());
         
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetSegmentLookupsAsync : [Exception] - {ex.Message}");
            return Tuple.Create(Enumerable.Empty<SegmentationFilterFieldModel>().ToList(),
                                Enumerable.Empty<SegmentationFilterOperatorModel>().ToList(),
                                Enumerable.Empty<SegmentFieldLookupModel>().ToList());
        }

    }

    public async Task<Tuple<InFilePlayersValidationCount, List<InFilePlayersInvalidRemarks>>> ValidateInFilePlayersAsync(ValidateInFileRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | ValidateInfilePlayers");

            var data = DataTableConverter.ToDataTable(request.playerList.playerIds);

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<InFilePlayersValidationCount, InFilePlayersInvalidRemarks>
                        (       DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_ValidateInFileSegmentPlayers, new
                                {
                                    @InFileSegmentPlayerType = data,
                                    @Brand = request.playerList.brandName,
                                    @UserId = request.UserId
                                }

                            ).ConfigureAwait(false);

            return Tuple.Create(result.Item1.First(), result.Item2.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | ValidateInfilePlayers : [Exception] - {JsonConvert.SerializeObject(ex)}");
        }

        return Tuple.Create(Enumerable.Empty<InFilePlayersValidationCount>().First(),
                                Enumerable.Empty<InFilePlayersInvalidRemarks>().ToList());
    }
    
    public async Task<bool> TriggerVarianceDistributionAsync(TriggerVarianceDistributionRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | TriggerVarianceDistributionAsync : [Request] - {request}");

            var result = await _mainDbFactory
                            .ExecuteQuerySingleOrDefaultAsync<bool>
                                (DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_SegmentVarianceDistribution, new
                                    {
                                        @SegmentId = request.SegmentId,
                                        @UserId = request.UserId,
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | TriggerVarianceDistributionAsync : [Response] - {result}");
            return result;

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | TriggerVarianceDistributionAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<SegmentDistributionByFilterResponseModel> GetVarianceDistributionForCSVAsync(SegmentDistributionByFilterRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetVarianceDistributionForCSVAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<int, SegmentDistributionUdt>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetSegmentDistributionByFilter, new
                    {
                        @SegmentId = string.IsNullOrEmpty(request.SegmentId) ? (int?)null : Int32.Parse(request.SegmentId),
                        @PlayerId = string.IsNullOrEmpty(request.PlayerId) ? (int?)null : Int32.Parse(request.PlayerId),
                        @UserName = string.IsNullOrEmpty(request.UserName) ? null : request.UserName,
                        @VarianceName = string.IsNullOrEmpty(request.VarianceName) ? null : request.VarianceName,
                        @PageSize = request.PageSize,
                        @OffsetValue = request.OffsetValue,
                        @SortColumn = string.IsNullOrEmpty(request.SortOrder) ? "PlayerId" : request.SortColumn,
                        @SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "desc" : request.SortOrder,
                        @UserId = request.UserId
                    }

                ).ConfigureAwait(false);

            return new SegmentDistributionByFilterResponseModel
            {
                TotalRecordCount = result.Item1.FirstOrDefault(),
                SegmentDistributions = result.Item2.ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetVarianceDistributionForCSVAsync - ExceptionMessage:{ex.Message} Stack Trace: {ex.StackTrace}");
            return new SegmentDistributionByFilterResponseModel()
            {
                TotalRecordCount = Enumerable.Empty<Int32>().FirstOrDefault(),
                SegmentDistributions = Enumerable.Empty<SegmentDistributionUdt>().ToList()
            };
        }
    }

    public async Task<List<SegmentationConditionModel>> GetSegmentConditionsBySegmentIdAsync(int SegmentId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetVarianceDistributionForCSVAsync - {SegmentId}");

            var result = await _mainDbFactory
                .ExecuteQueryAsync<SegmentationConditionModel>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetSegmentConditionsBySegmentId, new
                    {
                        @SegmentId = SegmentId
                    }

                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetVarianceDistributionForCSVAsync - ExceptionMessage:{ex.Message} Stack Trace: {ex.StackTrace}");
            return Enumerable.Empty<SegmentationConditionModel>().ToList();
        }
    }

    public async Task<List<LookupModel>> GetMessageStatusByCaseTypeIdAsync(string CaseTypeId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetMessageStatusByCaseTypeIdAsync : [Request] - {CaseTypeId}");

            var result = await _mainDbFactory
                            .ExecuteQueryAsync<LookupModel>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetMessageStatusByCaseTypeId, new
                                    {
                                        @CaseTypeId = CaseTypeId
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | GetMessageStatusByCaseTypeIdAsync : [Response] - {result.ToList().Count()}");
            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetMessageStatusByCaseTypeIdAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<LookupModel>> GetMessageResponseByMultipleIdAsync(string MessageStatusId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetMessageResponseByMultipleIdAsync : [Request] - {MessageStatusId}");

            var result = await _mainDbFactory
                            .ExecuteQueryAsync<LookupModel>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetMessageResponseByMultipleId, new
                                    {
                                        @MessageStatusId = MessageStatusId
                                    }

                                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | GetMessageResponseByMultipleIdAsync : [Response] - {result.ToList().Count()}");
            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetMessageResponseByMultipleIdAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<string> GetSegmentCustomQueryProhibitedKeywordsAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.SegmentationFactory} | GetSegmentCustomQueryProhibitedKeywordsAsync : [Request]");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<string>(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_GetSegmentCustomQueryProhibitedKeywords, new { }).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SegmentationFactory} | GetSegmentCustomQueryProhibitedKeywordsAsync : [Response] - {result}");
            return result;

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SegmentationFactory} | GetSegmentCustomQueryProhibitedKeywordsAsync : [Exception] - {ex.Message}");
        }

        return String.Empty;
    }
}

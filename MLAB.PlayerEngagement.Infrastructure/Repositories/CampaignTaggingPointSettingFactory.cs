using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;
using MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting.Response;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CampaignTaggingPointSettingFactory : ICampaignTaggingPointSettingFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<CampaignTaggingPointSettingFactory> _logger;

    #region Constructor
    public CampaignTaggingPointSettingFactory(IMainDbFactory mainDbFactory
                                            , ILogger<CampaignTaggingPointSettingFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }
    #endregion

    public async Task<List<CampaignSettingMasterReference>> GetCampaignSettingTypeSelectionAsync(int masterReferenceId, int masterReferenceIsParent)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCampaignSettingTypeSelectionAsync - [masterReferenceId: {masterReferenceId}, masterReferenceIsParent: {masterReferenceIsParent}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignSettingMasterReference>
                            (
                                DatabaseFactories.MLabDB,
                                StoredProcedures.USP_GetMasterReferenceList, new
                                {
                                    MasterReferenceId = masterReferenceId,
                                    MasterReferenceIsParent = masterReferenceIsParent
                                }

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignTaggingPointSettingFactory} | GetCampaignSettingTypeSelectionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CampaignSettingMasterReference>().ToList();
    }

    //  GET CAMPAIGN SETTING SEARCH RESULTS - AUTO TAGGING / POIN INCENTIVE
    public async Task<Tuple<Int64, List<AutoTaggingPointIncentiveResponseModel>>> GetCampaignSettingListAsync(AutoTaggingPointIncentiveFilterRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCampaignSettingListAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<Int64, AutoTaggingPointIncentiveResponseModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignSettingList, new
                                {
                                    CampaignSettingName = request.CampaignSettingName,
                                    IsActive = request.IsActive,
                                    CampaignSettingId = request.CampaignSettingId,
                                    CampaignSettingTypeId = request.CampaignSettingTypeId,
                                    DateFrom = request.DateFrom,
                                    DateTo = request.DateTo,

                                    CreatedDatetime = request.CreatedDate,
                                    PageSize = request.PageSize,
                                    OffsetValue = request.OffsetValue,
                                    SortColumn = request.SortColumn,
                                    SortOrder = request.SortOrder
                                }

                            ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.FirstOrDefault(), result.Item2.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignTaggingPointSettingFactory} | GetCampaignSettingListAsync : [Exception] - {ex.Message}");
        }
        return Tuple.Create(Enumerable.Empty<Int64>().FirstOrDefault(), Enumerable.Empty<AutoTaggingPointIncentiveResponseModel>().ToList());
    }

    //  GET CAMPAIGN SETTING SEARCH RESULTS - AUTO TAGGING / POIN INCENTIVE
    public async Task<AutoTaggingDetailsResponseModel> GetAutoTaggingDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetAutoTaggingDetailsByIdAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetAutoTaggingDetailsByIdAsync - Start Time : {DateTime.Now}" );

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<dynamic>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetAutoTaggingDetailsById, new
                                {
                                    CampaignSettingId = request.CampaignSettingId,
                                    CampaignSettingTypeId = request.CampaignSettingTypeId
                                } , 4

                            ).ConfigureAwait(false);

            var resultList = result.ToList();
            List<CampaignSettingModel> campaignSettings = new List<CampaignSettingModel>();
            List<TaggingConfigurationModel> taggingConfigurations = new List<TaggingConfigurationModel>();
            List<UserTaggingModel> userTaggings = new List<UserTaggingModel>();
            List<CampaignPeriodDetails> campaignPeriodDetails = new List<CampaignPeriodDetails>();


            for (int i = 0; i < resultList.Count; i++)
            {
                var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));
                List<Action> actions = new List<Action>
                {
                    () =>
                    {
                        var campaignSettingsModels = DynamicConverter.ConvertToModels<CampaignSettingModel>(dictionaries);
                        campaignSettings.AddRange(campaignSettingsModels);
                    },
                    () =>
                    {
                        var taggingConfigurationsSummaryModels = DynamicConverter.ConvertToModels<TaggingConfigurationModel>(dictionaries);
                        taggingConfigurations.AddRange(taggingConfigurationsSummaryModels);
                    },
                    () =>
                    {
                        var userTaggingsModels = DynamicConverter.ConvertToModels<UserTaggingModel>(dictionaries);
                        userTaggings.AddRange(userTaggingsModels);
                    },
                    () =>
                    {
                        var campaignPeriodDetailsModels = DynamicConverter.ConvertToModels<CampaignPeriodDetails>(dictionaries);
                        campaignPeriodDetails.AddRange(campaignPeriodDetailsModels);
                    },
                    // Add more actions as needed for other resultList indices
                };

                // Execute the action based on the index
                actions[i].Invoke();
            }
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetAutoTaggingDetailsByIdAsync - End Time : {DateTime.Now}");

            return new AutoTaggingDetailsResponseModel()
            {
                CampaignSettings = campaignSettings,
                TaggingConfigurations = taggingConfigurations,
                UserTaggings = userTaggings,
                CampaignPeriodDetails = campaignPeriodDetails
            };

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignTaggingPointSettingFactory} | GetAutoTaggingDetailsByIdAsync : [Exception] - {ex.Message}");
        }
        return new AutoTaggingDetailsResponseModel();
    }

    //  GET CAMPAIGN SETTING SEARCH RESULTS - AUTO TAGGING / POIN INCENTIVE
    public async Task<PointIncentiveDetailsByIdResponseModel> GetPointIncentiveDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetPointIncentiveDetailsByIdAsync - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetPointIncentiveDetailsByIdAsync | Start Time : {DateTime.Now}");
            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<dynamic>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetPointIncentiveDetailsById, new
                                {
                                    CampaignSettingId = request.CampaignSettingId,
                                    CampaignSettingTypeId = request.CampaignSettingTypeId
                                } , 4

                            ).ConfigureAwait(false);

            var resultList = result.ToList();
            List<CampaignSettingModel> campaignSettings = new List<CampaignSettingModel>();
            List<PointToIncentiveRangeConfigurationModel> pointToIncentiveRanges = new List<PointToIncentiveRangeConfigurationModel>();
            List<GoalParameterRangeConfigurationModel> goalParameterRanges = new List<GoalParameterRangeConfigurationModel>();
            List<CampaignPeriodDetails> campaignPeriodDetails = new List<CampaignPeriodDetails>();


            for (int i = 0; i < resultList.Count; i++)
            {
                var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));
                List<Action> actions = new List<Action>
                {
                    () =>
                    {
                        var campaignSettingsModels = DynamicConverter.ConvertToModels<CampaignSettingModel>(dictionaries);
                        campaignSettings.AddRange(campaignSettingsModels);
                    },
                    () =>
                    {
                        var pointToIncentiveRangesModels = DynamicConverter.ConvertToModels<PointToIncentiveRangeConfigurationModel>(dictionaries);
                        pointToIncentiveRanges.AddRange(pointToIncentiveRangesModels);
                    },
                    () =>
                    {
                        var goalParameterRangesModels = DynamicConverter.ConvertToModels<GoalParameterRangeConfigurationModel>(dictionaries);
                        goalParameterRanges.AddRange(goalParameterRangesModels);
                    },
                    () =>
                    {
                        var campaignPeriodDetailsModels = DynamicConverter.ConvertToModels<CampaignPeriodDetails>(dictionaries);
                        campaignPeriodDetails.AddRange(campaignPeriodDetailsModels);
                    },
                    // Add more actions as needed for other resultList indices
                };

                // Execute the action based on the index
                actions[i].Invoke();
            }
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetPointIncentiveDetailsByIdAsync | End Time : {DateTime.Now}");
            return new PointIncentiveDetailsByIdResponseModel()
            {
                CampaignSetting =  campaignSettings,
                PointToIncentiveRanges = pointToIncentiveRanges,
                GoalParameterRanges = goalParameterRanges,
                CampaignPeriodDetails = campaignPeriodDetails
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignTaggingPointSettingFactory} | GetPointIncentiveDetailsByIdAsync : [Exception] - {ex.Message}");
        }
        return new PointIncentiveDetailsByIdResponseModel();
    }

    public async Task<List<SegmentSelectionModel>> GetTaggingSegmentAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<SegmentSelectionModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.AT_USP_GetSegment,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignTaggingPointSettingFactory} | GetTaggingSegmentAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<SegmentSelectionModel>().ToList();
    }

    public async Task<List<UsersSelectionModel>> GetUsersByModuleAsync(int subMainModuleDetailId)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetUsersByModuleAsync - [subMainModuleDetailId: {subMainModuleDetailId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<UsersSelectionModel>
                            (
                    DatabaseFactories.UserManagementDb,
                                StoredProcedures.USP_GetUserByModule, new
                                {
                                    SubMainModuleDetailId = subMainModuleDetailId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignTaggingPointSettingFactory} | GetUsersByModuleAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<UsersSelectionModel>().ToList();
    }

    public async Task<bool> CheckCampaignSettingByNameIfExistAsync(CampaignSettingNameRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | CheckCampaignSettingByNameIfExistAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                .ExecuteQueryAsync<long>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetCampaignSettingByName, new
                    {
                        CampaignSettingName = request.CampaignSettingName,
                        CampaignSettingTypeId = request.CampaignSettingTypeId,
                        CampaignSettingId = request.CampaignSettingId

                    }

                ).ConfigureAwait(false);
            return result.FirstOrDefault() == 1;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignTaggingPointSettingFactory} | CheckCampaignSettingByNameIfExistAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

}
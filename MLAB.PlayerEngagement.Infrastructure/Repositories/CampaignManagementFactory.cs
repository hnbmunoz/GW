using System.Data;
using MLAB.PlayerEngagement.Core;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Enum;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Utilities;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CampaignManagementFactory : ICampaignManagementFactory
{
    
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<CampaignManagementFactory> _logger;
    #region Constructor
    public CampaignManagementFactory(IMainDbFactory mainDbFactory , ILogger<CampaignManagementFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;

    }
    #endregion

    public async Task<List<LookupModel>> GetAllSegmentAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<SegmentListModel>(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_GetSegment, null).ConfigureAwait(false);

            var lookUpList = new List<LookupModel>();

            foreach (var item in result)
            {
                lookUpList.Add(new LookupModel
                {
                    Label = item.SegmentName,
                    Value = item.SegmentId
                });
            }
            return lookUpList.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllSegmentAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }
    public async Task<List<CustomLookModel>> GetSegmentWithSegmentTypeNameAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<SegmentListModel>(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_GetSegmentWithType, null).ConfigureAwait(false);

            var lookUpList = new List<CustomLookModel>();

            foreach (var item in result)
            {
                var surveyTemplate = new CustomLookModel
                {
                    Label = item.SegmentName,
                    Value = item.SegmentId,
                    HasTableau = item.HasTableau
                };
                lookUpList.Add(surveyTemplate);
            }
            return lookUpList.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllSegmentAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CustomLookModel>().ToList();
    }
    public async Task<List<LookupModel>> GetAllSurveyTemplateAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<SaveSurveyTemplateModel>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetAllSurveyTemplate, null).ConfigureAwait(false);

            var surveyTemplates = new List<LookupModel>();

            foreach (var item in result)
            {
                surveyTemplates.Add(new LookupModel
                {
                    Label = item.SurveyTemplateName,
                    Value = item.SurveyTemplateId
                });
            }
            return surveyTemplates.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllSurveyTemplateAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<int> GetMasterReferenceId(string masterReferenceName, bool isParent)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetMasterReferenceId - [masterReferenceName: {masterReferenceName}, isParent: {isParent}]");

            var masterReferenceId = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (DatabaseFactories.MLabDB, StoredProcedures.USP_GetMasterReferenceId, new
                            {
                                FilterValue = masterReferenceName,
                                IsParent = isParent
                            }).ConfigureAwait(false);
            return masterReferenceId;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetMasterReferenceId : [Exception] - {ex.Message}");
        }
        return 0;
    }

    public async Task<List<MasterReferenceModel>> GetMasterReferenceList(int masterReferenceId, bool masterReferenceIsParent)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetMasterReferenceList - [masterReferenceId: {masterReferenceId}, masterReferenceIsParent: {masterReferenceIsParent}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<MasterReferenceModel>
                            (
                                DatabaseFactories.MLabDB,
                                StoredProcedures.USP_GetMasterReferenceList,
                                new
                                {
                                    MasterReferenceId = masterReferenceId,
                                    MasterReferenceIsParent = masterReferenceIsParent
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetMasterReferenceList : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<MasterReferenceModel>().ToList();
    }

    public async Task<List<SegmentListModel>> GetSegmentationByIdAsync(int segmentId)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetSegmentationByIdAsync - [segmentId: {segmentId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<SegmentListModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetSegmentationById,
                                new
                                {
                                    SegmentationId = segmentId,

                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetSegmentationByIdAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<SegmentListModel>().ToList();
    }

    public async Task<List<LookupModel>> GetAllCampaignAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LookupModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetAllCampaigns,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllCampaignAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<LookupModel>> GetCampaignByFilterAsync(CampaignLookupByFilterRequestModel filter)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LookupModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignLookupByFilter,
                                new
                                {
                                    CampaignId = filter.CampaignId,
                                    CampaignName = !String.IsNullOrEmpty(filter.CampaignName) ? filter.CampaignName : null,
                                    CampaignStatusId = filter.CampaignStatusId ?? null, 
                                    CampaignTypeId = filter.CampaignTypeId ?? null
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllCampaignAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<CampaignGoalSettingListModel>> GetCampaignGoalSettingListAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignGoalSettingListModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignGoalSettingName,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetCampaignGoalSettingListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CampaignGoalSettingListModel>().ToList();
    }

    public async Task<CampaignGoalSettingListModel> GetCampaignGoalSettingByIdAsync(int campaignGoalId)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCampaignGoalSettingByIdAsync - [campaignGoalId: {campaignGoalId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<CampaignGoalSettingListModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignGoalSettingById,
                                new
                                {
                                    CampaignSettingId = campaignGoalId
                                }
                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetCampaignGoalSettingByIdAsync : [Exception] - {ex.Message}");
        }
        return new CampaignGoalSettingListModel();
    }

    public async Task<List<LookupModel>> GetAllLeaderValidation()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LeaderValidationModel>
                            (
                                DatabaseFactories.MLabDB,
                                StoredProcedures.USP_GetLeaderValidation, null

                            ).ConfigureAwait(false);

            var leaderValidation = new List<LookupModel>();

            foreach (var item in result)
            {
                var lookUp = new LookupModel
                {
                    Label = item.LeaderValidationName,
                    Value = item.LeaderValidationId
                };
                leaderValidation.Add(lookUp);
            }
            return leaderValidation.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllLeaderValidation : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<Tuple<List<CampaignSettingListModel>, List<CampaignSettingListModel>, List<CampaignSettingListModel>>> GetCampaignLookUpAsync()
    {
        try
        {
            var result1 = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignSettingListModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetGoalParameterValueSetting, null

                            ).ConfigureAwait(false);

            var result2 = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignSettingListModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetGoalParameterPointSetting, null

                            ).ConfigureAwait(false);
            var result3 = await _mainDbFactory
                 .ExecuteQueryAsync<CampaignSettingListModel>
                     (
                         DatabaseFactories.PlayerManagementDB,
                         StoredProcedures.USP_GetAutoTaggingSetting, null

                     ).ConfigureAwait(false);

            return Tuple.Create(result1.ToList(), result2.ToList(), result3.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetCampaignLookUpAsync : [Exception] - {ex.Message}");
        }
        return Tuple.Create(Enumerable.Empty<CampaignSettingListModel>().ToList(),
                            Enumerable.Empty<CampaignSettingListModel>().ToList(),
                            Enumerable.Empty<CampaignSettingListModel>().ToList());
    }

    public async Task<List<CampaignAutoTaggingListModel>> GetAutoTaggingDetailsByIdAsync(int campaignSettingId)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetAutoTaggingDetailsByIdAsync - [campaignSettingId: {campaignSettingId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<CampaignGoalSettingListModel, CampaignAutoTaggingListModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetAutoTaggingDetailsById,
                                new
                                {
                                    CampaignSettingId = campaignSettingId,
                                    CampaignSettingTypeId = MasterReferenceName.AutoTagging
                                }
                            ).ConfigureAwait(false);

            var campaignAutoTagging = result.Item2;

            return campaignAutoTagging.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAutoTaggingDetailsByIdAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CampaignAutoTaggingListModel>().ToList();
    }

    public async Task<List<LookupModel>> GetAllCampaignBySearchFilterAsync(int searchFilterType, string searchFilterField, int campaignType)
    {
        try
        {

            DataTable searchActivityLogType = null;
            searchActivityLogType = DataTableConverter.ToDataTable(new List<SearchActivityTableType>().ToList());
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignModel>
                            (
                                DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_SearchActivity,
                                new
                                {
                                    SearchActivityId = 2,
                                    UserId = 0,
                                    SearchActivityLogType = searchActivityLogType,
                                    CampaignTypeId = campaignType,
                                    SearchActivityTypeId = searchFilterType,
                                    ActionType = 0
                                }
                            ).ConfigureAwait(false);

            var campaignNames = new List<LookupModel>();

            foreach (var item in result)
            {
                campaignNames.Add(new LookupModel
                {
                    Label = item.CampaignName,
                    Value = item.CampaignId
                });
            }
            return campaignNames.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllCampaignBySearchFilterAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<CampaignUploadPlayerList>> GetExportCampaignUploadPlayerListAsync(UploadPlayerFilterModel request)
    {
        try
        {
            _logger.LogInfo($"CampaignFactory | GetRemLoGetExportCampaignUploadPlayerListAsyncokupsAsync | Start Time : {DateTime.Now}");
            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<int, CampaignUploadPlayerList>(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_GetRetentionCampaignPlayerList, new
            {
                CampaignId = request.CampaignId,
                Guid = request.Guid,
                Username = request.Username,
                PlayerId = !string.IsNullOrEmpty(request.PlayerId) ? request.PlayerId : null,
                StatusIds = !String.IsNullOrEmpty(request.Status)  ? request.Status : null,
                BrandIds = !String.IsNullOrEmpty(request.Brand)  ? request.Brand : null,
                LastDepositDateFrom = !String.IsNullOrEmpty(request.LastDepositDateFrom) ? (request.LastDepositDateFrom.ToString()) : null,
                LastDepositDateTo = !String.IsNullOrEmpty(request.LastDepositDateTo) ? (request.LastDepositDateTo.ToString()) : null,
                LastDepositAmountFrom = request.LastDepositAmountFrom != 0 ? request.LastDepositAmountFrom : null,
                LastDepositAmountTo = request.LastDepositAmountTo != 0 ? request.LastDepositAmountTo : null,
                BonusAbuser = request.BonusAbuser ?? null, 
                PageSize = request.PageSize,
                LastBetProduct = "",
                LastBetDate = "",
                OffsetValue = request.OffsetValue,
                SortColumn = request.SortColumn,
                SortOrder = request.SortOrder,

            }).ConfigureAwait(false);

            _logger.LogInfo($"CampaignFactory | GetRemLoGetExportCampaignUploadPlayerListAsyncokupsAsync | End Time : {DateTime.Now}");
            return (result.Item2.ToList());

        }

        catch (Exception ex)
        {
            _logger.LogError($"CampaignFactory | GetRemLoGetExportCampaignUploadPlayerListAsyncokupsAsync - ExceptionMessage:{ex.Message} Stack Trace: {ex.StackTrace }");
        }

        return (Enumerable.Empty<CampaignUploadPlayerList>().ToList());
    }

    public async Task<List<LookupModel>> GetAllCampaignCustomEventSettingNameAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignCustomEventSettingResponseModel> (DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignCustomEventSettingName, null
                            ).ConfigureAwait(false);

            var customEvents = new List<LookupModel>();

            foreach (var item in result)
            {
                var lookUp = new LookupModel
                {
                    Label = item.CustomEventName,
                    Value = item.CampaignEventSettingId
                };
                customEvents.Add(lookUp);
            }
            return customEvents.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllCampaignCustomEventSettingNameAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<bool> ValidateHasPlayerInCampaignAsync(long campaignId, string campaignGuid)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<int, CampaignUploadPlayerList>(DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetRetentionCampaignPlayerList, new
                                {
                                    CampaignId = campaignId,
                                    Guid = campaignGuid,
                                    Username = (string)null,
                                    PlayerId = (string)null,
                                    StatusIds = (string)null,
                                    BrandIds = (string)null,
                                    LastDepositDateFrom = (string)null,
                                    LastDepositDateTo = (string)null,
                                    LastDepositAmountFrom = (string)null,
                                    LastDepositAmountTo = (string)null,
                                    BonusAbuser = (string)null,
                                    PageSize = 10,
                                    LastBetProduct = "",
                                    LastBetDate = "",
                                    OffsetValue = 0,
                                    SortColumn = "RegistrationDate",
                                    SortOrder = "desc",

                                }).ConfigureAwait(false);

            if (result.Item2.Any())
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllCampaignCustomEventSettingNameAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<List<CampaignConfigurationSegmentModel>> GetCampaignConfigurationSegmentByIdAsync(long segmentId, long? varianceId)
    {
        try
        {
            _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCampaignConfigurationSegmentByIdAsync - [segmentId: {segmentId}] [varianceId: {varianceId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CampaignConfigurationSegmentModel>
                            (   DatabaseFactories.PlayerManagementDB,
                                StoredProcedures.USP_GetCampaignConfigurationSegmentById,
                                new
                                {
                                    SegmentId = segmentId,
                                    VarianceId = varianceId

                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CampaignManagementFactory} | GetSegmentationByIdAsync : [Exception] - {ex.Message}");
        }
        return new List<CampaignConfigurationSegmentModel> ();
    }
}

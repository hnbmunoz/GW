using LZStringCSharp;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.CallListValidation;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Response;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Udt;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Request;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Response;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Request;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Response;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Samespace.Request;
using MLAB.PlayerEngagement.Core.Models.Samespace.Response;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Data;
using System.Numerics;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories
{
    public class CaseManagementFactory : ICaseManagementFactory
    {

        private readonly ILogger<CaseManagementFactory> _logger;
        private readonly IMainDbFactory _mainDbFactory;
        private readonly ISecondaryServerConnectionFactory _secondaryServerConnectionFactory;

        #region Constructor
        public CaseManagementFactory(ILogger<CaseManagementFactory> logger,IMainDbFactory mainDbFactory, ISecondaryServerConnectionFactory secondaryServerConnectionFactory)
        {
            _mainDbFactory = mainDbFactory;
            _logger = logger;
            _secondaryServerConnectionFactory = secondaryServerConnectionFactory;   
        }
        #endregion

        public async Task<CustomerCaseModel> GetCustomerCaseByIdAsync(int customerCaseId, long userId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCustomerCaseByIdAsync - {JsonConvert.SerializeObject(new { customerCaseId = customerCaseId, userId = userId})}");

                var result = await _mainDbFactory
                            .ExecuteQueryMultipleAsync<CustomerCaseModel, CampaignOptionModel>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCustomerCaseInfo, new
                                    {
                                        CaseInformatIonId = customerCaseId,
                                        UserId = userId
                                    }

                                ).ConfigureAwait(false);

                var campaignList = new List<CampaignOptionModel>();

                foreach (var campaign in result.Item2)
                {
                    campaignList.Add(campaign);
                }

                var returnItem = result.Item1.FirstOrDefault();
                if(returnItem != null)
                {
                    returnItem.CampaignList = campaignList;
                } 

                return returnItem;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCustomerCaseByIdAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<CustomerCaseModel>().FirstOrDefault();
        }

        public async Task<CustomerCaseChatStatisticsModel> GetChatStatisticsByCommunicationIdAsync(long communicationId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetChatStatisticsByCommunicationIdAsync - {JsonConvert.SerializeObject(new { communicationId = communicationId })}");

                var result = await _mainDbFactory
                                    .ExecuteQueryMultipleParamAsync(
                                        DatabaseFactories.IntegrationDb,
                                        StoredProcedures.USP_GetChatStatisticsByCommunicationId,
                                        new { CommunicationId = communicationId }
                                    ).ConfigureAwait(false);

                return result; 
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetChatStatisticsByCommunicationIdAsync : [Exception] - {ex.Message}");
                return Enumerable.Empty<CustomerCaseChatStatisticsModel>().FirstOrDefault();
            }

        }


        public async Task<Tuple<List<int>, List<CustomerCaseCommunicationModel>>> GetCustomerCaseCommListAsync(CustomerCaseCommunicationListRequest request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCustomerCaseCommListAsync - {JsonConvert.SerializeObject(request)}");

                var result = await _mainDbFactory
                            .ExecuteQueryMultipleAsync<int, CustomerCaseCommunicationModel>
                                (
                                    DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCustomerCaseCommList, new
                                    {
                                        CaseInformatIonId = request.CaseInformationId,
                                        PageSize = request.PageSize,
                                        OffsetValue = request.OffsetValue,
                                        SortColumn = string.IsNullOrEmpty(request.SortColumn) ? "CaseCommunicationId" : request.SortColumn,
                                        SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "ASC" : request.SortOrder,
                                    }

                                ).ConfigureAwait(false);
                return Tuple.Create(result.Item1.ToList(), result.Item2.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCustomerCaseCommListAsync : [Exception] - {ex.Message}");
            }
            return Tuple.Create(Enumerable.Empty<int>().ToList(),
                                Enumerable.Empty<CustomerCaseCommunicationModel>().ToList());
        }

        public async Task<CaseCommunicationByIdResponseModel> GetCaseCommunicationByIdAsync(int communicationId, long userId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCaseCommunicationByIdAsync - {JsonConvert.SerializeObject(communicationId)}");
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCaseCommunicationByIdAsync | Start Time : {DateTime.Now}");
                var result = await _mainDbFactory
                            .ExecuteQueryMultipleAsync<dynamic>
                                (
                                    DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCustomerCaseCommInfo, new
                                    {
                                        CaseCommunicationId = communicationId,
                                        UserId = userId
                                    } , 4

                                ).ConfigureAwait(false);

                var resultList = result.ToList();
                List<CustomerCaseCommunicationInfoModel> customerCaseCommunicationInfo = new List<CustomerCaseCommunicationInfoModel>();  
                List<PCSDataModel> pcsData = new List<PCSDataModel>();
                List<CustomerCaseCommunicationFeedbackResponseModel> customerCaseCommunicationFeedbacks =   new List<CustomerCaseCommunicationFeedbackResponseModel>();
                List<CustomerCaseCommunicationSurveyResponseModel> customerCaseCommunicationSurveys = new List<CustomerCaseCommunicationSurveyResponseModel>();


                for (int i = 0; i < resultList.Count; i++)
                {
                    var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));
                    // Define actions or delegates for each case
                    List<Action> actions = new List<Action>
                    {
                        () =>
                        {
                            var customerCaseCommunicationInfoModels = DynamicConverter.ConvertToModels<CustomerCaseCommunicationInfoModel>(dictionaries);
                            customerCaseCommunicationInfo.AddRange(customerCaseCommunicationInfoModels);
                        },
                        () =>
                        {
                            var pcsDataModels = DynamicConverter.ConvertToModels<PCSDataModel>(dictionaries);
                            pcsData.AddRange(pcsDataModels);
                        },
                        () =>
                        {
                            var customerCaseCommunicationFeedbacksModels = DynamicConverter.ConvertToModels<CustomerCaseCommunicationFeedbackResponseModel>(dictionaries);
                            customerCaseCommunicationFeedbacks.AddRange(customerCaseCommunicationFeedbacksModels);
                        },
                        () =>
                        {
                            var customerCaseCommunicationSurveysModels = DynamicConverter.ConvertToModels<CustomerCaseCommunicationSurveyResponseModel>(dictionaries);
                            customerCaseCommunicationSurveys.AddRange(customerCaseCommunicationSurveysModels);
                        },
                        // Add more actions as needed for other resultList indices
                    };

                    // Execute the action based on the index
                    actions[i].Invoke();
                }
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCaseCommunicationByIdAsync | End Time : {DateTime.Now}");
                return new CaseCommunicationByIdResponseModel()
                {
                    CustomerCaseCommunicationInfo = customerCaseCommunicationInfo.FirstOrDefault(),
                    PcsData = pcsData,
                    CustomerCaseCommunicationFeedbacks = customerCaseCommunicationFeedbacks,
                    CustomerCaseCommunicationSurveys = customerCaseCommunicationSurveys
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCaseCommunicationByIdAsync : [Exception] - {ex.Message}");
            }
            return new CaseCommunicationByIdResponseModel();
        }

        public async Task<List<CommunicationOwnerResponseList>> GetAllCommunicationOwnersAsync()
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetAllCommunicationOwnersAsync ");

                var result = await _mainDbFactory 
                            .ExecuteQueryAsync<CommunicationOwnerResponseList>
                                (
                                    DatabaseFactories.PlayerManagementDB,   
                                    StoredProcedures.USP_GetAllCommunicationOwner, null

                                ).ConfigureAwait(false);
               
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetAllCommunicationOwnersAsync : [Exception] - {ex.Message}");
            }
            return  Enumerable.Empty<CommunicationOwnerResponseList>().ToList();
        }

        public async Task<List<CaseCommunicationFilterResponse>> GetCaseCommunicationListCsvAsync(CaseCommunicationFilterRequest request)
        {
            try
            {
                var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("SearchCaseCommunicationPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

                _logger.LogInfo($"CaseManagementFactory | GetCaseCommunicationListCsvAsync - Request [ {request} ]");
                _logger.LogInfo($"CaseManagementFactory | GetCaseCommunicationListCsvAsync | Start Time : {DateTime.Now}");
                _logger.LogInfo($"CaseManagementFactory | GetCaseCommunicationListCsvAsync - ConnectionString uses server -  {connectionString}");

                var result = await _mainDbFactory.ExecuteQueryMultipleAsync<CaseCommunicationFilterResponse, int>(connectionString,StoredProcedures.USP_GetCaseAndCommunicationFilter, new
                {
                    CaseTypeIds = GetNullable(request.CaseTypeIds),
                    BrandId = GetNullable(request.BrandId),
                    VIPLevelIds = GetNullableString(request.VipLevelIds),
                    CaseStatusIds = GetNullableString(request.CaseStatusIds),
                    MessageTypeIds = GetNullableString(request.MessageTypeIds),
                    PlayerIds = GetNullableString(request.PlayerIds),
                    UserNames = GetNullableString(request.Usernames),
                    CaseIds = GetNullableString(request.CaseId),
                    CommunicationIds = GetNullableString(request.CommunicationId),
                    CommunicationOwners = GetNullableString(request.CommunicationOwners),
                    ExternalId = GetNullableString(request.ExternalId),
                    DateByFrom = GetNullableString(request.DateByFrom),
                    DateByTo = GetNullableString(request.DateByTo),
                    TopicLanguageIds = GetNullableString(request.TopicLanguageIds),
                    SubtopicLanguageIds = GetNullableString(request.SubtopicLanguageIds),
                    Duration = GetNullable(request.Duration),
                    UserId = request.UserId,
                    CampaignId = GetNullable(request.CampaignId),
                    CommunicationOwnerTeamId = GetNullable(request.CommunicationOwnerTeamId),
                    CurrencyIds = GetNullableString(request.CurrencyIds),
                    Subject = GetNullableString(request.Subject),
                    Notes = GetNullableString(request.Notes),
                    PageSize = "",
                    DateBy = GetNullable(request.DateBy),
                    IsLastSkillAbandonedQueue = GetNullableString(request.IsLastSkillAbandonedQueue),
                    IsLastAgentAbandonedAssigned = GetNullableString(request.IsLastAgentAbandonedAssigned),
                }).ConfigureAwait(false);

                _logger.LogInfo($"CaseManagementFactory | GetCaseCommunicationListCsvAsync | End Time : {DateTime.Now}");
                return result.Item1.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementFactory | GetCaseCommunicationListAsync - {ex.Message}");
            }

            return new List<CaseCommunicationFilterResponse>();
        }
        private static long? GetNullable(long value)
        {
            return value > 0 ? value : (long?)null;
        }

        private static string GetNullableString(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : null;
        }

        public async Task<PlayerInfoCaseCommunicationResponse> ValidatePlayerCaseCommunicationAsync(long mlabPlayerId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | ValidatePlayerCaseCommunicationAsync ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<PlayerInfoCaseCommunicationResponse>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_ValidatePlayerCaseCommunication, new
                                    {
                                        mlabPlayerId
                                    }

                                ).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | ValidatePlayerCaseCommunicationAsync : [Exception] - {ex.Message}");
            }
            return null;
        }
        public async Task<CaseInformationResponse> GetCustomerServiceCaseInformationByIdAsync(long caseInformationId, long userId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCustomerServiceCaseInformationByIdAsync {JsonConvert.SerializeObject(new { customerCaseId = caseInformationId, userId = userId })}");

                var result = await _mainDbFactory
                            .ExecuteQueryMultipleAsync<CaseInformationResponse, CampaignOptionModel>
                                (
                                    DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCustomerServiceCaseInformationById, new
                                    {
                                        CaseInformationId = caseInformationId,
                                        UserId = userId
                                    }

                                ).ConfigureAwait(false);

                var campaignList = new List<CampaignOptionModel>();

                foreach (var campaign in result.Item2)
                {
                    campaignList.Add(campaign);
                }

                var returnItem = result.Item1.FirstOrDefault();
                if (returnItem != null)
                {
                    returnItem.CampaignList = campaignList;
                }

                return returnItem;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCustomerServiceCaseInformationByIdAsync : [Exception] - {ex.Message}");
            }
            return null;
        }

        public async Task<List<PlayerCaseCommunicationResponse>> GetPlayersByUsernameAsync(string username, int brandId, long userId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPlayersByUsernameAsync ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<PlayerCaseCommunicationResponse>
                                (
                                    DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetPlayersByUsername, new
                                    {
                                        Username = username,
                                        BrandId = brandId,
                                        UserId = userId
                                    }

                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCustomerServiceCaseInformationByIdAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<PlayerCaseCommunicationResponse>().ToList();
        }

        public async Task<List<PlayerCaseCommunicationResponse>> GetPlayersByPlayerIdAsync(string playerId, int brandId, long userId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPlayersByPlayerIdAsync ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<PlayerCaseCommunicationResponse>
                                (
                                    DatabaseFactories.PlayerManagementDB,   
                                    StoredProcedures.USP_GetPlayersByPlayerId, new
                                    {
                                        PlayerId = playerId,
                                        BrandId = brandId,
                                        UserId = userId
                                    }

                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetPlayersByPlayerIdAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<PlayerCaseCommunicationResponse>().ToList();
        }

        public async Task<Tuple<List<ExportPcsResponseModel>, List<ExportQuestionAnswerUdtModel>>> GetPCSQuestionaireListCsvAsync(PCSQuestionaireListByFilterRequestModel request)
        {
            try
            {
                var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("PCSQuestionnairesPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPCSQuestionaireListCsvAsync - {JsonConvert.SerializeObject(request)}");
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPCSQuestionaireListCsvAsync | Start Time : {DateTime.Now}");
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPCSQuestionaireListCsvAsync | DBConnectionString - {connectionString}");

                var questionAnswerDT = DataTableConverter.ToDataTable(request.PCSCommunicationQuestionAnswerType);

                var result = await _mainDbFactory
                            .ExecuteQueryMultipleAsync<ExportPcsResponseModel, ExportQuestionAnswerUdtModel>
                                (
                                    connectionString,
                                    StoredProcedures.USP_GetPCSQuestionAndAnswerListCsv, new
                                    {
                                        BrandId = string.IsNullOrEmpty(request.BrandId) ? (int?)null : Int32.Parse(request.BrandId),
                                        SkillId = string.IsNullOrEmpty(request.SkillId) ? (int?)null : Int32.Parse(request.SkillId),
                                        SummaryAction = string.IsNullOrEmpty(request.SummaryAction) ? (int?)null : Int32.Parse(request.SummaryAction),
                                        StartDate = String.IsNullOrWhiteSpace(request.StartDate) ? null : request.StartDate,
                                        EndDate = String.IsNullOrWhiteSpace(request.EndDate) ? null : request.EndDate,
                                        License = String.IsNullOrWhiteSpace(request.License) ? null : request.License,
                                        PCSCommunicationQuestionAnswerType = questionAnswerDT
                                    }

                                ).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPCSQuestionaireListCsvAsync | End Time : {DateTime.Now}");

                return Tuple.Create(result.Item1.ToList(), result.Item2.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetPCSQuestionaireListCsvAsync : [Exception] - {ex.Message}");
            }
            return Tuple.Create(Enumerable.Empty<ExportPcsResponseModel>().ToList(), Enumerable.Empty<ExportQuestionAnswerUdtModel>().ToList());
        }
        public static DataTable ToDataTable(IEnumerable<dynamic> items)
        {
            if (items == null) return null;
            var data = items.ToArray();
            if (data.Length == 0) return null;

            var dt = new DataTable();
            foreach (var pair in ((IDictionary<string, object>)data[0]))
            {
                dt.Columns.Add(pair.Key, (pair.Value ?? string.Empty).GetType());
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }
        public async Task<List<PCSCommunicationQuestionsByIdResponseModel>> GetPCSCommunicationQuestionsByIdAsync(long caseCommunicationId)
        {
            _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPCSCommunicationQuestionsByIdAsync - {JsonConvert.SerializeObject(caseCommunicationId)}");

            try
            {
                var result = await _mainDbFactory
            .ExecuteQueryAsync<PCSCommunicationQuestionsByIdResponseModel>
                (
                    DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetPCSCommunicationQuestionsById, new
                    {
                        CaseCommunicationId = caseCommunicationId,
                    }

                ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetPCSCommunicationQuestionsByIdAsync : [Exception] - {ex.Message}");
            }

            return Enumerable.Empty<PCSCommunicationQuestionsByIdResponseModel>().ToList();
        }


        public async Task<List<PCSQuestionaireListByFilterResponseModel>> GetCaseManagementPCSQuestionsByFilterAsync(CaseManagementPCSQuestionsByFilterRequestModel request)
        {
            try
            {
                var connectionString = await _secondaryServerConnectionFactory.GetDatabaseToUseAsync("PCSQuestionnairesPrimaryDbOn", DatabaseFactories.PlayerManagementDB);

                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPCSCommunicationQuestionsByIdAsync - {JsonConvert.SerializeObject(request)}");
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPCSCommunicationQuestionsByIdAsync | DBConnectionString - {connectionString}");

                var result = await _mainDbFactory.ExecuteQueryAsync<PCSQuestionaireListByFilterResponseModel>(
                    connectionString,
                    StoredProcedures.USP_GetCaseManagementPCSQuestionsByFilter, new
                {
                    BrandId = string.IsNullOrEmpty(request.BrandId) ? (int?)null : Int32.Parse(request.BrandId),
                    MessageTypeId = string.IsNullOrEmpty(request.MessageTypeId) ? (int?)null : Int32.Parse(request.MessageTypeId),
                    License = String.IsNullOrWhiteSpace(request.License) ? null : request.License,
                    SkillId = string.IsNullOrEmpty(request.SkillId) ? (int?)null : Int32.Parse(request.SkillId),
                    SummaryAction = request.SummaryAction,
                    StartDate = String.IsNullOrWhiteSpace(request.StartDate) ? null : request.StartDate,
                    EndDate = String.IsNullOrWhiteSpace(request.EndDate) ? null : request.EndDate,
                }).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementFactory | GetCaseManagementPCSQuestionsByFilterAsync - ExceptionMessage:{ex.Message} Stack Trace: {ex.StackTrace}");
                return Enumerable.Empty<PCSQuestionaireListByFilterResponseModel>().ToList();
            }
        }
        public async Task<SurveyTemplateResponse> GetCustomerCaseCommSurveyTemplateByIdAsync(int surveyTemplateId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCustomerCaseCommSurveyTemplateByIdAsync - [surveyTemplateId: {surveyTemplateId}]");
                _logger.LogInfo($"{Factories.CaseManagementFactory}| GetCustomerCaseCommSurveyTemplateByIdAsync | Start Time : {DateTime.Now}");

                var result = await _mainDbFactory.ExecuteQueryMultipleAsync<SurveyTemplateInfoResponse, SurveyQuestionResponse, SurveyQuestionAnswerResponse>(
                        DatabaseFactories.PlayerManagementDB,
                        StoredProcedures.USP_GetCustomerCaseCommSurveyTemplateById, new
                        {
                            SurveyTemplateId = surveyTemplateId
                        }
                    ).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.CaseManagementFactory}| GetCustomerCaseCommSurveyTemplateByIdAsync | End Time : {DateTime.Now}");
                return new SurveyTemplateResponse()
                {
                    SurveyTemplate = result.Item1.FirstOrDefault(),
                    SurveyQuestions = result.Item2.ToList(),
                    SurveyQuestionAnswers = result.Item3.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCustomerCaseCommSurveyTemplateByIdAsync : [Exception] - {ex.Message}");
                return Enumerable.Empty<SurveyTemplateResponse>().FirstOrDefault();
            }
        }

        public async Task<CaseManagementPcsCommunicationByFilterResponseModel> GetCaseManagementPCSCommunicationByFilterAsync(CaseManagementPCSCommunicationByFilterRequestModel request)
        {
            _logger.LogInfo($"{Factories.CaseManagementFactory} | GetCaseManagementPCSCommunicationByFilterAsync - {JsonConvert.SerializeObject(request)}");
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory}| GetCaseManagementPCSCommunicationByFilterAsync | Start Time : {DateTime.UtcNow}");
                var questionAnswerDT = DataTableConverter.ToDataTable(request.PCSCommunicationQuestionAnswerType);

                var result = await _mainDbFactory.ExecuteQueryMultipleAsync<CaseManagementPcsCommunicationResponseModel, int>(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_GetCaseManagementPCSCommunicationByFilter, new
                {
                    BrandId = string.IsNullOrEmpty(request.BrandId) ? (int?)null : Int32.Parse(request.BrandId),
                    MessageTypeId = string.IsNullOrEmpty(request.MessageTypeId) ? (int?)null : Int32.Parse(request.MessageTypeId),
                    License = String.IsNullOrWhiteSpace(request.License) ? null : request.License,
                    SkillId = string.IsNullOrEmpty(request.SkillId) ? (int?)null : Int32.Parse(request.SkillId),
                    SummaryAction = request.SummaryAction,
                    StartDate = String.IsNullOrWhiteSpace(request.StartDate) ? null : request.StartDate,
                    EndDate = String.IsNullOrWhiteSpace(request.EndDate) ? null : request.EndDate,
                    PageSize = request.PageSize,
                    OffsetValue = request.OffsetValue,
                    SortColumn = string.IsNullOrEmpty(request.SortColumn) ? "IsNull(a.CreatedDate,a.UpdatedDate)" : request.SortColumn,
                    SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "desc" : request.SortOrder,
                    PCSCommunicationQuestionAnswerType = questionAnswerDT
                }).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.CaseManagementFactory}| GetCaseManagementPCSCommunicationByFilterAsync | End Time : {DateTime.UtcNow}");
                return new CaseManagementPcsCommunicationByFilterResponseModel
                {
                    RecordCount = result.Item2.FirstOrDefault(),
                    CaseManagementPCSCommunications = result.Item1.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementFactory | GetCaseManagementPCSCommunicationByFilterAsync - ExceptionMessage:{ex.Message} Stack Trace: {ex.StackTrace}");
                return new CaseManagementPcsCommunicationByFilterResponseModel
                {
                    RecordCount = 0,
                    CaseManagementPCSCommunications = Enumerable.Empty<CaseManagementPcsCommunicationResponseModel>().ToList()
                };
            }

        }

        public async Task<List<CustomerCaseCommunicationFeedbackResponseModel>> GetCaseCommunicationFeedbackByIdAsync(int communicationId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPlayersByUsernameAsync ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<CustomerCaseCommunicationFeedbackResponseModel>
                                (
                                    DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCustomerCaseCommFeedback, new
                                    {
                                        CaseCommunicationId = communicationId
                                    }

                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCustomerServiceCaseInformationByIdAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<CustomerCaseCommunicationFeedbackResponseModel>().ToList();
        }

        public async Task<List<CustomerCaseCommunicationSurveyResponseModel>> GetCaseCommunicationSurveyByIdAsync(int communicationId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetPlayersByUsernameAsync ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<CustomerCaseCommunicationSurveyResponseModel>
                                (
                                    DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCustomerCaseCommSurvey, new
                                    {
                                        CaseCommunicationId = communicationId
                                    }

                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCustomerServiceCaseInformationByIdAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<CustomerCaseCommunicationSurveyResponseModel>().ToList();
        }

        public async Task<List<LookupModel>> GetCustomerCaseSurveyTemplateAsync(int caseTypeId)
        {
            try
            {
                var result = await _mainDbFactory
                            .ExecuteQueryAsync<SaveSurveyTemplateModel>
                                (
                                    DatabaseFactories.MLabDB,
                                    StoredProcedures.USP_GetAllSurveyTemplate, new
                                    { 
                                        CaseTypeId = caseTypeId, // For Customer Case
                                        SurveyTemplateStatus = "1"
                                    }

                                ).ConfigureAwait(false);

                var surveyTemplates = new List<LookupModel>();

                foreach (var item in result)
                {
                    var surveyTemplate = new LookupModel
                    {
                        Label = item.SurveyTemplateName,
                        Value = item.SurveyTemplateId
                    };
                    surveyTemplates.Add(surveyTemplate);
                }
                return surveyTemplates.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | GetAllSurveyTemplateAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<LookupModel>().ToList();
        }

        public async Task<FlyFoneRecordedParameterResponseModel> FlyFoneOutboundCallAsync(FlyFoneOutboundCallRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | FlyFoneOutboundCallAsync | parameter - {JsonConvert.SerializeObject(request)} ");
                var insertResult = await _mainDbFactory.ExecuteQueryAsync<FlyFoneRecordedParameterResponseModel>(DatabaseFactories.IntegrationDb, StoredProcedures.USP_InsertRequestFlyFoneRecordDetail, new
               {
                   Outnumber = request.Outnumber,
                   Extension = request.Ext,
                   Department = request.Department,
                   userId = request.UserId,
                   MlabPlayerId = request.MlabPlayerId,
               }).ConfigureAwait(false);

                return insertResult.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | FlyFoneOutboundCallAsync : [Exception] - {ex.Message}");
                return Enumerable.Empty<FlyFoneRecordedParameterResponseModel>().FirstOrDefault();
            }
        }

        public async Task<List<FlyFoneCallDetailRecordResponseModel>> GetFlyFoneCallDetailRecordsAsync()
        {
            try
            {
                var fylfoneDetails = await _mainDbFactory.ExecuteQueryAsync<FlyFoneCallDetailRecordResponseModel>(DatabaseFactories.IntegrationDb, StoredProcedures.USP_GetFlyFoneCallDetailRecords, new { }).ConfigureAwait(false);
                return fylfoneDetails.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | GetFlyFoneCallDetailRecordsAsync : [Exception] - {ex.Message}");
                return Enumerable.Empty<FlyFoneCallDetailRecordResponseModel>().ToList();
            }

        }

        public async Task<bool> FlyFoneEndOutboundCallAsync(FlyFoneSaveCallDetailRecordRequestModel request)
        {
            try
            {
                var flyFoneCallDetailRecordTypeDT = DataTableConverter.ToDataTable(request.FlyFoneCallDetailRecordType);

                var endCallInsertResult = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.IntegrationDb, StoredProcedures.USP_UpdateFlyFoneCallDetailRecordByCallingCode, new
                {
                    UserId = request.UserId,
                    EndTime = request.EndTime,
                    FlyFoneCallDetailRecordType = flyFoneCallDetailRecordTypeDT

                }).ConfigureAwait(false);

                return endCallInsertResult.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | GetFlyFoneCallDetailRecordsAsync : [Exception] - {ex.Message}");
                return false;
            }
        }

        public async Task<bool> FlyFoneFetchDetailRecordsAsync(FlyFoneSaveFetchCallDetailRecordRequestModel request)
        {
            try
            {
                var flyFoneFetchCallDetailRecordTypeDT = DataTableConverter.ToDataTable(request.FlyFoneCallDetailRecordType);

                var saveFetchedCallDetailRecords = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.IntegrationDb, StoredProcedures.USP_UpdateFlyFoneCallDetailRecord, new
                {
                    UserId = request.UserId,
                    FlyFoneCallDetailRecordType = flyFoneFetchCallDetailRecordTypeDT
                }).ConfigureAwait(false);

                return saveFetchedCallDetailRecords.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | FlyFoneFetchDetailRecordsAsync : [Exception] - {ex.Message}");
                return false;
            }
        }

        public async Task<CloudTalkMakeACallResponseModel> CloudTalkMakeACallAsync(CloudTalkMakeACallRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | CloudTalkMakeACallAsync | parameter - {JsonConvert.SerializeObject(request)} ");
                var cloudTalkInsertResponse = await _mainDbFactory.ExecuteQueryAsync<CloudTalkMakeACallResponseModel>(DatabaseFactories.IntegrationDb, StoredProcedures.USP_InsertRequestCloudTalkRecordDetail, new
                {
                    AgentId = request.AgentId,
                    MlabPlayerId = request.MlabPlayerId,
                    userId = request.UserId
                }).ConfigureAwait(false);

                return cloudTalkInsertResponse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | CloudTalkMakeACallAsync : [Exception] - {ex.Message}");
                return Enumerable.Empty<CloudTalkMakeACallResponseModel>().FirstOrDefault();
            }
        }

        public async Task<int> AppendCaseCommunicationContent(CaseCommunicationContentRequestModel request)
        {
            var result = -1;
            try
            {
                _logger.LogInfo($"CaseManagementService | AppendCaseCommunicationContent | Parameters {request}");

                result = await _mainDbFactory.ExecuteQueryAsync(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_AppendCaseCommunicationContent, new
                   {
                       CommunicationContent = request.CommunicationContent,
                       CaseCommunicationId = request.CommunicationId,
                       StartCommunicationDate = request.StartCommunicationDate,
                       EndCommunicationDate = request.EndCommunicationDate
                   }).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                _logger.LogError($"FlyFoneProcessService | AppendCaseCommunicationContent : [Exception] {ex.StackTrace} Message: {ex.Message}");
            }
            return result;
        }

        public async Task<bool> UpdateCloudTalkCdrByCallingCodeAsync(UpdateCloudTalkCdrByCallingCodeRequestModel request)
        {
            try
            {
                _logger.LogInfo($"CaseManagementService | UpdateCloudTalkCdrByCallingCode | Parameters {request}");

               var hasUpdate = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.IntegrationDb, StoredProcedures.USP_UpdateCloudTalkCdrByCallingCode, new
                {
                   CallingCode = request.CallingCode,
                   CallId = request.CallId,
                   Type = request.Type,
                   Billsec = request.Billsec,
                   CloudTalkUserId = request.CloudTalkUserId,
                   PublicInternal = request.PublicInternal,
                   TalkingTime = request.TalkingTime,
                   UserId = request.UserId,
                   startedAt = request.StartedAt,
                   answeredAt = request.AnsweredAt,
                   endedAt = request.EndedAt,
                   waitingTime = request.WaitingTime,
                   recordingLink = request.RecordingLink,
               }).ConfigureAwait(false);

                return hasUpdate.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | UpdateCloudTalkCdrByCallingCode : [Exception] - {ex.Message}");
                return false;
            }
       
        }

        public async Task<AppConfigSettingsModel> GetFlyFoneAppSettingsAsync()
        {
            try
            {

                var result = await _mainDbFactory.ExecuteQueryAsync<AppConfigSettingsModel>(
                         DatabaseFactories.MLabDB,
                         StoredProcedures.USP_GetAppConfigSetting,
                         new
                         {
                             ApplicationId = 312
                         }).ConfigureAwait(false);

                return result.FirstOrDefault();


            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | GetSegmentationByIdAsync : [Exception] - {ex.Message}");
                return Enumerable.Empty<AppConfigSettingsModel>().FirstOrDefault();
            }
        }

        public async Task<UpsertCaseResponse> UpSertCustomerServiceCaseCommunicationAsync(AddCustomerServiceCaseCommunicationRequest request)
        {

            _logger.LogInfo($"{Factories.CaseManagementFactory} | UpSertCustomerServiceCaseCommunicationAsync - {JsonConvert.SerializeObject(request)}");

            // FEEDBACK DETAILS
            DataTable communicationFeedbackDT = DataTableConverter.ToDataTable(new List<AddCommunicationFeedbackRequest>());
            // SURVEY DETAILS DATA TABLE
            DataTable communicationSurveyDT = DataTableConverter.ToDataTable(new List<AddCommunicationSurveyRequest>());
            //COMMUNICATION DETAILS
            var caseCommunication = new List<CaseCommunicationTypeUdtModel>();


            if (request.caseCommunication != null)
            {
                if (request.caseCommunication.CommunicationFeedBackType.Any())
                {
                    communicationFeedbackDT = DataTableConverter.ToDataTable(request.caseCommunication.CommunicationFeedBackType);
                }


                if (request.caseCommunication.CommunicationSurveyQuestion.Any())
                {
                    communicationSurveyDT = DataTableConverter.ToDataTable(request.caseCommunication.CommunicationSurveyQuestion);
                }
                caseCommunication.Add(new CaseCommunicationTypeUdtModel
                {
                    CaseCommunicationId = request.caseCommunication.CaseCommunicationId,
                    CaseInformationId = request.caseCommunication.CaseInformationId,
                    PurposeId = request.caseCommunication.PurposeId,
                    MessageTypeId = request.caseCommunication.MessageTypeId,
                    MessageStatusId = request.caseCommunication.MessageStatusId,
                    MessageReponseId = request.caseCommunication.MessageReponseId,
                    StartCommunicationDate = request.caseCommunication.StartCommunicationDate,
                    EndCommunicationDate = request.caseCommunication.EndCommunicationDate,
                    CommunicationOwner = request.caseCommunication.CommunicationOwner,
                    CommunicationContent = LZString.DecompressFromBase64(request.caseCommunication.CommunicationContent),
                    CreatedBy = request.caseCommunication.CreatedBy,
                    UpdatedBy = request.caseCommunication.UpdatedBy,
                    Duration = request.caseCommunication.Duration,
                    SurveyTemplateId = request.caseCommunication.SurveyTemplateId
                });
            }

            // CASE COMMUNICATION CONVERT TO DATA TABLE
            DataTable caseCommunicationDT = DataTableConverter.ToDataTable(caseCommunication);
            DataTable campaignIdsDT = DataTableConverter.ToDataTable(new List<CampaignIds>());

            if (request.CampaignIds != null && request.CampaignIds.Count > 0)
            {
                campaignIdsDT = DataTableConverter.ToDataTable(request.CampaignIds);
            }

            try
            {
                var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<UpsertCaseResponse>(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_UpsertCustomerServiceCaseCommunication, new
                {
                    request.MlabPlayerId,
                    request.CaseInformationId,
                    request.CaseCreatorId,
                    request.CaseTypeId,
                    request.CaseStatusId,
                    request.TopicLanguageId,
                    request.SubtopicLanguageId,
                    request.UserId,
                    request.Subject,
                    request.BrandId,
                    request.LanguageId,
                    CaseCommunicationType = caseCommunicationDT,
                    CommunicationSurveyQuestionType = communicationSurveyDT,
                    CommunicationFeedBackType = communicationFeedbackDT,
                    CaseInformationCampaignType = campaignIdsDT
                }).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementFactory | UpSertCustomerServiceCaseCommunicationAsync - ExceptionMessage:{ex.Message} Stack Trace: {ex.StackTrace}");
                return new UpsertCaseResponse();
            }


        }

        #region Communication Review 
        public async Task<CommunicationReviewLookupResponseModel> GetCommunicationReviewLookupsAsync()
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCommunicationReviewLookupsAsync | Request: None");
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCommunicationReviewLookupsAsync | Start Time : {DateTime.Now}");
                var result = await _mainDbFactory
                    .ExecuteQueryMultipleAsync<dynamic>
                    (
                     DatabaseFactories.MLabDB,
                     StoredProcedures.USP_GetCommunicationReviewLookups, new { } , 4
                    ).ConfigureAwait(false);


                var resultList = result.ToList();
                List<QualityReviewMeasurementResponseModel> qualityReviewMeasurements = new List<QualityReviewMeasurementResponseModel>();
                List<QualityReviewBenchmarkResponseModel> qualityReviewBenchmarks = new List<QualityReviewBenchmarkResponseModel>();
                List<QualityReviewPeriodResponseModel> qualityReviewPeriods = new List<QualityReviewPeriodResponseModel>();
                List<CommunicationReviewFieldLookupsResponseModel> communicationReviewFieldLookups = new List<CommunicationReviewFieldLookupsResponseModel>();


                for (int i = 0; i < resultList.Count; i++)
                {
                    var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));

                    List<Action> actions = new List<Action>
                    {
                        () =>
                        {
                            var qualityReviewMeasurementsModels = DynamicConverter.ConvertToModels<QualityReviewMeasurementResponseModel>(dictionaries);
                            qualityReviewMeasurements.AddRange(qualityReviewMeasurementsModels);
                        },
                        () =>
                        {
                            var qualityReviewBenchmarksModels = DynamicConverter.ConvertToModels<QualityReviewBenchmarkResponseModel>(dictionaries);
                            qualityReviewBenchmarks.AddRange(qualityReviewBenchmarksModels);
                        },
                        () =>
                        {
                            var qualityReviewPeriodsModels = DynamicConverter.ConvertToModels<QualityReviewPeriodResponseModel>(dictionaries);
                            qualityReviewPeriods.AddRange(qualityReviewPeriodsModels);
                        },
                        () =>
                        {
                            var communicationReviewFieldLookupsModels = DynamicConverter.ConvertToModels<CommunicationReviewFieldLookupsResponseModel>(dictionaries);
                            communicationReviewFieldLookups.AddRange(communicationReviewFieldLookupsModels);
                        },
                        // Add more actions as needed for other resultList indices
                    };

                    // Execute the action based on the index
                    actions[i].Invoke();
                }
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCommunicationReviewLookupsAsync | End Time : {DateTime.Now}");
                return new CommunicationReviewLookupResponseModel()
                {
                    QualityReviewMeasurements = qualityReviewMeasurements, 
                    QualityReviewBenchmarks = qualityReviewBenchmarks,
                    QualityReviewPeriods = qualityReviewPeriods,
                    CommunicationReviewFieldLookups = communicationReviewFieldLookups
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | GetCommunicationReviewLookupsAsync : [Exception] - {ex.Message}");
                return new CommunicationReviewLookupResponseModel();
            }
        }

        public async Task<bool> ValidateCommunicationReviewLimitAsync(CommunicationReviewLimitRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | ValidateCommunicationReviewLimitAsync | Request: {request}");
              
                var result = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_ValidateCommunicationReviewLimit, new
                {
                    CaseCommunicationId = request.CaseCommunicationId,
                    QualityReviewPeriodId = request.QualityReviewPeriodId,
                    RevieweeId = request.RevieweeId,
                    ReviewerId = request.ReviewerId,
                    UserId = request.UserId
                }).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | ValidateCommunicationReviewLimitAsync : [Exception] - {ex.Message}");
                return false;
            }
        }

        public async Task<bool> InsertCommunicationReviewEventLogAsync(CommunicationReviewEventLogRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | InsertCommunicationReviewEventLogAsync | Request: {request}");

                var result = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_InsertCommunicationReviewEventLog, new
                {
                    CaseCommunicationId = request.CaseCommunicationId,
                    EventTypeId = request.EventTypeId,
                    UserId = request.UserId
                }).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | InsertCommunicationReviewEventLogAsync : [Exception] - {ex.Message}");
                return false;
            }
        }

        public async Task<List<CommunicationReviewCriteriaResponseModel>> GetCriteriaListByMeasurementId(int? measurementId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | GetCriteriaListByMeasurementId | MeasurementId: {measurementId}");

                var result = await _mainDbFactory.ExecuteQueryAsync<CommunicationReviewCriteriaResponseModel>(DatabaseFactories.MLabDB, StoredProcedures.USP_GetQualityReviewCriteriaByQualityReviewMeasurementId, new 
                {
                    QualityReviewMeasurementId = measurementId
                }).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | GetCriteriaListByMeasurementId : [Exception] - {ex.Message}");

            }

            return Enumerable.Empty<CommunicationReviewCriteriaResponseModel>().ToList();
        }

        public async Task<List<CommunicationReviewEventLogRequestModel>> GetCommunicationReviewEventLogAsync(int caseCommunicationId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | USP_GetCommunicationReviewEventLogAsync | CommunicationId: {caseCommunicationId}");

               var result = await _mainDbFactory.ExecuteQueryAsync<CommunicationReviewEventLogRequestModel>(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_GetCommunicationReviewEventLogByCaseCommunicationId, new
                {
                    CaseCommunicationId = caseCommunicationId
                }).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.CaseManagementFactory} | USP_GetCommunicationReviewEventLogAsync | Result: {JsonConvert.SerializeObject(result)}");

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCommunicationMarkAsReadCountAndLimitAsync : [Exception] - {ex.Message}");

            }

            return Enumerable.Empty<CommunicationReviewEventLogRequestModel>().ToList();
        }

        public async Task<bool> UpdateCommReviewPrimaryTaggingAsync(UpdateCommReviewTaggingModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | UpdateCommReviewTaggingModel | {JsonConvert.SerializeObject(request)}");

                var result = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_UpdateCommunicationReviewIsPrimary, new
                {
                    CommunicationReviewId = request.CommunicationReviewId,
                    CaseCommunicationId = request.CommunicationId,
                    ReviewerId = request.ReviewerId,
                    RevieweeId = request.RevieweeId,
                    QualityReviewPeriodId = request.ReviewPeriodId

                }).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | InsertCommunicationReviewEventLogAsync : [Exception] - {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveCommReviewPrimaryTaggingAsync(RemoveCommReviewPrimaryTaggingModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | RemoveCommReviewPrimaryTaggingAsync | param: {JsonConvert.SerializeObject(request)}");

                var result = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_RemoveCommunicationReviewIsPrimary, new
                {
                    CaseCommunicationId = request.CommunicationId,
                    ReviewerId = request.ReviewerId,
                    RevieweeId = request.RevieweeId,
                    QualityReviewPeriodId = request.ReviewPeriodId

                }).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | RemoveCommReviewPrimaryTaggingAsync : [Exception] - {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Samespace VoIP Integration
        public async Task<SamespaceMakeACallResponseModel> SamespaceMakeACallAsync(SamespaceMakeACallRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | SamespaceMakeACallAsync | parameter={JsonConvert.SerializeObject(request)}");

                var samespaceInsertResponse = await _mainDbFactory.ExecuteQueryAsync<SamespaceMakeACallResponseModel>(DatabaseFactories.IntegrationDb,StoredProcedures.USP_InsertRequestSamespaceRecordDetail, new
                {
                    request.AgentId,
                    request.MlabPlayerId,
                    request.UserId,
                    request.SubscriptionId
                }).ConfigureAwait(false);

                return samespaceInsertResponse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | SamespaceMakeACallAsync : Exception={ex.Message} parameter={JsonConvert.SerializeObject(request)}");
                return Enumerable.Empty<SamespaceMakeACallResponseModel>().FirstOrDefault();
            }
        }
        public async Task<bool> UpdateSamespaceCdrByCallingCodeAsync(UpdateSamespaceCdrByCallingCodeRequestModel request, int userId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CampaignManagementFactory} | UpdateSamespaceCdrByCallingCodeAsync | UserId={userId} SamespaceCDRType={JsonConvert.SerializeObject(request)}");

                var SamespaceCdrType = DataTableConverter.ToDataTable(new List<UpdateSamespaceCdrByCallingCodeRequestModel>{request});

                var hasUpdate = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.IntegrationDb,StoredProcedures.USP_UpdateSamespaceCdrByCallingCode, new
                {
                    SamespaceCallDetailRecordType = SamespaceCdrType,
                    UserId = userId

                }).ConfigureAwait(false);

                return hasUpdate.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | UpdateSamespaceCdrByCallingCodeAsync | Exception={ex.Message} UserId={userId} SamespaceCDRType={JsonConvert.SerializeObject(request)}");
                return false;
            }

        }
        #endregion

        #region Case and Communication
        public async Task<List<CaseCommunicationAnnotationModel>> GetCaseCommunicationAnnotationByCaseCommunicationIdAsync(long caseCommunicationId)
        {
            try
            {
                _logger.LogInfo($"CaseManagementService | GetCaseCommunicationAnnotationByCaseCommunicationIdAsync | Parameter caseCommunicationId: {caseCommunicationId}");

                var result = await _mainDbFactory.ExecuteQueryAsync<CaseCommunicationAnnotationModel>(
                         DatabaseFactories.PlayerManagementDB,
                         StoredProcedures.USP_GetCaseCommunicationAnnotationByCaseCommunicationId,
                         new
                         {
                             CaseCommunicationId = caseCommunicationId
                         }).ConfigureAwait(false);

                return result.ToList();


            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CampaignManagementFactory} | GetCaseCommunicationAnnotationByCaseCommunicationIdAsync : [Exception] - {ex.Message}");
                return Enumerable.Empty<CaseCommunicationAnnotationModel>().ToList();
            }
        }

        public async Task<bool> UpsertCaseCommunicationAnnotationAsync(CaseCommAnnotationRequestModel request)
        {
            try
            {
                _logger.LogInfo($"CaseManagementService | UpsertCaseCommunicationAnnotationAsync | Parameter request: {request}");

                var caseCommAnnotationTypeUDT = DataTableConverter.ToDataTable(request.CaseCommunicationAnnotationUdt);

                var upsertCaseCommAnnotation = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_UpsertCaseCommunicationAnnotation, new
                {
                    CaseCommunicationAnnotationType = caseCommAnnotationTypeUDT,
                    UserId = request.UserId,
                    CaseCommunicationId = request.CaseCommunicationId,
                }).ConfigureAwait(false);

                return upsertCaseCommAnnotation.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | UpsertCaseCommunicationAnnotationAsync : [Exception] - {ex.Message}");
                return false;
            }
        }

        public async Task<int> ValidateCaseCommunicationAnnotationAsync(long caseCommunicationId, string contentBefore, string contentAfter)
        {
            try
            {
                _logger.LogInfo($"CaseManagementService | ValidateCaseCommunicationAnnotationAsync | Parameters - CaseCommunicationId: {caseCommunicationId} ContentBefore: {contentBefore} ContentAfter: {contentAfter}");


                var validateCaseCommAnnotation = await _mainDbFactory.ExecuteQueryAsync<int>(DatabaseFactories.PlayerManagementDB,StoredProcedures.USP_ValidateCaseCommunicationAnnotation, new
                {
                    CaseCommunicationId = caseCommunicationId,
                    ContentBefore = contentBefore,
                    ContentAfter = contentAfter,
                }).ConfigureAwait(false);

                return validateCaseCommAnnotation.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | ValidateCaseCommunicationAnnotationAsync : [Exception] - {ex.Message}");
                return 0;
            }
        }

        public async  Task<List<LookupModel>> GetCampaignByCaseTypeId(int caseTypeId)
        {
            try
            {
                var result = await _mainDbFactory
                            .ExecuteQueryAsync<LookupModel>
                                (
                                    DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCampaign, new
                                    {
                                        @CaseTypeId = caseTypeId
                                    }

                                ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetCampaignByCaseTypeId : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<LookupModel>().ToList();
        }
        #endregion

        #region Campaign Dropdown
        public async Task<List<CampaignOptionModel>> GetEditCustomerServiceCaseCampainNameByUsername(string username, long brandId)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | GetEditCustomerServiceCaseCampainNameByUsername - [username:  {username}, brandId: {brandId} ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<CampaignOptionModel> (DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_GetCustomerCaseCampaignNamesByUsername, 
                            new { 
                                Username = username,
                                BrandId = brandId,
                            } ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.CaseManagementFactory} | GetAllActiveCampaignByUsername : [Exception] - {ex.Message}");
                return Enumerable.Empty<CampaignOptionModel>().ToList();
            }
        }
        #endregion
    }
}

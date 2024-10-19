using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Response;
using MLAB.PlayerEngagement.Core.Models.TicketManagement;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Response;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using Newtonsoft.Json;
using NLog.Filters;
using System.Data;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories
{
    public class TicketManagementFactory : ITicketManagementFactory
    {
        private readonly ILogger<TicketManagementFactory> _logger;
        private readonly IMainDbFactory _mainDbFactory;

        #region Constructor
        public TicketManagementFactory(ILogger<TicketManagementFactory> logger, IMainDbFactory mainDbFactory)
        {
            _mainDbFactory = mainDbFactory;
            _logger = logger;
        }
        #endregion


        public async Task<MlabTransactionResponseModel> GetMlabTransactionDataAsync(GetMlabRequestModel request)
        {
            try
            {
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<MlabTransactionResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetDepositTransactionDetailsById,
                              new
                              {
                                  DepositTransactionId = request.TransactionId,
                                  MlabPlayerId = request.MlabPlayerId,
                                  ProviderTransactionId = request.ProviderTransactionId
                              }
                          ).ConfigureAwait(false);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetMlabTransactionDataAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<MlabTransactionResponseModel>().FirstOrDefault();
        }

        public async Task<List<TicketStatusHierarchyResponseModel>> GetTicketStatusHierarchyByTicketTypeAsync(TicketStatusHierarchyRequestModel request)
        {
            try
            {

                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTicketStatusHierarchyByTicketTypeAsync | request={JsonConvert.SerializeObject(request)}");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<TicketStatusHierarchyResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetTicketStatusHierarchyByTicketType,
                              new
                              {
                                  TicketTypeId = request.TicketTypeId,
                                  IsForVip = request.IsForVip,
                                  UserId=request.UserId
                              }
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketStatusHierarchyByTicketTypeAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TicketStatusHierarchyResponseModel>().ToList();
        }

        #region Ticket Configuration
        public async Task<List<TicketTypesResponseModel>> GetTicketTypesAsync()
        {
            var result = await _mainDbFactory
                      .ExecuteQueryAsync<TicketTypesResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetActiveTicketTypes,
                              null
                          ).ConfigureAwait(false);
            return result.ToList();
        }
       

        public async Task<List<LookupModel>> GetTicketLookUpByFieldIdAsync(string filter)
        {
            try
            {
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<LookupModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetLookUpsByTicketFieldId,
                              new
                              {
                                  FieldId = filter
                              }
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketLookUpByFieldId  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<LookupModel>().ToList();
        }
 
       public async Task<FieldMappingConfigurationModel> GetTicketFieldMappingByTicketTypeAsync(string ticketTypeId)
        {
            try
            {
                var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<dynamic>
                (
                 DatabaseFactories.TicketManagementDb,
                 StoredProcedures.USP_GetTicketFieldMappingByTicketType,
                new
                {
                    ticketTypeId = ticketTypeId
                }, 2
                ).ConfigureAwait(false);

                List<FormConfigurationModel> _formConfig = new List<FormConfigurationModel>();
                List<GroupingConfigurationModel> _groupConfig = new List<GroupingConfigurationModel>();

                var resultList = result.ToList();

                for (int i = 0; i < resultList.Count; i++)
                {
                    var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));

                    List<Action> actions = new List<Action>
                    {
                        () =>
                        {
                            var formConfigurationModels = DynamicConverter.ConvertToModels<FormConfigurationModel>(dictionaries);
                            _formConfig.AddRange(formConfigurationModels);
                        }
                        ,
                        () =>
                        {
                            var groupingConfigurationModels = DynamicConverter.ConvertToModels<GroupingConfigurationModel>(dictionaries);
                            _groupConfig.AddRange(groupingConfigurationModels);
                        }
                    };


                    actions[i].Invoke();
                }

                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTicketInfoByIdAsync | End Time : {DateTime.Now}");
                return new FieldMappingConfigurationModel()
                {
                    
                    FormConfigurations = _formConfig,
                    GroupConfiguration = _groupConfig
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketFieldMappingByTicketTypeAsync  : [Exception] - {ex.Message}");
            }
            return new FieldMappingConfigurationModel();
        }

        public async Task<List<TicketCustomGroupingResponseModel>> GetTicketCustomGroupByTicketTypeAsync(string ticketTypeId)
        {
            try
            {
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<TicketCustomGroupingResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetTicketCustomGroupByTicketType,
                              new
                              {
                                  ticketTypeId = ticketTypeId
                              }
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketCustomGroupByTicketTypeAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TicketCustomGroupingResponseModel>().ToList();
        }

        public async Task<TicketPlayerResponseModel> GetPlayerByFilterAsync(TicketPlayerRequestModel request)
        {
            try
            {
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<TicketPlayerResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetCustomGroupPlayerDetails,
                              new
                              {
                                  BrandId = request.BrandId,
                                  PlayerId = request.PlayerId,
                                  Username = request.PlayerUsername
                              }
                          ).ConfigureAwait(false);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetPlayerByFilterAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TicketPlayerResponseModel>().FirstOrDefault();
        }

        public async Task<TicketInfoResponseModel> GetTicketInfoByIdAsync(string ticketTypeSequenceId, string ticketTypeId)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTicketInfoByIdAsync | Request: None");
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTicketInfoByIdAsync | Start Time : {DateTime.Now}");
                var result = await _mainDbFactory
                    .ExecuteQueryMultipleAsync<dynamic>
                    (
                     DatabaseFactories.TicketManagementDb,
                     StoredProcedures.USP_GetTicketDetails, new
                     {
                         TicketTypeSequenceId = ticketTypeSequenceId,
                         TicketTypeId = ticketTypeId
                     }, 4
                    ).ConfigureAwait(false);


                var resultList = result.ToList();

                List<TicketInformationModel> _ticketInfo = new List<TicketInformationModel>();
                List<TicketDetailsResponseModel> _ticketDetails = new List<TicketDetailsResponseModel>();
                List<TicketCustomPlayerResponseModel> _ticketPlayers = new List<TicketCustomPlayerResponseModel>();
                List<TicketAttachmentResponseModel> _ticketAttachments = new List<TicketAttachmentResponseModel>();
                for (int i = 0; i < resultList.Count; i++)
                {
                    var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));

                    List<Action> actions = new List<Action>
                    {
                        () =>
                        {
                            var ticketInformationModels = DynamicConverter.ConvertToModels<TicketInformationModel>(dictionaries);
                            _ticketInfo.AddRange(ticketInformationModels);
                        }
                        ,
                        () =>
                        {
                            var ticketDetailsModels = DynamicConverter.ConvertToModels<TicketDetailsResponseModel>(dictionaries);
                            _ticketDetails.AddRange(ticketDetailsModels);
                        }
                        ,
                        () =>
                        {
                            var ticketPlayerModels = DynamicConverter.ConvertToModels<TicketCustomPlayerResponseModel>(dictionaries);
                            _ticketPlayers.AddRange(ticketPlayerModels);
                        }
                        ,
                         () =>
                        {
                            var ticketAttachmentModels = DynamicConverter.ConvertToModels<TicketAttachmentResponseModel>(dictionaries);
                            _ticketAttachments.AddRange(ticketAttachmentModels);
                        }

                    };


                    actions[i].Invoke();
                }

                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTicketInfoByIdAsync | End Time : {DateTime.Now}");
                return new TicketInfoResponseModel()
                {
                    TicketId = _ticketInfo.FirstOrDefault().TicketId,
                    TicketDetails = _ticketDetails,
                    TicketPlayer = _ticketPlayers,
                    TicketAttachments = _ticketAttachments
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketInfoByIdAsync : [Exception] - {ex.Message}");
                return new TicketInfoResponseModel();
            }
        }

        public async Task<ValidateTransactionIdResponsModel> ValidateUnfinishedTransactionIdByTicketAsync(string transactionId, string ticketTypeId, string fieldId)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | ValidateUnfinishedTransactionIdByTicketAsync - Request [ transactionId: {transactionId}, ticketTypeId: {ticketTypeId}, fieldId: {fieldId} ]");
                _logger.LogInfo($"{Factories.TicketManagementFactory} | ValidateUnfinishedTransactionIdByTicketAsync | Start Time : {DateTime.Now}");

                var result = await _mainDbFactory
                      .ExecuteQueryAsync<ValidateTransactionIdResponsModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_ValidateTransactionID,
                              new
                              {
                                  TransactionId = transactionId,
                                  TicketTypeId = ticketTypeId,
                                  FieldId = fieldId
                              }
                          ).ConfigureAwait(false);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | ValidateUnfinishedTransactionIdByTicketAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<ValidateTransactionIdResponsModel>().FirstOrDefault();
        }

        public async Task<long> InsertTicketAttachmentAsync(InsertTicketAttachmentRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | InsertTicketAttachmentAsync - {JsonConvert.SerializeObject(request)}");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<long>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_InsertTicketAttachment,
                              new
                              {
                                  TicketId = request.TicketId,
                                  TicketTypeId = request.TicketTypeId,
                                  TypeId = request.TypeId,
                                  Url = request.Url,
                                  UserId = request.UserId
                              }
                          ).ConfigureAwait(false);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | InsertTicketAttachmentAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<long>().FirstOrDefault();
        }


        #endregion

        public async Task<List<TeamAssignmentResponseModel>> GetTeamAssignmentAsync()
        {
            var result = await _mainDbFactory
                      .ExecuteQueryAsync<TeamAssignmentResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetTeamAssignments,
                              null
                          ).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<List<AssigneeResponseModel>> GetAssigneesByIdsAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, long departmentId, decimal adjustmentAmount)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetAssigneesByIdsAsync - {JsonConvert.SerializeObject(new { statusId = statusId, ticketTypeId = ticketTypeId, paymentMethodId = paymentMethodId, mlabPlayerId = mlabPlayerId, ticketId = ticketId, departmentId = departmentId , adjustmentAmount = adjustmentAmount })}");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<AssigneeResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetAssigneesByIds,
                              new
                              {
                                  StatusId = statusId,
                                  TicketTypeId = ticketTypeId,
                                  PaymentMethodExtId = paymentMethodId,
                                  MlabPlayerId = mlabPlayerId,
                                  TicketId = ticketId,
                                  DepartmentId = departmentId,
                                  AdjustmentAmount = adjustmentAmount
                              }
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetAssigneesByIdsAsync : [Exception] - {ex.Message}");                
            }
            return Enumerable.Empty<AssigneeResponseModel>().ToList();
        }

        public async Task<AutoAssignedIdResponseModel> GetAutoAssignedIdAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId,long departmentId, decimal adjustmentAmount)
        {
            try
            {

                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetAutoAssignedIdAsync - {JsonConvert.SerializeObject(new { statusId = statusId, ticketTypeId = ticketTypeId, paymentMethodId = paymentMethodId, mlabPlayerId = mlabPlayerId, ticketId = ticketId , departmentId = departmentId, adjustmentAmount = adjustmentAmount })}");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<AutoAssignedIdResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetAutoAssignedById,
                              new
                              {
                                  StatusId = statusId,
                                  TicketTypeId = ticketTypeId,
                                  PaymentMethodExtId = paymentMethodId,
                                  MlabPlayerId = mlabPlayerId,
                                  TicketId = ticketId,
                                  departmentId = departmentId,
                                  adjustmentAmount = adjustmentAmount
                              }
                          ).ConfigureAwait(false);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetAutoAssignedIdAsync  : [Exception] - {ex.Message}");
                 return new AutoAssignedIdResponseModel() {                  
                     errMsg = ex.Message,
                 };

            }

        }

        public async Task<List<TicketStatusPopupMappingResponseModel>> GetTicketStatusPopupMappingAsync(long ticketTypeId)
        {
            try
            {
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<TicketStatusPopupMappingResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetTicketStatusPopupMapping,
                              new
                              {
                                  ticketTypeId = ticketTypeId
                              }
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketLookUpByFieldId  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TicketStatusPopupMappingResponseModel>().ToList();

        }
        public async Task<List<TicketThresholdResponseModel>> GetTicketThresholdAsync(GetTicketThresholdRequestModel request)
        {
            try
            {
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<TicketThresholdResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetTicketThreshold,
                              new
                              {
                                  mlabPlayerId = request.MlabPlayerId,
                                  ticketTypeId = request.TicketTypeId
                              }
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketThresholdAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TicketThresholdResponseModel>().ToList();

        }

        public async Task<int> GetFilterIDByUserId(string userId)
        {
            try
            {
                var result = await _mainDbFactory.ExecuteQueryAsync<int>(DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetFilterIDByUserId, new
                {
                    UserId = userId
                }).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetFilterIDByUserId : [Exception] - {ex.Message}");
                return 0;
            }
        }
        public async Task<SearchFilterResponseModel> GetSavedFilterByFilterId(int filterId)
        {
            try
            {
                var result = await _mainDbFactory.ExecuteQueryAsync<SearchFilterResponseModel>(DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetSavedFilterByFilterId, new {
                    filterId = filterId
                }).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetSavedFilterByFilterId : [Exception] - {ex.Message}");
                return Enumerable.Empty<SearchFilterResponseModel>().FirstOrDefault();
            }
        }
        public async Task<TicketManagementLookupsResponseModel> GetTicketManagementLookupsAsync()
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory}| GetTicketManagementLookupsAsync | Start Time : {DateTime.Now}");
                var result = await _mainDbFactory.ExecuteQueryMultipleAsync<
                    TicketManagementLookupModel // Standard look up model
                    >(DatabaseFactories.TicketManagementDb, StoredProcedures.USP_TicketManagementLookups, null, 9).ConfigureAwait(false);

                var resultList = result.ToList();
                _logger.LogInfo($"{Factories.TicketManagementFactory}| GetTicketManagementLookupsAsync | End Time : {DateTime.Now}");
                return new TicketManagementLookupsResponseModel()
                {

                    TicketType = resultList[0].ToList(),
                    Status = resultList[1].ToList(),
                    Assignee = resultList[2].ToList(),
                    Reporter = resultList[3].ToList(),
                    Currency = resultList[4].ToList(),
                    MethodCurrency = resultList[5].ToList(),
                    VIPGroup = resultList[6].ToList(),
                    VIPLevel = resultList[7].ToList(),
                    UserListTeams = resultList[8].ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketManagementLookupsAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TicketManagementLookupsResponseModel>().FirstOrDefault();
        }

        public async Task<List<SearchTicketResponseModel>> ExportSearchTicketByFilters(SearchTicketFilterRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | ExportSearchTicketByFilters - Request [ {request} ]");
                _logger.LogInfo($"{Factories.TicketManagementFactory} | ExportSearchTicketByFilters | Start Time : {DateTime.Now}");

                var result = await _mainDbFactory.ExecuteQueryMultipleAsync<SearchTicketResponseModel, int>(DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetSearchTicketByFilters, new
                {
                    CreatedDateFrom = request.CreatedDateFrom,
                    CreatedDateTo = request.CreatedDateTo,
                    TicketType = request.TicketType,
                    TicketCode = request.TicketCode,
                    Summary = request.Summary,
                    PlayerUsername = request.PlayerUsername,
                    Status = request.Status,
                    Assignee = request.Assignee,
                    Reporter = request.Reporter,
                    ExternalLinkName = request.ExternalLinkName,
                    Currency = request.Currency,
                    MethodCurrency = request.MethodCurrency,
                    VIPGroup = request.VIPGroup,
                    VIPLevel = request.VIPLevel,
                    UserListTeams = request.UserListTeams,
                    PlatformTransactionId = request.PlatformTransactionId,
                    CurrentPage = request.CurrentPage,
                    OffsetValue = request.OffsetValue,
                    PageSize = "",
                    UserId = request.CreatorId,
                    SortColumn = request.SortColumn,
                    SortOrder = request.SortOrder,
                }).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.TicketManagementFactory} | ExportSearchTicketByFilters | End Time : {DateTime.Now}");
                return result.Item1.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | ExportSearchTicketByFilters - {ex.Message}");
            }

            return new List<SearchTicketResponseModel>();
        }

        public async Task<List<TransactionFieldMappingResponseModel>> GetTransactionFieldMappingAsync(long ticketTypeId)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTransactionFieldMappingAsync | TicketTypeId: {ticketTypeId} | Start Time : {DateTime.Now} ");
                var result = await _mainDbFactory
                          .ExecuteQueryAsync<TransactionFieldMappingResponseModel>
                              (
                                  DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetTransactionFieldMapping,
                                  new
                                  {
                                      ticketTypeId = ticketTypeId
                                  }
                              ).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTransactionFieldMappingAsync - Response [ {JsonConvert.SerializeObject(result)} ] End Time : {DateTime.Now}");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTransactionFieldMappingAsync : [Exception]  - {ex.Message}");
                return new List<TransactionFieldMappingResponseModel>();
            }


        }

        public async Task<int> UpsertTransactionDataFromApiAsync(UpsertTransactionDataFromApiRequestModel request)
        {
            try
            {
                var result = await _mainDbFactory.ExecuteQueryAsync(DatabaseFactories.TicketManagementDb, StoredProcedures.USP_UpsertTransactionDataFromApi, new
                {
                    TransactionId = request.TransactionId,
                    PlayerId = request.PlayerId,
                    PaymentMethodName = request.PaymentMethodName,
                    PaymentMethodExt = request.PaymentMethodExt,
                    TransactionDate = request.TransactionDate,
                    BalanceBefore = request.BalanceBefore,
                    TransactionTypeId = request.TransactionTypeId,
                    TransactionStatusId = request.TransactionStatusId,
                    Amount = request.Amount,
                    ProviderTransactionId = request.ProviderTransactionId,
                    ProviderId = request.ProviderId,
                    PaymentInstrumentId = request.PaymentInstrumentId,
                    CustomParameters = request.CustomParameters,
                    UserId = request.UserId,
                    PgTransactionId = request.PgTransactionId,
                    PaymentSystemTransactionStatusId = request.PaymentSystemTransactionStatusId,
                    TransactionHash = request.TransactionHash,
                    MethodCurrency = request.MethodCurrency,
                    ReferenceNumber = request.ReferenceNumber,
                    Remarks = request.Remarks,
                    ReceivedAmount = request.ReceivedAmount,
                    WalletAddress = request.WalletAddress,
                    TicketId = request.TicketId,
                    TicketTypeId = request.TicketTypeId

                }).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | UpsertTransactionDataFromApiAsync : [Exception] - {ex.Message}");
                return 0;
            }
        }

        public async Task<List<PaymentMethodHiddenTicketFieldsResponseModel>> GetHiddenPaymentMethodTicketsAsync(PaymentMethodHiddenTicketFieldsRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetHiddenPaymentMethodTicketsAsync | Start Time : {DateTime.Now} request parameters : {request}" );
                var result = await _mainDbFactory
                          .ExecuteQueryAsync<PaymentMethodHiddenTicketFieldsResponseModel>
                              (
                                  DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetHiddenPaymentMethodTickets,
                                  new
                                  {
                                    TicketTypeId = request.TicketTypeId,
                                    PaymentMethodExtId = request.PaymentMethodExtId,
                                    PageMode = request.PageMode,
                                  }

                              ).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetHiddenPaymentMethodTicketsAsync - Response [ {JsonConvert.SerializeObject(result)} ] End Time : {DateTime.Now}");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetHiddenPaymentMethodTicketsAsync : [Exception]  - {ex.Message} request parameters:  { request}");
                return new List<PaymentMethodHiddenTicketFieldsResponseModel>();
            }


        }

        public async Task<List<LookupModel>> GetAdjustmentBusinessTypeList()
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetAdjustmentBusinessTypeList | Start Time : {DateTime.Now} ");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<LookupModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetAdjustmentBusinessTypeList, new { }
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetAdjustmentBusinessTypeList  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<LookupModel>().ToList();
        }
        public async Task<List<TransactionStatusReferenceResponseModel>> GetTransactionStatusReferenceAsync()
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetHiddenPaymentMethodTicketsAsync | Start Time : {DateTime.Now}");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<TransactionStatusReferenceResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetTransactionStatusReference, null
                          ).ConfigureAwait(false);


                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTransactionStatusReferenceAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TransactionStatusReferenceResponseModel>().ToList();

        }

        public async Task<bool> ValidateAddUserAsCollaborator(ValidateAddUserAsCollaboratorRequestsModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | ValidateAddUserAsCollaborator : [Request] - {JsonConvert.SerializeObject(request)}");

                var result = await _mainDbFactory
                      .ExecuteQueryAsync<bool>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_ValidateAddUserAsCollaborator, new
                              {
                                UserId = request.UserId,
                                Username = request.Username,
                                TicketId = request.TicketId,
                                TIcketTypeId = request.TIcketTypeId,
                                CreatedBy = request.CreatedBy
                              }
                          ).ConfigureAwait(false);


                return result.FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | ValidateAddUserAsCollaborator  : [Exception] - {ex.Message}");
            }

            return false;
        }
        public async Task<bool> DeleteUserAsCollaborator(AddDeleteUserAsCollaboratorRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | DeleteUserAsCollaborator | Start Time : {DateTime.Now}");

                var result = await _mainDbFactory
                      .ExecuteQueryAsync<bool>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_DeleteUserAsCollaborator, new
                              {
                                  TicketCollaboratorID = request.TicketCollaboratorID,
                                  TicketId = request.TicketId,
                                  TIcketTypeId = request.TicketTypeId
                              }
                          ).ConfigureAwait(false);


                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | DeleteUserAsCollaborator  : [Exception] - {ex.Message}");
            }

            return false;
        }
        public async Task<List<LookupModel>> GetUserCollaboratorList()
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetUserCollaboratorList | Start Time : {DateTime.Now} ");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<LookupModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetUserCollaboratorList, null
                          ).ConfigureAwait(false);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetUserCollaboratorList  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<LookupModel>().ToList();
        }

        public async Task<TicketEmailDetails> GetTicketEmailDetails(int? ticketId, int ticketTypeId, int? ticketTypeSequenceId)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetTicketEmailDetails : [Request] - Ticket Id: {ticketId}, Ticket Type Id: {ticketTypeId}, Ticket Type Sequence Id: {ticketTypeSequenceId}");

                var result = await _mainDbFactory.ExecuteQueryAsync<TicketEmailDetails>(DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetEmailDetails, new
                {
                    TicketId = ticketId,
                    TicketTypeId = ticketTypeId,
                    TicketTypeSequenceId = ticketTypeSequenceId
                }).ConfigureAwait(false);

                return result.SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetTicketEmailDetails  : [Exception] - {ex.Message}");
                return new TicketEmailDetails();
            }
        }

        public async Task<bool> ValidateUserTierAsync(ValidateUserTierRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | ValidateUserTierAsync | Start Time : {DateTime.Now}");
                _logger.LogInfo($"{Factories.TicketManagementFactory} | ValidateUserTierAsync : [Request] - {JsonConvert.SerializeObject(request)}");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<bool>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_ValidateUserTier, new
                              {
                                  TIcketTypeId = request.TicketTypeId,
                                  UserId = request.UserId,
                                  MlabPlayerId = request.MlabPlayerId,
                                  AdjustmentAmount = request.AdjustmentAmount
                              }
                          ).ConfigureAwait(false);


                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | ValidateUserTierAsync  : [Exception] - {ex.Message}");
            }

            return false;
        }

        public async Task<List<GetAllPaymentProcessorResponseModel>> GetAllPaymentProcessorAsync()
        {
            try
            {
                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetAllPaymentProcessorAsync | Start Time : {DateTime.Now} ");
                var result = await _mainDbFactory
                      .ExecuteQueryAsync<GetAllPaymentProcessorResponseModel>
                          (
                              DatabaseFactories.TicketManagementDb, StoredProcedures.USP_GetAllPaymentProcessor, null
                          ).ConfigureAwait(false);

                _logger.LogInfo($"{Factories.TicketManagementFactory} | GetAllPaymentProcessorAsync | End Time : {DateTime.Now} ");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.TicketManagementFactory} | GetAllPaymentProcessorAsync  : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<GetAllPaymentProcessorResponseModel>().ToList();
        }
    }
 }

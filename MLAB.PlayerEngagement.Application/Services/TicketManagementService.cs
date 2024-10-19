using Microsoft.Extensions.Options;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Response;
using MLAB.PlayerEngagement.Core.Models.TicketManagement;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Response;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using MLAB.PlayerEngagement.Infrastructure.Config;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MLAB.PlayerEngagement.Application.Services
{
    public class TicketManagementService : ITicketManagementService
    {
        private readonly ILogger<TicketManagementService> _logger;
        private readonly ITicketManagementFactory _ticketManagementFactory;
        private readonly EmailConfig _emailConfig;
        private readonly IOptions<IcoreEventIntegration> _icoreEventIntegration;
        private readonly IOptions<FmboIntegration> _fmboIntegration;
        private readonly IOptions<ManualBalanceCorrectionIntegration> _manualBalanceCorrectionIntegration;
        private readonly IOptions<HoldWithdrawalIntegration> _holdWithdrawalIntegration;

        public TicketManagementService(ILogger<TicketManagementService> logger, ITicketManagementFactory ticketManagementFactory, EmailConfig emailConfig
            ,IOptions<IcoreEventIntegration> icoreEventIntegration, IOptions<FmboIntegration> fmboIntegration, IOptions<ManualBalanceCorrectionIntegration> manualBalanceCorrectionIntegration, IOptions<HoldWithdrawalIntegration> holdWithdrawalIntegration)
        {
            _logger = logger;
            _ticketManagementFactory = ticketManagementFactory;
            _emailConfig = emailConfig;
            _icoreEventIntegration = icoreEventIntegration;
            _fmboIntegration = fmboIntegration;
            _manualBalanceCorrectionIntegration = manualBalanceCorrectionIntegration;
            _holdWithdrawalIntegration = holdWithdrawalIntegration;
        }

        public static string GenerateEmailBodyForTicketManagement(TicketManagementEmailRequestModel emailBodyRequest)
        {
            return @$"<html>
                     <body>
                     <p>Hi <b>{emailBodyRequest.userFullName}</b>,</p>                      
                     <p>We've encountered an error while fetching Ticket Management Data::</p>
                     <p>Source: {emailBodyRequest.Source}</p>
                     <p>Affected Field: {emailBodyRequest.AffectedField}</p>
                     <p>Affected Value: {emailBodyRequest.AffectedValue}</p>
                     <p>Error Details: {emailBodyRequest.ErrorDetails}</p>
                     <p>Thanks and Regards,</p>
                     <p>This is an automated message. Please refrain from replying to this email.</p>
                     </br>
                     <p><b>{emailBodyRequest.emailSignature}</b></p>
                     </body>
                     </html>";

        }

        public static string GenerateEmailBodyForFailedAutoActionFailed(TicketAutoActionFailedModel emailBodyRequest)
        {
            return @$"<html>
                     <body>
                     <p>Hi <b>Everyone</b>,</p>                      
                     <p>Error found when executing Auto Action:</p>
                     <p>Auto Action: {emailBodyRequest.ActionName}</p>
                     <p>Ticket Code: {emailBodyRequest.TicketCode}</p>
                     <p>Ticket Status: {emailBodyRequest.TicketStatus}</p>
                     <p>Error Details: {emailBodyRequest.ErrorDetails}</p>                    
                     <p>This is an automatic email, please do not reply to this email.</p>
                     </br>              
                     </body>
                     </html>";

        }

        
        private void SendEmailAsync(TicketManagementEmailRequestModel emailFormatRequest)
        {
            try
            {
                EmailRequestModel emailRequest = new EmailRequestModel()
                {
                    //Content = EmailHelper.GenerateEmailBody(_emailConfig.RecipientName, errorMsg, _emailConfig.EmailSignature),
                    Content = GenerateEmailBodyForTicketManagement(emailFormatRequest),
                    UserEmail = _emailConfig.RecipientEmail,
                    EmailType = EmailType.emailCreate,
                    Subject = string.Format(_emailConfig.TicketManagementSubject),
                    From = _emailConfig.Email,
                    CC = _emailConfig.Cc,
                    BCC = _emailConfig.Bcc,
                    IsSMTPWithAuth = Convert.ToBoolean(_emailConfig.IsSMTPWithAuth),
                    Email = _emailConfig.Email,
                    SmtpHost = _emailConfig.SmtpHost,
                    Port = Convert.ToInt32(_emailConfig.Port),
                    Password = _emailConfig.Password
                };

                EmailHelper.ProcessMail(emailRequest);

                _logger.LogError(($"[TicketManagement] Email Sent = {emailRequest.Content}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(($"[TicketManagement] | SendEmailAsync | Error in sending email: {ex.Message}"));
            }
        }

        private void SendAutoActionEmailAsync(TicketAutoActionFailedModel emailFormatRequest, TicketEmailDetails emailDetails)
        {
            try
            {
                EmailRequestModel emailRequest = new EmailRequestModel()
                {
                    Content = GenerateEmailBodyForFailedAutoActionFailed(emailFormatRequest),
                    UserEmail = emailDetails.Collaborator,
                    EmailType = EmailType.emailCreate,
                    Subject = string.Format(_emailConfig.TicketManagementAutoActionSubject),
                    From = _emailConfig.Email,
                    CC = _emailConfig.Cc,
                    BCC = _emailConfig.Bcc,
                    IsSMTPWithAuth = Convert.ToBoolean(_emailConfig.IsSMTPWithAuth),
                    Email = _emailConfig.Email,
                    SmtpHost = _emailConfig.SmtpHost,
                    Port = Convert.ToInt32(_emailConfig.Port),
                    Password = _emailConfig.Password
                };

                EmailHelper.ProcessMail(emailRequest);

                _logger.LogError(($"[TicketManagement - Auto Action] Email Sent = {emailRequest.Content}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(($"[TicketManagement - Auto Action] | SendEmailAsync | Error in sending email: {ex.Message}"));
            }
        }

        private async Task<TicketEmailDetails> GetTicketEmailDetails(int? ticketId, int ticketTypeId, int? ticketTypeSequenceId)
        {
            return await _ticketManagementFactory.GetTicketEmailDetails(ticketId, ticketTypeId, ticketTypeSequenceId);
        }

        public async Task<GetIcoreTransactionDataResponseModel> GetIcoreTransactionDataAsync(TransactionDataRequestModel request)
        {
            try
            {
                int retryCount = 0;
                int maxRetryAttempts = 3;
                int retryDelayMilliseconds = 1000;

                do
                {
                    var icoreEventInformationApi = _icoreEventIntegration.Value.IcoreEventInformationUrl;
                    var icoreEventInformationApiKey = _icoreEventIntegration.Value.IcoreEventInformationAPIKey;

                    var client = new HttpClient();
                    var requestToIcoreApi = new HttpRequestMessage(HttpMethod.Post, icoreEventInformationApi + "/transactiondata");

                    _logger.LogInfo($"TicketManagementService | GetIcoreTransactionDataAsync | Request From Api={JsonConvert.SerializeObject(requestToIcoreApi)}");

                    requestToIcoreApi.Headers.Add("X-API-Key", icoreEventInformationApiKey);
                    var jsonSerializeRequestToIcore = JsonSerializer.Serialize(request);
                    var content = new StringContent(jsonSerializeRequestToIcore, null, "application/json");
                    requestToIcoreApi.Content = content;
                    HttpResponseMessage responseFromIcore = await client.SendAsync(requestToIcoreApi);

                    _logger.LogInfo($"TicketManagementService | GetIcoreTransactionDataAsync | start, IcoreTransactionDataParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromIcore)}");

                    if (responseFromIcore.IsSuccessStatusCode)
                    {
                        // Handle successful response
                        var responseFromIcoreReadAsString = await responseFromIcore.Content.ReadAsStringAsync();
                        var responseFromIcoreToJson = JsonSerializer.Deserialize<IcoreTransactionDataResponseModel>(responseFromIcoreReadAsString);
                        _logger.LogInfo($"TicketManagementService | GetIcoreTransactionDataAsync | end, IcoreTransactionDataParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromIcore)}");

                        return new GetIcoreTransactionDataResponseModel()
                        {
                            Amount = Math.Round(responseFromIcoreToJson.Amount, 2),
                            BalanceBefore = responseFromIcoreToJson.BalanceBefore,
                            PaymentInstrumentId = responseFromIcoreToJson.PaymentInstrumentId,
                            PaymentMethodExt = responseFromIcoreToJson.PaymentMethodExt,
                            PaymentMethodName = responseFromIcoreToJson.PaymentMethodName,
                            PlayerId = responseFromIcoreToJson.PlayerId,
                            ProviderId = responseFromIcoreToJson.ProviderId,
                            ProviderTransactionId = responseFromIcoreToJson.ProviderTransactionId,
                            TransactionDate = responseFromIcoreToJson.TransactionDate,
                            TransactionId = responseFromIcoreToJson.TransactionId,
                            TransactionStatusId = responseFromIcoreToJson.TransactionStatusId,
                            TransactionTypeId = responseFromIcoreToJson.TransactionTypeId,
                            AccountHolder = responseFromIcoreToJson.CustomParameters.AccountHolder,
                            BankName = responseFromIcoreToJson.CustomParameters.BankName,
                            AccountNumber = responseFromIcoreToJson.AccountNumber,
                        };
                    }
                    else
                    {

                        retryCount++;

                        if (retryCount <= maxRetryAttempts)
                        {
                            _logger.LogInfo($"TicketManagementService | GetIcoreTransactionDataAsync |retry, Attempt {retryCount} ,Retrying in {retryDelayMilliseconds} milliseconds IcoreTransactionDataParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromIcore.ReasonPhrase)}");
                            await Task.Delay(retryDelayMilliseconds);
                        }
                        else
                        {
                            // Maximum retry attempts reached, handle accordingly
                            _logger.LogInfo($"TicketManagementService | GetIcoreTransactionDataAsync | end,Maximum retry attempts reached, handle accordingly failed to Fetch Data from ICore IcoreTransactionDataParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromIcore.ReasonPhrase)}");
                            var errorMessage = $"GetIcoreTransactionDataAsync. Reached maximum number of retries({maxRetryAttempts}). {DateTime.Now} Error={responseFromIcore.ReasonPhrase}";

                            //formulate email on x3 fail api
                            var requestFromRetry = new TicketManagementEmailRequestModel()
                            {
                                AffectedField = "N/A",
                                AffectedValue = "N/A",
                                Source = "iCore API",
                                emailSignature = _emailConfig.EmailSignature,
                                ErrorDetails = errorMessage,
                                userFullName = "Team",
                            };
                            SendEmailAsync(requestFromRetry);
                            return Enumerable.Empty<GetIcoreTransactionDataResponseModel>().FirstOrDefault();
                        }
                    }
                } while (true);



            }
            catch (Exception ex)
            {
                _logger.LogError($"TicketManagementService | end, GetIcoreTransactionDataAsync Vendor Api: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
                return Enumerable.Empty<GetIcoreTransactionDataResponseModel>().FirstOrDefault();
            }
        }
        public async Task<TicketIntegrationResponseModel> GetFmboTransactionDataAsync(FmboTransactionDataRequestModel request)
        {
            try
            {
                int retryCount = 0;
                int maxRetryAttempts = 3;
                int retryDelayMilliseconds = 1000;
                if (_fmboIntegration.Value.IsFmboIntegrationEnabled)
                {
                    do
                    {
                        var fmboIntegrationApi =  _fmboIntegration.Value.FmboIntegrationUrl;
                        var fmboIntegrationSecretKey = _fmboIntegration.Value.FmboIntegrationSecretKey;

                        var client = new HttpClient();
                        var requestToFmboApi = new HttpRequestMessage(HttpMethod.Post, fmboIntegrationApi);
                        _logger.LogInfo($"TicketManagementService | GetFmboTransactionDataAsync | Request From Api={JsonConvert.SerializeObject(requestToFmboApi)}");


                        request.CheckSum = StringConverter.StringToSHA256(request.Source + request.TransactionId + fmboIntegrationSecretKey);
                        requestToFmboApi.Headers.Add("MLabSecretKey", fmboIntegrationSecretKey);
                        var jsonSerializeRequestToIcore = JsonSerializer.Serialize(request);
                        var content = new StringContent(jsonSerializeRequestToIcore, null, "application/json");
                        requestToFmboApi.Content = content;
                        HttpResponseMessage responseFromFmbo = await client.SendAsync(requestToFmboApi);
                        var responseFromFmboReadAsString = await responseFromFmbo.Content.ReadAsStringAsync();
                        var responseFromFmboToJson = JsonSerializer.Deserialize<FmboTransactionDataResponseModel>(responseFromFmboReadAsString);
                        _logger.LogInfo($"TicketManagementService | GetFmboTransactionDataAsync | start, FmboTransactionDataParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromFmboToJson)}");

                        if (responseFromFmbo.IsSuccessStatusCode)
                        {
                        
                            if (responseFromFmboToJson.ErrorCode == null)
                            {
                                decimal.TryParse(responseFromFmboToJson.TransactionInfo.CryptoCreditedAmount, out decimal result);

                                var cryptoCurrency = responseFromFmboToJson.TransactionInfo.CryptoCurrency;
                                var isCrypto = !string.IsNullOrWhiteSpace(cryptoCurrency) && cryptoCurrency.Trim() != "()";

                                var methodCurrency = isCrypto ? cryptoCurrency : responseFromFmboToJson.TransactionInfo.Currency;
                                var fiat = isCrypto ? "(Crypto)" : "(FIAT)";

                                string convertedMethodCurrency = CurrencyConverter.ExtractCurrencyCode(methodCurrency);
                                _logger.LogInfo($"TicketManagementService | GetFmboTransactionDataAsync | end, Success");

                                return new TicketIntegrationResponseModel()
                                {
                                    ResponseData = new GetFmboTransactionDataResponseModel()
                                    {
                                        PaymentMethodName = responseFromFmboToJson.TransactionInfo.PaymentMethod,
                                        PaymentMethodExt = responseFromFmboToJson.TransactionInfo.PaymentMethod,
                                        //TransactionDate = timestamp,
                                        CryptoCurrency = responseFromFmboToJson.TransactionInfo.CryptoCurrency,
                                        CryptoAmount = Math.Round(result, 2),
                                        PgTransactionId = responseFromFmboToJson.TransactionInfo.PgTransactionId,
                                        PaymentSystemTransactionStatusId = responseFromFmboToJson.TransactionInfo.Status,
                                        TransactionHash = responseFromFmboToJson.TransactionInfo.CryptoTransactionHash,
                                        MethodCurrency = convertedMethodCurrency + " " + fiat,
                                        ReferenceNumber = responseFromFmboToJson.TransactionInfo?.ReferenceNumber ?? "",
                                        Remarks = responseFromFmboToJson.TransactionInfo.Remarks, // question
                                        RecievedAmount = Math.Round(responseFromFmboToJson.TransactionInfo.CreditedAmount, 2),
                                        WalletAddress = responseFromFmboToJson.TransactionInfo.WalletAddress,
                                        ProviderTransactionId = request.TransactionId,
                                        TransactionId = responseFromFmboToJson.TransactionInfo.OperatorTransactionId,
                                        PaymentProcessor = responseFromFmboToJson.TransactionInfo.PaymentProcessor

                                    },
                                    IsIntegrationEnabled = _fmboIntegration.Value.IsFmboIntegrationEnabled
                                };
                            }
                            else
                            {

                                _logger.LogInfo($"TicketManagementService | GetFmboTransactionDataAsync | end | Response Not Successful | Response From FMBO ={JsonConvert.SerializeObject(responseFromFmbo)} | Json Response={JsonConvert.SerializeObject(responseFromFmboToJson)}");
                                _logger.LogInfo($"TicketManagementService | GetFmboTransactionDataAsync | ErrorCode = {responseFromFmboToJson.ErrorCode} ");
                                return new TicketIntegrationResponseModel()
                                {
                                    ResponseData = Enumerable.Empty<GetFmboTransactionDataResponseModel>().FirstOrDefault(),
                                    IsIntegrationEnabled = _fmboIntegration.Value.IsFmboIntegrationEnabled
                                };
                            }


                        }
                        else
                        {

                            retryCount++;

                            if (retryCount <= maxRetryAttempts)
                            {
                                _logger.LogInfo($"TicketManagementService | GetFmboTransactionDataAsync |retry, Attempt {retryCount} ,Retrying in {retryDelayMilliseconds} milliseconds FmboTransactionDataParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromFmbo.ReasonPhrase)}");
                                await Task.Delay(retryDelayMilliseconds);
                            }
                            else
                            {
                                // Maximum retry attempts reached, handle accordingly
                                 _logger.LogInfo($"TicketManagementService | GetFmboTransactionDataAsync | end,Maximum retry attempts reached, handle accordingly failed to Fetch Data from FMBO FmboTransactionDataParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromFmbo.ReasonPhrase)}");
                                var errorMessage = $"GetFmboTransactionDataAsync. Reached maximum number of retries({maxRetryAttempts}). {DateTime.Now} Error={maxRetryAttempts}";

                                //formulate email on x3 fail api
                                var requestFromRetry = new TicketManagementEmailRequestModel()
                                {
                                    AffectedField = "N/A",
                                    AffectedValue = "N/A",
                                    Source = "Fmbo API",
                                    emailSignature = _emailConfig.EmailSignature,
                                    ErrorDetails = errorMessage,
                                    userFullName = "Team",
                                };
                                SendEmailAsync(requestFromRetry);
                                return new TicketIntegrationResponseModel()
                                {
                                    ResponseData = Enumerable.Empty<GetFmboTransactionDataResponseModel>().FirstOrDefault(),
                                    IsIntegrationEnabled = _fmboIntegration.Value.IsFmboIntegrationEnabled
                                };
                            }
                        }
                    } while (true);
                }else
                {
                    _logger.LogError($"TicketManagementService | GetFmboTransactionDataAsync |  disabled");
                    return new TicketIntegrationResponseModel()
                    {
                        ResponseData = Enumerable.Empty<GetFmboTransactionDataResponseModel>().FirstOrDefault(),
                        IsIntegrationEnabled = _fmboIntegration.Value.IsFmboIntegrationEnabled
                    };

                }
                

            }
            catch (Exception ex)
            {
                _logger.LogError($"TicketManagementService | end, GetFmboTransactionDataAsync Vendor Api: [Exception] - {ex} FmboTransactionDataParameter={JsonConvert.SerializeObject(request)}");
                return new TicketIntegrationResponseModel()
                {
                    ResponseData = Enumerable.Empty<GetFmboTransactionDataResponseModel>().FirstOrDefault(),
                    IsIntegrationEnabled = _fmboIntegration.Value.IsFmboIntegrationEnabled
                };
            }
        }


        public async Task<MlabTransactionResponseModel> GetMlabTransactionDataAsync(GetMlabRequestModel request)
        {
            return await _ticketManagementFactory.GetMlabTransactionDataAsync(request);
        }

        public async Task<List<TicketStatusHierarchyResponseModel>> GetTicketStatusHierarchyByTicketTypeAsync(TicketStatusHierarchyRequestModel request)
        {
            return await _ticketManagementFactory.GetTicketStatusHierarchyByTicketTypeAsync(request);
        }


        #region Ticket Configuration

        public async Task<List<TicketTypesResponseModel>> GetTicketTypesAsync()
        {
            return await _ticketManagementFactory.GetTicketTypesAsync();
        }

        public async Task<List<LookupModel>> GetTicketLookUpByFieldIdAsync(string filter)
        {
            return await _ticketManagementFactory.GetTicketLookUpByFieldIdAsync(filter);
        }

      
        public async Task<FieldMappingConfigurationModel> GetTicketFieldMappingByTicketTypeAsync(string ticketTypeId)
        {
            var results = await _ticketManagementFactory.GetTicketFieldMappingByTicketTypeAsync(ticketTypeId);
            return new FieldMappingConfigurationModel
            {
                
                FormConfigurations = results.FormConfigurations,
                GroupConfiguration = results.GroupConfiguration,

            };
        }

        public async Task<List<TicketCustomGroupingResponseModel>> GetTicketCustomGroupByTicketTypeAsync(string ticketTypeId)
        {
            return await _ticketManagementFactory.GetTicketCustomGroupByTicketTypeAsync(ticketTypeId);
        }

        public async Task<TicketPlayerResponseModel> GetPlayerByFilterAsync(TicketPlayerRequestModel request)
        {
            return await _ticketManagementFactory.GetPlayerByFilterAsync(request);
        }

        public async Task<TicketInfoResponseModel> GetTicketInfoByIdAsync(string ticketTypeSequenceId, string ticketTypeId)
        {
            var results = await _ticketManagementFactory.GetTicketInfoByIdAsync(ticketTypeSequenceId, ticketTypeId);
            return new TicketInfoResponseModel
            {
                TicketId = results.TicketId,
                TicketDetails = results.TicketDetails,
                TicketPlayer = results.TicketPlayer,
                TicketAttachments = results.TicketAttachments,

            };
        }
        #endregion

        public async Task<ValidateTransactionIdResponsModel> ValidateUnfinishedTransactionIdByTicketAsync(string transactionId, string ticketTypeId, string fieldId)
        {
            return await _ticketManagementFactory.ValidateUnfinishedTransactionIdByTicketAsync(transactionId, ticketTypeId, fieldId);
        }
        public async Task<List<TeamAssignmentResponseModel>> GetTeamAssignmentAsync()
        {
            return await _ticketManagementFactory.GetTeamAssignmentAsync();
        }

        public async Task<List<AssigneeResponseModel>> GetAssigneesByIdsAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, long departmentId, decimal adjustmentAmount)
        {
            var result = await _ticketManagementFactory.GetAssigneesByIdsAsync(statusId, ticketTypeId, paymentMethodId, mlabPlayerId, ticketId , departmentId, adjustmentAmount);

            return result;
        }

        public async Task<AutoAssignedIdResponseModel> GetAutoAssignedIdAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, string ticketCode, string statusDescription, long departmentId, decimal adjustmentAmount)
        {
            var result = await _ticketManagementFactory.GetAutoAssignedIdAsync(statusId, ticketTypeId, paymentMethodId, mlabPlayerId, ticketId, departmentId, adjustmentAmount);

            if (result != null && result.errMsg != null)
            {
                var requestFailedAutoAssigned = new TicketAutoActionFailedModel()
                {
                    TicketCode = ticketCode == null || ticketCode == "" ? "N/A" : ticketCode,
                    TicketStatus = statusDescription == null || statusDescription == "" ? "N/A" : statusDescription,
                    ActionName = "Assign Ticket",
                    ErrorDetails = result.errMsg == null ? "" : result.errMsg,
                };

                var emailDetails = await GetTicketEmailDetails(ticketId, ticketTypeId, 0);
                SendAutoActionEmailAsync(requestFailedAutoAssigned, emailDetails);
            }

            return result;
        }


        public async Task<List<TicketStatusPopupMappingResponseModel>> GetTicketStatusPopupMappingAsync(long ticketTypeId)
        {
            return await _ticketManagementFactory.GetTicketStatusPopupMappingAsync(ticketTypeId);
        }

        public async Task<int> GetFilterIDByUserId(string userId)
        {
            return await _ticketManagementFactory.GetFilterIDByUserId(userId);
        }

        public async Task<SearchFilterResponseModel> GetSavedFilterByFilterId(int filterId)
        {
            return await _ticketManagementFactory.GetSavedFilterByFilterId(filterId);
        }
        public async Task<TicketManagementLookupsResponseModel> GetTicketManagementLookupsAsync()
        {
            return await _ticketManagementFactory.GetTicketManagementLookupsAsync();
        }
        public async Task<List<TicketThresholdResponseModel>> GetTicketThresholdAsync(GetTicketThresholdRequestModel request)
        {
            return await _ticketManagementFactory.GetTicketThresholdAsync(request);
        }

        public async Task<List<SearchTicketResponseModel>> ExportSearchTicketByFilters(SearchTicketFilterRequestModel request)
        {
            return await _ticketManagementFactory.ExportSearchTicketByFilters(request);
        }

        public async Task<long> InsertTicketAttachmentAsync(InsertTicketAttachmentRequestModel request)
        {
            return await _ticketManagementFactory.InsertTicketAttachmentAsync(request);
        }
        public async Task<List<TransactionFieldMappingResponseModel>> GetTransactionFieldMappingAsync(long ticketTypeId)
        {
            return await _ticketManagementFactory.GetTransactionFieldMappingAsync(ticketTypeId);
        }

        public async Task<int> UpsertTransactionDataFromApiAsync(UpsertTransactionDataFromApiRequestModel request)
        {
            return await _ticketManagementFactory.UpsertTransactionDataFromApiAsync(request);
        }

        // Supersede fields
        public async Task<List<PaymentMethodHiddenTicketFieldsResponseModel>> GetHiddenPaymentMethodTicketsAsync(PaymentMethodHiddenTicketFieldsRequestModel request)
        {
            return await _ticketManagementFactory.GetHiddenPaymentMethodTicketsAsync(request);
        }

        public async Task<TicketIntegrationResponseModel> PostManualBalanceCorrection(ManualBalanceCorrectionRequestModel request)
        {
            try
            {
                int retryCount = 0;
                int maxRetryAttempts = 3;
                int retryDelayMilliseconds = 1000;
                var isManualBalanceCorrectionIntegrationEnabled = _manualBalanceCorrectionIntegration.Value.IsManualBalanceCorrectionIntegrationEnabled;
                if (isManualBalanceCorrectionIntegrationEnabled)
                {
                    do
                    {
                        var manualBalanceCorrectionUrl = _manualBalanceCorrectionIntegration.Value.ManualBalanceCorrectionUrl;

                        var client = new HttpClient();
                        var requestToMBCApi = new HttpRequestMessage(HttpMethod.Post, manualBalanceCorrectionUrl);
                        var jsonSerializeRequestToMBC = JsonSerializer.Serialize(request);
                        var content = new StringContent(jsonSerializeRequestToMBC, null, "application/json");
                        requestToMBCApi.Content = content;
                        HttpResponseMessage responseFromMBC = await client.SendAsync(requestToMBCApi);

                        _logger.LogInfo($"TicketManagementService | PostManualBalanceCorrection | start, ManualBalanceCorrectionParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromMBC)}");

                        if (responseFromMBC.IsSuccessStatusCode)
                        {
                            // Handle successful response
                            var responseFromMBCReadAsString = await responseFromMBC.Content.ReadAsStringAsync();
                            var responseFromMBCToJson = JsonSerializer.Deserialize<ManualBalanceCorrectionResponseModel>(responseFromMBCReadAsString);
                            _logger.LogInfo($"TicketManagementService | PostManualBalanceCorrection | end, ManualBalanceCorrectionParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromMBC.ReasonPhrase)}");

                            return new TicketIntegrationResponseModel() 
                            { 
                                ResponseData = new ManualBalanceCorrectionResponseModel()
                                {
                                    TransactionId = responseFromMBCToJson.TransactionId,
                                    Status = responseFromMBCToJson.Status,
                                    DateTime = responseFromMBCToJson.DateTime,
                                    StatusCode = responseFromMBCToJson.StatusCode,
                                    ErrorMessage = responseFromMBCToJson.ErrorMessage,
                                },
                                IsIntegrationEnabled = _manualBalanceCorrectionIntegration.Value.IsManualBalanceCorrectionIntegrationEnabled
                            };
                        }
                        else
                        {
                            retryCount++;

                            if (retryCount <= maxRetryAttempts)
                            {
                                _logger.LogInfo($"TicketManagementService | PostManualBalanceCorrection |retry, Attempt {retryCount} ,Retrying in {retryDelayMilliseconds} milliseconds ManualBalanceCorrectionParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromMBC.ReasonPhrase)}");
                                await Task.Delay(retryDelayMilliseconds);
                            }
                            else
                            {
                                // Maximum retry attempts reached, handle accordingly
                                _logger.LogInfo($"TicketManagementService | PostManualBalanceCorrection | end,Maximum retry attempts reached, handle accordingly failed to Fetch Data from ManualBalanceCorrectionParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromMBC.ReasonPhrase)}");
                                var errorMessage = $"PostManualBalanceCorrection. Reached maximum number of retries({maxRetryAttempts}). {DateTime.Now} Error={responseFromMBC.ReasonPhrase}";

                                //formulate email on x3 fail api
                                var requestFromRetry = new TicketManagementEmailRequestModel()
                                {
                                    AffectedField = "N/A",
                                    AffectedValue = "N/A",
                                    Source = "Manual Balance Correction API",
                                    emailSignature = _emailConfig.EmailSignature,
                                    ErrorDetails = errorMessage,
                                    userFullName = "Team",
                                };
                                SendEmailAsync(requestFromRetry);
                                return new TicketIntegrationResponseModel()
                                {
                                    ResponseData = Enumerable.Empty<ManualBalanceCorrectionResponseModel>().FirstOrDefault(),
                                    IsIntegrationEnabled = _manualBalanceCorrectionIntegration.Value.IsManualBalanceCorrectionIntegrationEnabled
                                };
                            }
                        }
                    } while (true);
                } 
                else
                {
                    _logger.LogError($"TicketManagementService | PostManualBalanceCorrection |  disabled");
                    return new TicketIntegrationResponseModel()
                    {
                        ResponseData = Enumerable.Empty<ManualBalanceCorrectionResponseModel>().FirstOrDefault(),
                        IsIntegrationEnabled = _manualBalanceCorrectionIntegration.Value.IsManualBalanceCorrectionIntegrationEnabled
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"TicketManagementService | end, PostManualBalanceCorrection Vendor Api: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
                return new TicketIntegrationResponseModel()
                {
                    ResponseData = Enumerable.Empty<ManualBalanceCorrectionResponseModel>().FirstOrDefault(),
                    IsIntegrationEnabled = _manualBalanceCorrectionIntegration.Value.IsManualBalanceCorrectionIntegrationEnabled
                };
            }
        }

        public async Task<TicketIntegrationResponseModel> PostICoreHoldWithdrawal(HoldWithdrawalRequestModel request)
        {
            try
            {
                int retryCount = 0;
                int maxRetryAttempts = 3;
                int retryDelayMilliseconds = 1000;
                var isHoldWithdrawalIntegrationEnabled = _holdWithdrawalIntegration.Value.IsHoldWithdrawalIntegrationEnabled;
                if (isHoldWithdrawalIntegrationEnabled)
                {
                    do
                    {
                        var holdWithdrawalUrl = _holdWithdrawalIntegration.Value.HoldWithdrawalUrl;

                        var client = new HttpClient();
                        var requestToHoldWithdrawalApi = new HttpRequestMessage(HttpMethod.Post, holdWithdrawalUrl);
                        var jsonSerializeRequestToHoldWithdrawal = JsonSerializer.Serialize(request);
                        var content = new StringContent(jsonSerializeRequestToHoldWithdrawal, null, "application/json");
                        requestToHoldWithdrawalApi.Content = content;
                        HttpResponseMessage responseFromHoldWithdrawal = await client.SendAsync(requestToHoldWithdrawalApi);

                        _logger.LogInfo($"TicketManagementService | PostICoreHoldWithdrawal | start, PostICoreHoldWithdrawalParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromHoldWithdrawal)}");

                        if (responseFromHoldWithdrawal.IsSuccessStatusCode)
                        {
                            // Handle successful response
                            var responseFromHoldWithdrawalReadAsString = await responseFromHoldWithdrawal.Content.ReadAsStringAsync();
                            var responseFromHoldWithdrawalToJson = JsonSerializer.Deserialize<HoldWithdrawalResponseModel>(responseFromHoldWithdrawalReadAsString);
                            _logger.LogInfo($"TicketManagementService | PostICoreHoldWithdrawal | end, PostICoreHoldWithdrawalParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromHoldWithdrawal.ReasonPhrase)}");

                            return new TicketIntegrationResponseModel() 
                            {
                                ResponseData = new HoldWithdrawalResponseModel()
                                {
                                    NonExistingPlayers = responseFromHoldWithdrawalToJson.NonExistingPlayers,
                                    RecordCount = responseFromHoldWithdrawalToJson.RecordCount,
                                    StatusCode = responseFromHoldWithdrawalToJson.StatusCode,
                                    ErrorMessage = responseFromHoldWithdrawalToJson.ErrorMessage
                                },
                                IsIntegrationEnabled = _holdWithdrawalIntegration.Value.IsHoldWithdrawalIntegrationEnabled
                            };
                        }
                        else
                        {
                            retryCount++;

                            if (retryCount <= maxRetryAttempts)
                            {
                                _logger.LogInfo($"TicketManagementService | PostICoreHoldWithdrawal |retry, Attempt {retryCount} ,Retrying in {retryDelayMilliseconds} milliseconds PostICoreHoldWithdrawalParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromHoldWithdrawal.ReasonPhrase)}");
                                await Task.Delay(retryDelayMilliseconds);
                            }
                            else
                            {
                                // Maximum retry attempts reached, handle accordingly
                                _logger.LogInfo($"TicketManagementService | PostICoreHoldWithdrawal | end,Maximum retry attempts reached, handle accordingly failed to Fetch Data from ICore PostICoreHoldWithdrawalParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(responseFromHoldWithdrawal.ReasonPhrase)}");
                                var errorMessage = $"PostICoreHoldWithdrawal. Reached maximum number of retries({maxRetryAttempts}). {DateTime.Now} Error={responseFromHoldWithdrawal.ReasonPhrase}";

                                //formulate email on x3 fail api
                                var requestFromRetry = new TicketManagementEmailRequestModel()
                                {
                                    AffectedField = "N/A",
                                    AffectedValue = "N/A",
                                    Source = "ICore Hold Withdrawal API",
                                    emailSignature = _emailConfig.EmailSignature,
                                    ErrorDetails = errorMessage,
                                    userFullName = "Team",
                                };
                                SendEmailAsync(requestFromRetry);
                                return new TicketIntegrationResponseModel()
                                {
                                    ResponseData = Enumerable.Empty<HoldWithdrawalResponseModel>().FirstOrDefault(),
                                    IsIntegrationEnabled = _holdWithdrawalIntegration.Value.IsHoldWithdrawalIntegrationEnabled
                                };
                            }
                        }
                    } while (true);
                }
                else
                {
                    _logger.LogError($"TicketManagementService | PostICoreHoldWithdrawal |  disabled");
                    return new TicketIntegrationResponseModel()
                    {
                        ResponseData = Enumerable.Empty<HoldWithdrawalResponseModel>().FirstOrDefault(),
                        IsIntegrationEnabled = _holdWithdrawalIntegration.Value.IsHoldWithdrawalIntegrationEnabled
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"TicketManagementService | end, PostICoreHoldWithdrawal Vendor Api: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
                return new TicketIntegrationResponseModel()
                {
                    ResponseData = Enumerable.Empty<HoldWithdrawalResponseModel>().FirstOrDefault(),
                    IsIntegrationEnabled = _holdWithdrawalIntegration.Value.IsHoldWithdrawalIntegrationEnabled
                };
            }
        }

        public async Task<List<LookupModel>> GetAdjustmentBusinessTypeList()
        {
            return await _ticketManagementFactory.GetAdjustmentBusinessTypeList();
        }
        public async Task<List<TransactionStatusReferenceResponseModel>> GetTransactionStatusReferenceAsync()
        {
            return await _ticketManagementFactory.GetTransactionStatusReferenceAsync();
        }

        public async Task<bool> ValidateAddUserAsCollaborator(ValidateAddUserAsCollaboratorRequestsModel request)
        {
            return await _ticketManagementFactory.ValidateAddUserAsCollaborator(request);
        }
        public async Task<bool> DeleteUserAsCollaborator(AddDeleteUserAsCollaboratorRequestModel request)
        {
            return await _ticketManagementFactory.DeleteUserAsCollaborator(request);
        }
        public async Task<List<LookupModel>> GetUserCollaboratorList()
        {
            return await _ticketManagementFactory.GetUserCollaboratorList();
        }
        public async Task<bool> ValidateUserTierAsync(ValidateUserTierRequestModel request)
        {
            return await _ticketManagementFactory.ValidateUserTierAsync(request);
        }
        public async Task<List<GetAllPaymentProcessorResponseModel>> GetAllPaymentProcessorAsync()
        {
            return await _ticketManagementFactory.GetAllPaymentProcessorAsync();
        }
    }
}
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MLAB.PlayerEngagement.Application.Helpers;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Request;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Response;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Request;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Response;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Udt;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Samespace.Request;
using MLAB.PlayerEngagement.Core.Models.Samespace.Response;
using MLAB.PlayerEngagement.Core.Models.Users.Udt;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Config;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace MLAB.PlayerEngagement.Application.Services
{

    public class CaseManagementService : ICaseManagementService
    {
        private readonly ILogger<CaseManagementService> _logger;
        private readonly ICaseManagementFactory _caseManagementFactory;
        private readonly IConfiguration _configuration;
        private readonly ISystemService _systemService;
        private readonly IOptions<AppSetting> _appSetting;

        public CaseManagementService(IMediator mediator, ILogger<CaseManagementService> logger, ICaseManagementFactory caseManagementFactor, IConfiguration configuration, ISystemService systemService, IOptions<AppSetting> appSetting)
        {
            _logger = logger;
            _caseManagementFactory = caseManagementFactor;
            _configuration = configuration;
            _systemService = systemService;
            _appSetting = appSetting;
        }

        public async Task<CustomerCaseModel> GetCustomerCaseByIdAsync(int customerCaseId, long userId)
        {
            var result = await _caseManagementFactory.GetCustomerCaseByIdAsync(customerCaseId, userId);
            if (result != null)
            {
                return new CustomerCaseModel
                {
                    CaseInformationId = result.CaseInformationId,
                    Brand = result.Brand,
                    CaseType = result.CaseType,
                    CaseStatus = result.CaseStatus,
                    CaseOwner = result.CaseOwner,
                    Username = result.Username,
                    MlabPlayerId = result.MlabPlayerId,
                    Currency = result.Currency,
                    VipLevel = result.VipLevel,
                    PaymentGroup = result.PaymentGroup,
                    PlayerId = result.PlayerId,
                    CaseOrigin = result.CaseOrigin,
                    CaseCreatedBy = result.CaseCreatedBy,
                    CaseCreatedDate = result.CaseCreatedDate,
                    UpdatedBy = result.UpdatedBy,
                    UpdatedDate = result.UpdatedDate,
                    Subject = result.Subject,
                    LanguageCode = result.LanguageCode,
                    Topic = result.Topic,
                    Subtopic = result.Subtopic,
                    CampaignList = result.CampaignList,
                };
            }

            return null;
        }
        public async Task<CustomerCaseCommunicationListModel> GetCustomerCaseCommListAsync(CustomerCaseCommunicationListRequest request)
        {
            var result = await _caseManagementFactory.GetCustomerCaseCommListAsync(request);
            return new CustomerCaseCommunicationListModel
            {
                RecordCount = result.Item1[0],
                CaseCommunicationList = result.Item2
            };
        }

        public async Task<CustomerCaseCommunicationTabsModel> GetCaseCommunicationByIdAsync(int communicationId, long userId)
        {
            var result = await _caseManagementFactory.GetCaseCommunicationByIdAsync(communicationId, userId);
            return new CustomerCaseCommunicationTabsModel
            {
                CustomerCaseCommunicationInfo = result.CustomerCaseCommunicationInfo,
                PCSData = result.PcsData,
                CustomerCaseCommunicationFeedback = result.CustomerCaseCommunicationFeedbacks,
                CustomerCaseCommunicationSurvey = result.CustomerCaseCommunicationSurveys
            };
        }

        public async Task<List<CommunicationOwnerResponseList>> GetAllCommunicationOwnerAsync()
        {
            var result = await _caseManagementFactory.GetAllCommunicationOwnersAsync();
            return result;
        }

        public async Task<List<CaseCommunicationFilterResponse>> GetCaseCommunicationListCsvAsync(CaseCommunicationFilterRequest request)
        {
            var result = await _caseManagementFactory.GetCaseCommunicationListCsvAsync(request);
            return result;
        }

        public async Task<PlayerInfoCaseCommunicationResponse> ValidatePlayerCaseCommunicationAsync(long mlabPlayerId)
        {
            return await _caseManagementFactory.ValidatePlayerCaseCommunicationAsync(mlabPlayerId);
        }
        public async Task<CaseInformationResponse> GetCustomerServiceCaseInformationByIdAsync(long caseInformationId, long userId)
        {
            return await _caseManagementFactory.GetCustomerServiceCaseInformationByIdAsync(caseInformationId, userId);
        }

        public async Task<List<PlayerCaseCommunicationResponse>> GetPlayersByUsernameAsync(string username, int brandId, long userId)
        {
            return await _caseManagementFactory.GetPlayersByUsernameAsync(username, brandId, userId);

        }
        public async Task<List<PlayerCaseCommunicationResponse>> GetPlayersByPlayerIdAsync(string playerId, int brandId, long userId)
        {
            return await _caseManagementFactory.GetPlayersByPlayerIdAsync(playerId, brandId, userId);
        }

        public async Task<PcsQuestionaireResponseModel> GetPCSQuestionaireListCsvAsync(PCSQuestionaireListByFilterRequestModel request)
        {
            var result = await _caseManagementFactory.GetPCSQuestionaireListCsvAsync(request);
            return new PcsQuestionaireResponseModel
            {
                RecordList = result.Item1.ToList(),
                QuestionAnswer = result.Item2.ToList()
            };

        }

        public async Task<List<PCSCommunicationQuestionsByIdResponseModel>> GetPCSCommunicationQuestionsByIdAsync(long caseCommunicationId)
        {
            return await _caseManagementFactory.GetPCSCommunicationQuestionsByIdAsync(caseCommunicationId);
        }

        public async Task<SurveyTemplateResponse> GetCustomerCaseCommSurveyTemplateByIdAsync(int surveyTemplateId)
        {
            var result = await _caseManagementFactory.GetCustomerCaseCommSurveyTemplateByIdAsync(surveyTemplateId);

            return result;
        }

        public async Task<List<CustomerCaseCommunicationFeedbackResponseModel>> GetCaseCommunicationFeedbackByIdAsync(int communicationId)
        {
            var result = await _caseManagementFactory.GetCaseCommunicationFeedbackByIdAsync(communicationId);

            return result;
        }

        public async Task<List<CustomerCaseCommunicationSurveyResponseModel>> GetCaseCommunicationSurveyByIdAsync(int communicationId)
        {
            var result = await _caseManagementFactory.GetCaseCommunicationSurveyByIdAsync(communicationId);

            return result;
        }

        public async Task<List<LookupModel>> GetCustomerCaseSurveyTemplateAsync(int caseTypeId)
        {
            var surveyTemplate = await _caseManagementFactory.GetCustomerCaseSurveyTemplateAsync(caseTypeId);
            return surveyTemplate;
        }

        public async Task<List<PCSQuestionaireListByFilterResponseModel>> GetCaseManagementPCSQuestionsByFilterAsync(CaseManagementPCSQuestionsByFilterRequestModel request)
        {
            return await _caseManagementFactory.GetCaseManagementPCSQuestionsByFilterAsync(request);
        }

        public async Task<CaseManagementPcsCommunicationByFilterResponseModel> GetCaseManagementPCSCommunicationByFilterAsync(CaseManagementPCSCommunicationByFilterRequestModel request)
        {
            return await _caseManagementFactory.GetCaseManagementPCSCommunicationByFilterAsync(request);
        }

        public async Task<FlyFoneOutboundCallResponsetModel> FlyFoneOutboundCallAsync(FlyFoneOutboundCallRequestModel request)
        {
            try
            {
                var recordedParam = await _caseManagementFactory.FlyFoneOutboundCallAsync(request);

                if (recordedParam.MobileNumber.Equals('0'))
                {
                    return Enumerable.Empty<FlyFoneOutboundCallResponsetModel>().FirstOrDefault();
                }
                else
                {
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            var flyFoneSetting = await _caseManagementFactory.GetFlyFoneAppSettingsAsync();

                            httpClient.BaseAddress = new Uri(flyFoneSetting.BaseUrl);

                            var callResult = await httpClient.GetFromJsonAsync<FlyfoneOutboundCallApiResponseModel>($"?outnumber={recordedParam.MobileNumber}&secret=API@CallHttp$!2018&ext={request.Ext}&department={request.Department}&dial_id={recordedParam.DialId}");
                            
                            _logger.LogInfo($"CaseManagementService | FlyFoneOutboundCallAsync | FlyfoneOutboundCallParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(callResult)}");
                           
                            return new FlyFoneOutboundCallResponsetModel()
                            {
                                DialId = recordedParam.DialId,
                                ResultCode = callResult.ResultCode,
                                ResultDesc = callResult.ResultDesc,
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"CaseManagementService | FlyFoneOutboundCallAsync Exception: {ex.InnerException}");
                        return Enumerable.Empty<FlyFoneOutboundCallResponsetModel>().FirstOrDefault();
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | FlyFoneOutboundCallAsync Exception: {ex.InnerException}");
                return Enumerable.Empty<FlyFoneOutboundCallResponsetModel>().FirstOrDefault();
            }
        }

        public async Task<List<FlyFoneCallDetailRecordResponseModel>> GetFlyFoneCallDetailRecordsAsync()
        {
            return await _caseManagementFactory.GetFlyFoneCallDetailRecordsAsync();
        }

        public async Task<FormattedFlyFoneCdrUdt> FlyFoneEndOutboundCallAsync(FlyFoneCallDetailRecordRequestModel request)
        {
            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                    var flyFoneSettings = await _caseManagementFactory.GetFlyFoneAppSettingsAsync();

                    _httpClient.BaseAddress = new Uri(flyFoneSettings.BaseUrl);

                    var callDetailResult = await _httpClient.GetFromJsonAsync<FlyFoneCallDetailRecordApiResponse>($"cdr.php?secretCode=M88@CDRapi&calling_code={request.CallingCode}");

                    _logger.LogInfo($"CaseManagementService | FlyFoneEndOutboundCallAsync | FlyfoneFetchCDRParameter={JsonConvert.SerializeObject(request)} Response={JsonConvert.SerializeObject(callDetailResult)}");

                    if (callDetailResult.Data.Count != 0)
                    {
                        var requestToSaveDetail = new FlyFoneSaveCallDetailRecordRequestModel()
                        {
                            UserId = request.UserId,
                            EndTime = request.EndTime,
                            FlyFoneCallDetailRecordType = callDetailResult.Data
                        };
                        var cdrData = callDetailResult.Data.FirstOrDefault();

                        var saveRecordDetails = await _caseManagementFactory.FlyFoneEndOutboundCallAsync(requestToSaveDetail);

                        if (saveRecordDetails)
                        {
                            if (cdrData != null)
                            {
                                return new FormattedFlyFoneCdrUdt()
                                {
                                    ExtTeam = cdrData.ExtTeam,
                                    ExtNumber = cdrData.ExtNumber,
                                    ExtName = cdrData.ExtName,
                                    CallDate = cdrData.CallDate,
                                    CallDisposition = cdrData.CallDisposition,
                                    CalledNumber = cdrData.CalledNumber,
                                    CallingCode = cdrData.CallingCode,
                                    CallRecording = cdrData.CallRecording,
                                    CallRoute = cdrData.CallRoute,
                                    CmsUser = cdrData.CmsUser,
                                    Duration = cdrData.Duration,
                                };
                            }
                            else
                            {
                                return Enumerable.Empty<FormattedFlyFoneCdrUdt>().FirstOrDefault();
                            }
                        }
                        else
                        {
                            return Enumerable.Empty<FormattedFlyFoneCdrUdt>().FirstOrDefault();
                        }
                    }
                    else
                    {
                        return Enumerable.Empty<FormattedFlyFoneCdrUdt>().FirstOrDefault();

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | FlyFoneEndOutboundCallAsync Exception: {ex.InnerException}");
                return Enumerable.Empty<FormattedFlyFoneCdrUdt>().FirstOrDefault();
            }
        }

        public async Task<bool> FlyFoneFetchDetailRecordsAsync(FlyFoneFetchCallDetailRecordRequestModel request)
        {
            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                    var flyFoneSettings = await _caseManagementFactory.GetFlyFoneAppSettingsAsync();

                    _httpClient.BaseAddress = new Uri(flyFoneSettings.BaseUrl);

                    var formatedDateToday = DateTime.Now.ToString("yyyy-MM-dd");
                    var fetchCallDetailRecords = await _httpClient.GetFromJsonAsync<FlyFoneCallDetailRecordApiResponse>($"cdr.php?secretCode=M88@CDRapi&fromDate={formatedDateToday}&toDate={formatedDateToday}");

                    var requestRecords = new FlyFoneSaveFetchCallDetailRecordRequestModel()
                    {
                        FlyFoneCallDetailRecordType = fetchCallDetailRecords.Data,
                        UserId = request.UserId
                    };

                    var saveFetchedRecords = await _caseManagementFactory.FlyFoneFetchDetailRecordsAsync(requestRecords);

                    return saveFetchedRecords;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | FlyFoneFetchDetailRecordsAsync Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<CloudTalkMakeACallWithApiResponseModel> CloudTalkMakeACallAsync(CloudTalkMakeACallRequestModel request)
        {
            try
            {
                var recordedParameter = await _caseManagementFactory.CloudTalkMakeACallAsync(request);

                if (recordedParameter.MobileNumber.Equals('0'))
                {
                    return Enumerable.Empty<CloudTalkMakeACallWithApiResponseModel>().FirstOrDefault();
                }
                else
                {
                    try
                    {
                        var cloudTalkUri = _configuration.GetConnectionString("CloudTalkUri");
                        var cloudTalkUserName = _configuration.GetConnectionString("CloudTalkUserName");
                        var cloudTalkPassword = _configuration.GetConnectionString("CloudTalkPassword");

                        using (HttpClient _cloudTalkHttpClient = new HttpClient())
                        {
                            string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{cloudTalkUserName}:{cloudTalkPassword}"));

                            // Create the Authorization header value with Basic Authentication
                            string authValue = $"Basic {base64Credentials}";
                            _cloudTalkHttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                            _cloudTalkHttpClient.DefaultRequestHeaders.Add("Authorization", authValue);

                            var validatedMobileRequest = recordedParameter.MobileNumber.ToString()[0] == '+' ? recordedParameter.MobileNumber.ToString() : '+' + recordedParameter.MobileNumber.ToString();

                            CloudTalkMakeACallApiRequest cloudTalkRequest = new CloudTalkMakeACallApiRequest() { AgentId = request.AgentId, CalleeNumber = validatedMobileRequest };
                            HttpResponseMessage cloudTalkCallResult = await _cloudTalkHttpClient.PostAsJsonAsync(cloudTalkUri + "create.json", cloudTalkRequest);

                            _logger.LogInfo($"CaseManagementService | CloudTalkMakeACallAsync | CloudTalkMakeACallParameter={JsonConvert.SerializeObject(cloudTalkRequest)} Response={JsonConvert.SerializeObject(cloudTalkCallResult)}");
                           
                            if (cloudTalkCallResult.IsSuccessStatusCode)
                            {
                                var callResponse = await cloudTalkCallResult.Content.ReadAsStringAsync();
                                var callResponseToJson = JsonSerializer.Deserialize<CloudTalkApiMakeACallApiResponse>(callResponse);
                                return new CloudTalkMakeACallWithApiResponseModel()
                                {
                                    DialId = recordedParameter.DialId,
                                    Message = callResponseToJson.ResponseData.Message,
                                    Status = callResponseToJson.ResponseData.Status,
                                };
                            }
                            else
                            {
                                _logger.LogError($"CaseManagementService | CloudTalkMakeACallAsync Vendor Api: {cloudTalkCallResult.ReasonPhrase}");
                                return Enumerable.Empty<CloudTalkMakeACallWithApiResponseModel>().FirstOrDefault();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"CaseManagementService | CloudTalkMakeACallAsync Vendor Api: {ex.InnerException}");
                        return Enumerable.Empty<CloudTalkMakeACallWithApiResponseModel>().FirstOrDefault();
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | CloudTalkMakeACallAsync Exception: {ex.InnerException}");
                return Enumerable.Empty<CloudTalkMakeACallWithApiResponseModel>().FirstOrDefault();
            }
        }

        public async Task<CloudTalkCdrResponseModel> CloudTalkGetCallAsync(CloudTalkGetCallRequestModel request)
        {
            try
            {
                var cloudTalkUrl = _configuration.GetConnectionString("CloudTalkUri");
                var cloudTalkUser = _configuration.GetConnectionString("CloudTalkUserName");
                var cloudTalkPass = _configuration.GetConnectionString("CloudTalkPassword");

                using (HttpClient _cloudTalkHttpClient = new HttpClient())
                {
                    string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{cloudTalkUser}:{cloudTalkPass}"));

                    // Create the Authorization header value with Basic Authentication
                    string authValue = $"Basic {base64Credentials}";
                    _cloudTalkHttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    _cloudTalkHttpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                    DateTime startTimeOfDay = DateTime.Today;
                    DateTime endTimeOfDay = DateTime.Today.AddDays(1).AddTicks(-1);

                    var requestUri = $"{cloudTalkUrl}index.json?date_from={startTimeOfDay.ToString("yyyy-MM-dd HH:mm:ss")}&date_to={endTimeOfDay.ToString("yyyy-MM-dd HH:mm:ss")}&user_id={request.AgentId}";

                    var cloudTalkCallResult = await _cloudTalkHttpClient.GetFromJsonAsync<CloudTalkGetCallApiResponseModel>(requestUri);
                    _logger.LogInfo($"CaseManagementService | CloudTalkGetCallAsync | CloudTalkGetCallParameter={JsonConvert.SerializeObject(requestUri)} Response={JsonConvert.SerializeObject(cloudTalkCallResult)}");
                  
                    if (cloudTalkCallResult.ResponseData.ItemsCount != 0)
                    {
                        DateTime startTime;
                        DateTime newAnswerTime;
                        DateTime endTime;

                        var notes = cloudTalkCallResult.ResponseData?.Data?.Find(t => t.Notes?.FirstOrDefault()?.Note == request.DialId);
                        
                        var startedAt = notes != null && notes.Cdr != null ? notes.Cdr.StartedAt.ToUniversalTime().AddHours(8).ToString("dd-MM-yyyy HH:mm:ss") : DateTime.Now.ToString();
                        var endedAt = notes != null && notes.Cdr != null ? notes.Cdr.EndedAt.ToUniversalTime().AddHours(8).ToString("dd-MM-yyyy HH:mm:ss") : DateTime.Now.ToString();
                        var answeredAt = notes != null && notes.Cdr != null ? notes.Cdr.AnsweredAt.ToUniversalTime().AddHours(8).ToString("dd-MM-yyyy HH:mm:ss") : DateTime.Now.ToString();

                        DateTime.TryParseExact(startedAt,
                               "dd-MM-yyyy HH:mm:ss",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out startTime);
                        DateTime.TryParseExact(answeredAt, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out newAnswerTime);
                        DateTime.TryParseExact(endedAt,
                              "dd-MM-yyyy HH:mm:ss",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out endTime);

                        if (notes != null)
                        {
                            _logger.LogInfo($"CaseManagementService | CloudTalkGetCallAsync | End {notes}");

                            var requestToUpdateCdr = new UpdateCloudTalkCdrByCallingCodeRequestModel
                            {
                                Billsec = Int32.Parse(notes.Cdr.Billsec),
                                CallId = Int32.Parse(notes.Cdr.Id),
                                CallingCode = request.DialId,
                                CloudTalkUserId = Int32.Parse(notes.Cdr.UserId),
                                PublicInternal = notes.Cdr.PublicInternal,
                                TalkingTime = Int32.Parse(notes.Cdr.TalkingTime),
                                Type = notes.Cdr.Type,
                                UserId = request.UserId,
                                AnsweredAt = newAnswerTime,
                                EndedAt = endTime,
                                RecordingLink = notes.Cdr.RecordingLink,
                                StartedAt = startTime,
                                WaitingTime = notes.Cdr.WaitingTime,
                            };

                            await _caseManagementFactory.UpdateCloudTalkCdrByCallingCodeAsync(requestToUpdateCdr);
                        } else
                        {
                            _logger.LogInfo($"CaseManagementService | CloudTalkGetCallAsync | End Notes is NULL");
                            return Enumerable.Empty<CloudTalkCdrResponseModel>().FirstOrDefault();
                        }
                        
                        return new CloudTalkCdrResponseModel()
                        {
                            StartedAt = startTime,
                            EndedAt = endTime,
                            RecordingLink = notes.Cdr.RecordingLink
                        };
                    }
                    else
                    {
                        return Enumerable.Empty<CloudTalkCdrResponseModel>().FirstOrDefault();
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | CloudTalkGetCallAsync Vendor Api: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
                return Enumerable.Empty<CloudTalkCdrResponseModel>().FirstOrDefault();
            }
        }

        #region Communication Review
        public async Task<CommunicationReviewLookupsResponseModel> GetCommunicationReviewLookupsAsync()
        {
            var results = await _caseManagementFactory.GetCommunicationReviewLookupsAsync();
            return new CommunicationReviewLookupsResponseModel
            {
                QualityReviewMeasurementList = results.QualityReviewMeasurements,
                QualityReviewBenchmarkList = results.QualityReviewBenchmarks,
                QualityReviewPeriodList = results.QualityReviewPeriods,
                QualityReviewPeriodOptions = results.CommunicationReviewFieldLookups.Where(x => x.FieldName == "QualityReviewPeriod").Select(x => new LookupModel { Value = x.Value, Label = x.Label }).ToList(),
                QualityReviewRankingOptions = results.CommunicationReviewFieldLookups.Where(x => x.FieldName == "QualityReviewRanking").Select(x => new LookupModel { Value = x.Value, Label = x.Label }).ToList(),
                CommunicationReviewStatus = results.CommunicationReviewFieldLookups.Where(x => x.FieldName == "CommunicationReviewStatus").Select(x => new LookupModel { Value = x.Value, Label = x.Label }).ToList(),
                CommunicationReviewEvent = results.CommunicationReviewFieldLookups.Where(x => x.FieldName == "CommunicationReviewEvent").Select(x => new LookupModel { Value = x.Value, Label = x.Label }).ToList(),
                QualityReviewMeasurementType = results.CommunicationReviewFieldLookups.Where(x => x.FieldName == "QualityReviewMeasurementType").Select(x => new LookupModel { Value = x.Value, Label = x.Label }).ToList(),
                QualityReviewLimit = results.CommunicationReviewFieldLookups.Where(x => x.FieldName == "QualityReviewLimit").Select(x => x.Value).FirstOrDefault(),
            };
        }

        public async Task<bool> ValidateCommunicationReviewLimitAsync(CommunicationReviewLimitRequestModel request)
        {
            return await _caseManagementFactory.ValidateCommunicationReviewLimitAsync(request);
        }

        public async Task<bool> InsertCommunicationReviewEventLogAsync(CommunicationReviewEventLogRequestModel request)
        {
            return await _caseManagementFactory.InsertCommunicationReviewEventLogAsync(request);
        }

        public async Task<List<CommunicationReviewCriteriaResponseModel>> GetCriteriaListByMeasurementId (int? measurementId)
        {
            var results = await _caseManagementFactory.GetCriteriaListByMeasurementId(measurementId);
            return results;
        }

        public async Task<List<CommunicationReviewEventLogRequestModel>> GetCommunicationReviewEventLogAsync(int caseCommunicationId)
        {
          return await _caseManagementFactory.GetCommunicationReviewEventLogAsync(caseCommunicationId);
        }

        public async Task<bool> UpdateCommReviewPrimaryTaggingAsync(UpdateCommReviewTaggingModel request)
        {
            return await _caseManagementFactory.UpdateCommReviewPrimaryTaggingAsync(request);
        }

        public async Task<bool> RemoveCommReviewPrimaryTaggingAsync(RemoveCommReviewPrimaryTaggingModel request)
        {
            return await _caseManagementFactory.RemoveCommReviewPrimaryTaggingAsync(request);
        }

        public async Task<UpsertCaseResponse> UpSertCustomerServiceCaseCommunicationAsync(AddCustomerServiceCaseCommunicationRequest request)
        {

            try
            {
                _logger.LogInfo($"CaseManagementService | UpSertCustomerServiceCaseCommunicationAsync - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
                return await _caseManagementFactory.UpSertCustomerServiceCaseCommunicationAsync(request);
            }
            catch (Exception ex)
            {

                _logger.LogError($"CaseManagementService | UpSertCustomerServiceCaseCommunicationAsync: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
                return new UpsertCaseResponse();
            }

        }
        #endregion

        private async Task<SubscriptionReferenceResponseModel> FabricateSameSpaceCredentials (int _subscription)
        {
            int _sameSpaceAppConfigId = 355;
            string _subscriptionParent = "406";

            try
            {
                _logger.LogInfo($"CaseManagementService | parameter: {_subscription},{_sameSpaceAppConfigId} | _FabricateSameSpaceCredentials");
                List<MasterReferenceModel> _getListSpaceId = await _systemService.GetMasterReferenceList(_subscriptionParent);
                MasterReferenceModel _getSpaceId = _getListSpaceId.Find(obj => obj.MasterReferenceId == _subscription);

                List<GetAppConfigSettingByApplicationIdResponseModel> _configList = await _systemService.GetAppConfigSettingByApplicationIdAsync(_sameSpaceAppConfigId);
                GetAppConfigSettingByApplicationIdResponseModel _configUri = _configList.Find(obj => obj.Key == _getSpaceId.MasterReferenceChildName + "Url");
                GetAppConfigSettingByApplicationIdResponseModel _configKey = _configList.Find(obj => obj.Key == _getSpaceId.MasterReferenceChildName + "Key");

                string __keyVal = AesDecryption.AesDecryptedFromBase64(_configKey.Value, !string.IsNullOrWhiteSpace(_appSetting.Value.PrivateKey) ? _appSetting.Value.PrivateKey : string.Empty, !string.IsNullOrWhiteSpace(_appSetting.Value.SaltKey) ? _appSetting.Value.SaltKey : string.Empty);

                return new SubscriptionReferenceResponseModel()
                {
                    SubscriptionKey = __keyVal,
                    SubscriptionUri = _configUri.Value
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | parameter: {_subscription},{_sameSpaceAppConfigId} | _FabricateSameSpaceCredentials Exception: {ex}");
                return new SubscriptionReferenceResponseModel();
            }
        }

        #region Samespace VoIP Integration

        public async Task<SamespaceMakeACallWithApiResponseModel> SamespaceMakeACallAsync(SamespaceMakeACallRequestModel request)
        {
            try
            {
                var recordedParameter = await _caseManagementFactory.SamespaceMakeACallAsync(request);

                // get the api reference
                SubscriptionReferenceResponseModel _apiRef = await FabricateSameSpaceCredentials(request.SubscriptionId);

                if (_apiRef.SubscriptionKey == "" || _apiRef.SubscriptionUri == "")
                {
                    _logger.LogError($"CaseManagementService | SamespaceMakeACallAsync | Error : Invalid Appconfig Settings For Samespace Subscription {request.SubscriptionId}");
                    return new SamespaceMakeACallWithApiResponseModel()
                    {
                        CallId = "",
                        Message = "Invalid Configuration Setting from Mlab",
                        Success = false,
                    };
                  
                }

                if (recordedParameter.MobileNumber.Equals('0'))
                {
                    return Enumerable.Empty<SamespaceMakeACallWithApiResponseModel>().FirstOrDefault();
                }
                else
                {
                    try
                    {
                        string _sameSpaceMakeCallUrl = _apiRef.SubscriptionUri + "/apiv1/make_call";
                        string _samespaceAPIKey = _apiRef.SubscriptionKey;

                        using (HttpClient _samespaceHttpClient = new HttpClient())
                        {
                            string authValue = $"Bearer {_samespaceAPIKey}";

                            // Create the Authorization header value with Basic Authentication
                            _samespaceHttpClient.BaseAddress = new Uri(_sameSpaceMakeCallUrl);
                            _samespaceHttpClient.DefaultRequestHeaders.Accept.Clear();
                            _samespaceHttpClient.DefaultRequestHeaders.Add("Authorization", authValue);

                            Custom customDialId = new Custom()
                            {
                                DialId = recordedParameter.DialId
                            };

                            SamespaceMakeACallApiRequest sameSpaceRequest = new SamespaceMakeACallApiRequest()
                            {
                                Username = recordedParameter.AgentId,
                                Number = Regex.Unescape(recordedParameter.MobileNumber),
                                Custom = customDialId
                            };

                            // added option to fix issue in unicode + sign
                            var options = new JsonSerializerOptions
                            {
                                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                            };


                            var jsonSerialize = JsonSerializer.Serialize(sameSpaceRequest, options);
                            var content = new StringContent(jsonSerialize, Encoding.UTF8, "application/json");

                            _logger.LogInfo($"CaseManagementService | SamespaceMakeACallAsync | Content: {jsonSerialize}");

                            HttpResponseMessage sameSpaceCallResult = await _samespaceHttpClient.PostAsync(_samespaceHttpClient.BaseAddress, content);

                            if (sameSpaceCallResult.IsSuccessStatusCode)
                            {
                                var callResponse = await sameSpaceCallResult.Content.ReadAsStringAsync();
                                var callResponseToJson = JsonSerializer.Deserialize<SamespaceMakeACallResponseData>(callResponse);

                                _logger.LogInfo($"CaseManagementService | SamespaceMakeACallAsync | CallingSuccess={recordedParameter} Response={JsonConvert.SerializeObject(callResponseToJson)}");

                                return new SamespaceMakeACallWithApiResponseModel()
                                {
                                    CallId = recordedParameter.DialId,
                                    Message = callResponseToJson.Message,
                                    Success = callResponseToJson.Success,
                                };
                            }

                            else
                            {
                                _logger.LogError($"CaseManagementService | SamespaceMakeACallAsync Vendor Api: {sameSpaceCallResult.ReasonPhrase}, Request={JsonConvert.SerializeObject(request)}");
                                return Enumerable.Empty<SamespaceMakeACallWithApiResponseModel>().FirstOrDefault();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"CaseManagementService | SamespaceMakeACallAsync Vendor Api: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
                        return Enumerable.Empty<SamespaceMakeACallWithApiResponseModel>().FirstOrDefault();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | SamespaceMakeACallAsync Exception: {ex.InnerException}");
                return Enumerable.Empty<SamespaceMakeACallWithApiResponseModel>().FirstOrDefault();
            }
        }
        public async Task<SamespaceGetCallResponseModel> SamespaceGetCallAsync(SamespaceGetCallRequestModel request)
        {
            try
            {

                // get the api reference
                SubscriptionReferenceResponseModel _apiRef = await FabricateSameSpaceCredentials(request.SubscriptionId);

                if (_apiRef.SubscriptionKey == null && _apiRef.SubscriptionUri == null)
                {
                    _logger.LogError($"CaseManagementService | SamespaceGetCallAsync | Error : Invalid Appconfig Settings For Samespace Subscription {request.SubscriptionId}");
                    return new SamespaceGetCallResponseModel();
                }

                var getCallResponse = Enumerable.Empty<SamespaceGetCallResponseModel>().FirstOrDefault();

                string samespaceClusterUrl = _apiRef.SubscriptionUri;
                string samespaceBaseUrl = _apiRef.SubscriptionUri+ "/manage/api/analytics/sessions";
                string samepspaceAPIKey = _apiRef.SubscriptionKey;

                using (HttpClient samespaceClient = new HttpClient())
                {
                    samespaceClient.BaseAddress = new Uri(samespaceBaseUrl);
                    samespaceClient.DefaultRequestHeaders.Accept.Clear();
                    samespaceClient.DefaultRequestHeaders.Add("x-api-key", samepspaceAPIKey);

                    var jsonRequest = SamespaceFilterRequest();
                    var jsonSerialize = JsonSerializer.Serialize(jsonRequest);
                    var content = new StringContent(jsonSerialize, Encoding.UTF8, "application/json");

                    _logger.LogInfo($"CaseManagementService | SamespaceGetCallAsync | Content: {jsonSerialize}");

                    HttpResponseMessage response = await samespaceClient.PostAsync(samespaceClient.BaseAddress, content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && result != null)
                    {
                        var responseData = JsonSerializer.Deserialize<SamespaceCdrApiResponseModel>(result);
                        var cdrData = responseData.Data.Find(x => x.CustomData.DialId == request.DialId);

                        if (cdrData != null)
                        {
                            _logger.LogInfo($"CaseManagementService | SamespaceGetCallAsync | Fetching CDR Success, Dial Id Found. Request={JsonConvert.SerializeObject(request)}");
                            DateTime newStartTime;
                            DateTime newAnswerTime;
                            DateTime newEndTime; 
                            var datetimeFormat = "dd-MM-yyyy HH:mm:ss";

                            var startTime = cdrData.StartTime.ToUniversalTime().AddHours(8).ToString(datetimeFormat);
                            var answerTime = cdrData.AnswerTime != null ? cdrData.AnswerTime?.ToUniversalTime().AddHours(8).ToString(datetimeFormat) : cdrData.AnswerTime.ToString();
                            var endTime = cdrData.EndTime.ToUniversalTime().AddHours(8).ToString(datetimeFormat);

                            DateTime.TryParseExact(startTime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out newStartTime);
                            DateTime.TryParseExact(answerTime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out newAnswerTime);
                            DateTime.TryParseExact(endTime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out newEndTime);

                            var updateCdrRequest = new UpdateSamespaceCdrByCallingCodeRequestModel
                            {
                                CallingCode = cdrData.CustomData.DialId,
                                SamespaceId = cdrData.UUID,
                                CallerNumber = cdrData.Caller,
                                TeamSystemId = cdrData.TeamSystemId,
                                TeamName = cdrData.TeamName,
                                UserSystemId = cdrData.UserSystemId,
                                UserDisplayName = cdrData.UserDisplayName,
                                UserLoginId = cdrData.UserLoginId,
                                StartTime = newStartTime,
                                AnswerTime = newAnswerTime,
                                EndTime = newEndTime,
                                Duration = cdrData.Duration,
                                Status = cdrData.Status,
                                TerminatedBy = cdrData.TerminatedBy,
                                TerminatedCause = cdrData.TerminatedCause,
                                RecordingFilename = cdrData.RecordingFilename,
                                RecordingURL = cdrData.RecordingURL,
                                Type = cdrData.Type,
                                SpaceId = cdrData.SpaceId,
                                Direction = cdrData.Direction,
                                DomainId = cdrData.DomainId,
                                Channel = cdrData.Channel,
                                Notes = cdrData.Notes
                            };

                            var updateResult = await _caseManagementFactory.UpdateSamespaceCdrByCallingCodeAsync(updateCdrRequest, request.UserId);

                            if (updateResult)
                            {
                                getCallResponse = new SamespaceGetCallResponseModel()
                                {
                                    RecordingURL = samespaceClusterUrl + cdrData.RecordingURL,
                                    StartTime = newStartTime,
                                    EndTime = newEndTime,
                                    Status = cdrData.Status,
                                };

                                _logger.LogInfo($"CaseManagementService | SamespaceGetCallAsync | UpdateSuccess={updateResult} Response={JsonConvert.SerializeObject(getCallResponse)}");
                            }
                            else
                            {
                                _logger.LogInfo($"CaseManagementService | SamespaceGetCallAsync | Updating CDR in DB Failed. Response={result}");
                                return Enumerable.Empty<SamespaceGetCallResponseModel>().FirstOrDefault();
                            }
                        } 
                        else
                        {
                            _logger.LogInfo($"CaseManagementService | SamespaceGetCallAsync | Fetching CDR Success, Dial Id Not Found. Request={JsonConvert.SerializeObject(request)}");
                        }
                    }
                    else
                    {
                        _logger.LogInfo($"CaseManagementService | SamespaceGetCallAsync | Fetching CDR Failed. Response={result}");
                    }

                    return getCallResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CaseManagementService | SamespaceGetCallAsync Vendor Api: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
                return Enumerable.Empty<SamespaceGetCallResponseModel>().FirstOrDefault();
            }
        }
        protected static object SamespaceFilterRequest()
        {
            DateTime startTimeOfDay = DateTime.Today;
            DateTime endTimeOfDay = DateTime.Today.AddDays(1).AddTicks(-1);

            return new 
            {
                filters = new
                {
                    start_time = startTimeOfDay,
                    end_time = endTimeOfDay,
                    status = "answered"
                }
            };
        }
        #endregion

        #region Case and Communication
        public async Task<List<CaseCommunicationAnnotationModel>> GetCaseCommunicationAnnotationByCaseCommunicationIdAsync(long caseCommunicationId)
        {
            return await _caseManagementFactory.GetCaseCommunicationAnnotationByCaseCommunicationIdAsync(caseCommunicationId);
        }

        public async Task<bool> UpsertCaseCommunicationAnnotationAsync(CaseCommAnnotationRequestModel request)
        {
            return await _caseManagementFactory.UpsertCaseCommunicationAnnotationAsync(request);
        }

        public async Task<int> ValidateCaseCommunicationAnnotationAsync(long caseCommunicationId, string contentBefore, string contentAfter)
        {
            return await _caseManagementFactory.ValidateCaseCommunicationAnnotationAsync(caseCommunicationId, contentBefore,  contentAfter);
        }

        public async Task<List<LookupModel>> GetCampaignByCaseTypeId(int caseTypeId)
        {
            return await _caseManagementFactory.GetCampaignByCaseTypeId(caseTypeId);
        }
        #endregion

        public async Task<List<CampaignOptionModel>> GetEditCustomerServiceCaseCampainNameByUsername(string username, long brandId)
        {
            return await _caseManagementFactory.GetEditCustomerServiceCaseCampainNameByUsername(username, brandId);
        }

        public async Task<CustomerCaseChatStatisticsModel> GetChatStatisticsByCommunicationIdAsync(long communicationId)
        {

            var result = await _caseManagementFactory.GetChatStatisticsByCommunicationIdAsync(communicationId);
            if (result != null)
            {
                return new CustomerCaseChatStatisticsModel
                {
                    chatStatisticsCaseCommDetailsModel = result.chatStatisticsCaseCommDetailsModel,
                    chatInformationModel = result.chatInformationModel,
                    chatAbandonmentModel = result.chatAbandonmentModel,
                    chatAgentSegmentModel = result.chatAgentSegmentModel,
                    chatStatisticsAgentSegmentRecordCountModel = result.chatStatisticsAgentSegmentRecordCountModel,
                    chatStatisticsSkillSegmentModel = result.chatStatisticsSkillSegmentModel,
                    chatStatisticsSkillSegmentRecordCountModel = result.chatStatisticsSkillSegmentRecordCountModel,
                };
            }

            return null;

        }

    }
}

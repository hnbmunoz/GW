using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.ChatBot;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories
{
    public class ChatbotFactory : IChatbotFactory
    {
        private readonly IMainDbFactory _mainDbFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChatbotFactory> _logger;

        #region Constructor
        public ChatbotFactory(IMainDbFactory mainDbFactory,IConfiguration configuration, ILogger<ChatbotFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _mainDbFactory = mainDbFactory;
        }
        #endregion

        public async Task<CaseAndPlayerInformationResponse> GetCaseAndPlayerInformationByParamAsync(CaseAndPlayerInformationRequest request)
        {
            try
            {
                _logger.LogInfo($"{Factories.ChatbotFactory} | GetCaseAndPlayerInformationByParamAsync - {JsonConvert.SerializeObject(request)}");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<CaseAndPlayerInformationResponse>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_ChatBotGetCaseAndPlayerInformationByParam, new
                                    {
                                        ConversationId = String.IsNullOrWhiteSpace(request.ConversationId) ? null : request.ConversationId,
                                        ProviderId = String.IsNullOrWhiteSpace(request.ProviderId) ? null : request.ProviderId,
                                        License = String.IsNullOrWhiteSpace(request.License) ? null : request.License,
                                        Skill = String.IsNullOrWhiteSpace(request.Skill) ? null : request.Skill,
                                        BrandName = String.IsNullOrWhiteSpace(request.BrandName) ? null : request.BrandName
                                    }

                                ).ConfigureAwait(false);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ChatbotFactory} | GetCaseAndPlayerInformationByParamAsync : [Exception] - {ex.Message}");
            }
            return new CaseAndPlayerInformationResponse();
        }

        public async Task<PlayerTransactionResponse> GetPlayerTransactionDataByParamAsync(PlayerTransactionRequest request)
        {
            try
            {
                _logger.LogInfo($"{Factories.ChatbotFactory} | GetPlayerTransactionDataByParamAsync - {JsonConvert.SerializeObject(request)}");

                var response = new PlayerTransactionResponse()
                {
                    ErrorCode = 200,
                    Errormessage = "Ok",
                    Status = "Success",
                    Transactions = Enumerable.Empty<Transaction>().ToList()
                };

                var result = await _mainDbFactory
                            .ExecuteQueryMultipleAsync<PlayerTransactionResponse, Transaction>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_ChatBotGetPlayerTransactionDataByParam, new
                                    {
                                        Username = String.IsNullOrWhiteSpace(request.Username) ? null : request.Username,
                                        BrandName = String.IsNullOrWhiteSpace(request.BrandName) ? null : request.BrandName,
                                        PlayerId = String.IsNullOrWhiteSpace(request.PlayerID) ? null : request.PlayerID
                                    }

                                ).ConfigureAwait(false);
                
                response = result.Item1.FirstOrDefault();
                if(response != null) 
                    response.Transactions = (result.Item2.Count() == 0) ? Enumerable.Empty<Transaction>().ToList() : result.Item2.ToList();

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ChatbotFactory} | GetPlayerTransactionDataByParamAsync : [Exception] - {ex.Message}");
            }
            return new PlayerTransactionResponse();
        }

        public async Task<List<SubTopicResponse>> GetSubTopicAsync(int topicID, string currency, string language)
        {
            try
            {
                _logger.LogInfo($"{Factories.ChatbotFactory} | GetSubTopicAsync - {JsonConvert.SerializeObject(new { TopicId = topicID, Currency = currency, Language = language})}");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<SubTopicResponse>
                                (
                                    DatabaseFactories.MLabDB,
                                    StoredProcedures.USP_ChatBotGetSubTopic, new
                                    {
                                        TopicId = topicID,
                                         Currency = currency,
                                         language = language
                                    }
                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ChatbotFactory} | GetSubTopicAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<SubTopicResponse>().ToList();
        }

        public async Task<List<TopicResponse>> GetTopicAsync(string currency, string language)
        {
            try
            {
                _logger.LogInfo($"{Factories.ChatbotFactory} | GetTopicAsync - {JsonConvert.SerializeObject(new { Currency = currency, Language = language })}");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<TopicResponse>
                                (   DatabaseFactories.MLabDB,
                                    StoredProcedures.USP_ChatBotGetTopic, new
                                    {
                                        Currency = currency,
                                        Language = language
                                    }
                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ChatbotFactory} | GetTopicAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<TopicResponse>().ToList();
        }

        public async Task<ChatbotStatusResponse> SetCaseStatusAsync(SetStatusRequest request, long? userId)
        {
            try
            {

                _logger.LogInfo($" {Factories.ChatbotFactory} | SetCaseStatus - {JsonConvert.SerializeObject(new { request = request, userId = userId })}");

                var response = new ChatbotStatusResponse();

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<ChatbotStatusResponse>
                                (   DatabaseFactories.MLabDB,
                                    StoredProcedures.USP_ChatBotChangeStatus, new
                                    {
                                        ConversationID = request.conversationID,
                                        CaseinformationId = request.caseID,
                                        NewStatus = request.newStatus,
                                        UserId = userId,
                                    }
                                ).ConfigureAwait(false);

                response = (from x in result
                            select new ChatbotStatusResponse()
                            {
                                Status = x.Status,
                                ErrorMessage = x.ErrorMessage,
                                ErrorCode = x.ErrorCode,
                                CaseId = x.CaseId,
                                CommunicationId = x.CommunicationId,
                                ExternalCommunicationId = x.ExternalCommunicationId,
                                CaseDateCreated = x.CaseDateCreated,
                                CaseDateModified = x.CaseDateModified,

                                CaseStatus = x.CaseStatus,
                                CaseCreatorUserId = x.CaseCreatorUserId,
                                CaseCreatorUserName = x.CaseCreatorUserName,

                                CaseModifiedUserId = x.CaseModifiedUserId,
                                CaseModifiedUserName = x.CaseModifiedUserName,


                            }).FirstOrDefault();

                return (result == null) ? new ChatbotStatusResponse() { ErrorCode = 400 } : result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ChatbotFactory} | SetCaseStatus : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<ChatbotStatusResponse>().FirstOrDefault();
        }

        public async Task<ASWDetailResponse> SubmitAswDetail(ASWDetailRequest request)
        {
            try
            {

                _logger.LogInfo($" {Factories.ChatbotFactory} | SubmitAswDetail - {JsonConvert.SerializeObject(request)}");


                var result = await _mainDbFactory
                            .ExecuteQueryAsync<ASWDetailResponse>
                                (   DatabaseFactories.IntegrationDb,
                                    StoredProcedures.USP_ChatBotUpsertAgentSurvey, new
                                    {
                                        ConversationId  = request.ConversationID,
                                        PlayerId = request.PlayerID,
                                        UserName = request.Username,
                                        BrandName = request.BrandName,
                                        CaseId = request.CaseID,
                                        LanguageId = request.LanguageID,
                                        TopicId = request.TopicID,
                                        SubTopicId = request.SubtopicID,
                                        ProviderId = request.ProviderID,
                                        License = request.License,
                                        UserId = request.UserId
                                    }
                                ).ConfigureAwait(false);

                return (result == null) ? new ASWDetailResponse() { ErrorCode=400 } :result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ChatbotFactory} | SubmitAswDetail : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<ASWDetailResponse>().FirstOrDefault();
        }
    }
}

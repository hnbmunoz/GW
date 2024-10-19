using MediatR;
using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models.ChatBot;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Config;

namespace MLAB.PlayerEngagement.Application.Services
{
    public class ChatbotService : IChatbotService
    {
        private readonly ILogger<ChatbotService> _logger;
        private readonly IChatbotFactory _chatbotFactory;

        public ChatbotService(IMediator mediator, ILogger<ChatbotService> logger, IChatbotFactory chatbotFactory, IConfiguration configuration)
        {
            _logger = logger;
            _chatbotFactory = chatbotFactory;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public async Task<CaseAndPlayerInformationResponse> GetCaseAndPlayerInformationByParamAsync(CaseAndPlayerInformationRequest request)
        {
            return await _chatbotFactory.GetCaseAndPlayerInformationByParamAsync(request);
        }

        public async Task<PlayerTransactionResponse> GetPlayerTransactionDataByParamAsync(PlayerTransactionRequest request)
        {
            return await _chatbotFactory.GetPlayerTransactionDataByParamAsync(request);
        }

        public async Task<List<SubTopicResponse>> GetSubTopicAsync(int topicID, string currency, string language)
        {
            return await _chatbotFactory.GetSubTopicAsync(topicID, currency, language);
        }

        public async Task<List<TopicResponse>> GetTopicAsync(string currency, string language)
        {
            return await _chatbotFactory.GetTopicAsync(currency, language);
        }

        public async Task<ChatbotStatusResponse> SetCaseStatusAsync(SetStatusRequest request, long? userId)
        {
            return await _chatbotFactory.SetCaseStatusAsync(request, userId);
        }

        public async Task<ASWDetailResponse> SubmitAswDetail(ASWDetailRequest request)
        {
            return await _chatbotFactory.SubmitAswDetail(request);
        }
    }
}

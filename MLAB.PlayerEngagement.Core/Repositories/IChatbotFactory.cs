using MLAB.PlayerEngagement.Core.Models.ChatBot;

namespace MLAB.PlayerEngagement.Core.Repositories
{
    public interface IChatbotFactory
    {
        Task<CaseAndPlayerInformationResponse> GetCaseAndPlayerInformationByParamAsync(CaseAndPlayerInformationRequest request);
        Task<PlayerTransactionResponse> GetPlayerTransactionDataByParamAsync(PlayerTransactionRequest request);
        Task<List<TopicResponse>> GetTopicAsync(string currency, string language);
        Task<List<SubTopicResponse>> GetSubTopicAsync(int topicID, string currency, string language);
        Task<ChatbotStatusResponse> SetCaseStatusAsync(SetStatusRequest request, long? userId);
        Task<ASWDetailResponse> SubmitAswDetail(ASWDetailRequest request);
    }
}

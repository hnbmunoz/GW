using MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class AddCustomerServiceCaseCommunicationRequest : BaseModel
    {
        public long MlabPlayerId { get; set; }
        public int CaseInformationId { get; set; }
        public int CaseCreatorId { get; set; }
        public int CaseTypeId { get; set; }
        public int CaseStatusId { get; set; }
        public int SubtopicLanguageId { get; set; }
        public int TopicLanguageId { get; set; }
        public string Subject { get; set; }
        public int BrandId { get; set; }
        public int LanguageId { get; set; }
        public CustomerCaseCommunicationRequestModel caseCommunication { get; set; }
        public List<CampaignIds> CampaignIds { get; set; } = new();
    }
}

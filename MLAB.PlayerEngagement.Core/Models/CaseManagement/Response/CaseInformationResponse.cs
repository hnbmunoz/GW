using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models
{
    public class CaseInformationResponse
    {
        public int CaseInformatIonId { get; set; }
        public string PlayerId { get; set; }
        public string Username { get; set; }
        public int CaseCreatorId { get; set; }
        public string CaseCreatorName { get; set; }
        public string Subject { get; set; }
        public string CaseStatusName { get; set; }
        public int CaseTypeId { get; set; }
        public string CaseTypeName { get; set; }
        public int TopicLanguageId { get; set; }
        public string TopicLanguageTranslation { get; set; }
        public int SubtopicLanguageId { get; set; }
        public string SubtopicLanguageTranslation { get; set; }
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string CurrencyCode { get; set; }
        public string VIPLevelName { get; set; }
        public string PaymentGroupName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public string UpdatedDate { get; set; }
        public long MlabPlayerId { get; set; }
        public List<CampaignOptionModel> CampaignList { get; set; }
    }
}

using MLAB.PlayerEngagement.Core.Models.Option;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CustomerCaseModel
    {
        public long CaseInformationId { get; set; }
        public string Brand { get; set; }
        public string CaseType { get; set; }
        public string CaseStatus { get; set; }
        public string CaseOwner { get; set; }
        public string Username { get; set; }
        public long MlabPlayerId { get; set; }
        public string Currency { get; set; }
        public string VipLevel { get; set; }
        public string PaymentGroup { get; set; }
        public string PlayerId { get; set; }
        public string CaseOrigin { get; set; }
        public string CaseCreatedBy { get; set; }
        public string CaseCreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string Subject { get; set; }
        public string LanguageCode { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        public List<CampaignOptionModel> CampaignList { get; set; }
        public string ReportedDate { get; set; }
    }
}

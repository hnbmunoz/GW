namespace MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request
{
    public class CaseCommunicationFilterRequest : BaseModel
    {
        public long BrandId { get; set; }
        public int CaseTypeIds { get; set; }
        public string MessageTypeIds { get; set; }
        public string VipLevelIds { get; set; }
        public string DateByFrom { get; set; }
        public string DateByTo { get; set; }
        public string CaseStatusIds { get; set; }
        public string CommunicationOwners { get; set; }
        public string ExternalId { get; set; }
        public string PlayerIds { get; set; }
        public string Usernames { get; set; }
        public string CaseId { get; set; }
        public string CommunicationId { get; set; }
        public string TopicLanguageIds { get; set; }
        public string SubtopicLanguageIds { get; set; }
        public int Duration { get; set; }
        public int PageSize { get; set; }
        public int OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public long CampaignId { get; set; }
        public long CommunicationOwnerTeamId { get; set; }
        public string CurrencyIds { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public int DateBy { get; set; }
        public string IsLastSkillAbandonedQueue { get; set; }
        public string IsLastAgentAbandonedAssigned { get; set; }
    }
}

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class SearchTicketFilterRequestModel : BaseModel
    {
        public string CreatedDateFrom { get; set; }
        public string CreatedDateTo { get; set; }
        public string TicketType { get; set; }
        public string TicketCode { get; set; }
        public string Summary { get; set; }
        public string PlayerUsername { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
        public string ExternalLinkName { get; set; }
        public string Currency { get; set; }
        public string MethodCurrency { get; set; }
        public string VIPGroup { get; set; }
        public string VIPLevel { get; set; }
        public string UserListTeams { get; set; }
        public string PlatformTransactionId { get; set; }
        public int? CurrentPage { get; set; }
        public int? OffsetValue { get; set; }
        public int? PageSize { get; set; }
        public string CreatorId { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}

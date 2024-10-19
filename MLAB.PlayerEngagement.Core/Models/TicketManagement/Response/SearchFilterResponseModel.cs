namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class SearchFilterResponseModel
    {
        public string TicketSearchFilterName { get; set; }
        public string UserId { get; set; }
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
    }
}

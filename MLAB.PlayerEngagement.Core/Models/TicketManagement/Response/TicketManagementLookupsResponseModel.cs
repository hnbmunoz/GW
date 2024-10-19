namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketManagementLookupsResponseModel
    {
        public List<TicketManagementLookupModel> TicketType { get; set; }        
        public List<TicketManagementLookupModel> Status { get; set; }
        public List<TicketManagementLookupModel> Assignee { get; set; }
        public List<TicketManagementLookupModel> Reporter { get; set; }
        public List<TicketManagementLookupModel> Currency { get; set; }
        public List<TicketManagementLookupModel> MethodCurrency { get; set; }
        public List<TicketManagementLookupModel> VIPGroup { get; set; }
        public List<TicketManagementLookupModel> VIPLevel { get; set; }
        public List<TicketManagementLookupModel> UserListTeams { get; set;  }
    }
}

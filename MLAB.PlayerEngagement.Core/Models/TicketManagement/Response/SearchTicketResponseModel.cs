namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class SearchTicketResponseModel
    {
        public string TicketType { get; set; }
        public string TicketID { get; set; }
        public string TicketTypeSequenceId { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Reporter { get; set; }
        public string Assignee { get; set; }
        public string Currency { get; set; }
        public string VIPGroup { get; set; }
        public string VIPLevel { get; set; }
        public string UserListTeams { get; set; }
        public string Duration { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}

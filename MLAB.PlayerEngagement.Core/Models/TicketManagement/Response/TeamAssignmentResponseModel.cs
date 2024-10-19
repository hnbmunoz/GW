namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TeamAssignmentResponseModel
    {
        public long TicketTeamAssignmentId { get; set; }
        public string TicketTeamAssignmentName { get; set; }
        public long TeamDepartmentId { get; set; }
        public string TeamDepartmentName { get; set; }
    }
}

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class ValidateAddUserAsCollaboratorRequestsModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int TicketId { get; set; }
        public int TIcketTypeId { get; set; }
        public int CreatedBy { get; set; }
    }
}

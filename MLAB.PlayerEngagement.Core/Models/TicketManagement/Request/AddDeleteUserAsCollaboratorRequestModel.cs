namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class AddDeleteUserAsCollaboratorRequestModel
    {
        public int? UserId { get; set; }
        public int TicketId { get; set; }
        public int TicketTypeId { get; set; }
        public int? TicketCollaboratorID { get; set; }

    }
}

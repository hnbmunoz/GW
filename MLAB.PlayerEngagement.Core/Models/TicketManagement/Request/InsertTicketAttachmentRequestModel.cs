
namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class InsertTicketAttachmentRequestModel : BaseModel
    {
        public long TicketId { get; set; }
        public long TicketTypeId { get; set; }
        public long TypeId { get; set; }
        public string Url { get; set; }
    }
}

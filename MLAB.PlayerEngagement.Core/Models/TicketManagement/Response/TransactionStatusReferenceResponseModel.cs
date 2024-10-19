
namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TransactionStatusReferenceResponseModel
    {
        public string TransactionTag { get; set; }
        public long FieldId { get; set; }
        public long StaticReferenceId { get; set; }
        public string StaticReferenceDescription { get; set; }
        public long ApiStatusId { get; set; }
    }
}

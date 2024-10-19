
using System.Security.Principal;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class GetAllPaymentProcessorResponseModel
    {
        public long PaymentProcessorId { get; set; }
        public string PaymentProcessorName { get; set; }
        public long DepartmentId { get; set; }
        public long Verifier {  get; set; }
        public bool IsForSmVerification { get; set; }
        public long ExternalSourceId {  get; set; }
    }
}

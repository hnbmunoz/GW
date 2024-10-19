

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class PaymentMethodHiddenTicketFieldsRequestModel
    {
        public int TicketTypeId { get; set; }
        public long PaymentMethodExtId { get; set; }
        public string PageMode { get; set; }
       
    }
}

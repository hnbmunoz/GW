


namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketThresholdResponseModel
    {
        public long AmountMin { get; set; }
        public long AmountMax { get; set; }
        public bool IsAutoApproved { get; set; }
        public long TicketStatusId { get; set; }
    }
}

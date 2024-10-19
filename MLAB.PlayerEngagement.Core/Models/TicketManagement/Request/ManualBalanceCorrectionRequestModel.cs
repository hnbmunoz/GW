namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class ManualBalanceCorrectionRequestModel
    {
        public long PlayerId { get; set; }
        public int UserId { get; set; }
        public int ManualCorrectionReason { get; set; }
        public decimal Amount { get; set; }
        public string Explanation { get; set; }
        public int WagerMultiplier { get; set; }
    }
}

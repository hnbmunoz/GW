namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class ManualBalanceCorrectionResponseModel
    {
        public string TransactionId { get; set; }
        public int Status { get; set; }
        public string DateTime { get; set; }
        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class HoldWithdrawalResponseModel
    {
        public List<string> NonExistingPlayers { get; set; }
        public int RecordCount { get; set; }
        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

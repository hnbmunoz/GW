namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;

public class PlayerDepositAttemptsResponseModel
{
    public string TransactionId { get; set; }
    public string TransactionStatusName { get; set; }
    public DateTime TransactionDate { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethodExtName { get; set; }
}

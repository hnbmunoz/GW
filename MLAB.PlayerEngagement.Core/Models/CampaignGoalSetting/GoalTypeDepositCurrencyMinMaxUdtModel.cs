namespace MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting;

public class GoalTypeDepositCurrencyMinMaxUdtModel
{
    public long GoalTypeDepositCurrencyMinMaxId { get; set; }
    public long CurrencyId { get; set; }
    public long GoalTypeDepositId { get; set; }
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public Guid? DepositGuid { get; set; }
}

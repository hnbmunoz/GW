namespace MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting;

public class GoalTypeGameActivityCurrencyMinMaxUdtModel
{
    public long GoalTypeGameActivityCurrMinMaxId { get; set; }
    public long CurrencyId { get; set; }
    public long GoalTypeGameActivityId { get; set; }
    public decimal Min { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public Guid? GoalTypeGuid { get; set; }
}

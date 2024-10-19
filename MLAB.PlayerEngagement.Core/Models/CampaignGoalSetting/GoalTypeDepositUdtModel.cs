namespace MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting;

public class GoalTypeDepositUdtModel
{
    public long GoalTypeDepositId { get; set; }
    public int SequenceId { get; set; }
    public long GoalTypeId { get; set; }
    public long CampaignSettingId { get; set; }
    public long GoalTypeTransactionTypeId { get; set; }
    public long GoalTypeDataSourceId { get; set; }
    public long GoalTypePeriodId { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public Guid? DepositGuid { get; set; }
    public string GoalTypeName { get; set; }
    public long IntervalRelationalOperatorId { get; set; }
    public long? IntervalSourceId { get; set; }
    public long IntervalNumber { get; set; }
    public int NthNumber { get; set; }
    public long ThresholdTypeId { get; set; }
    public long IntervalSourceGoalTypeId { get; set; }
    public Guid? IntervalSourceGoalTypeGUID { get; set; }
}

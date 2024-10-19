namespace MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting;

public class GoalTypeGameActivityTypeUdtModel
{
    public long GoalTypeGameActivityId { get; set; }
    public string GoalTypeName { get; set; }
    public long GoalTypeId { get; set; }
    public long CampaignSettingId { get; set; }
    public long GoalTypeTransactionTypeId { get; set; }
    public string GoalTypeProductId { get; set; }
    public long ThresholdTypeId { get; set; }
    public long GoalTypeDataSourceId { get; set; }
    public long GoalTypePeriodId { get; set; }
    public long IntervalRelationalOperatorId { get; set; }
    public long? IntervalSourceId { get; set; }
    public long IntervalNumber { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public long IntervalSourceGoalTypeId { get; set; }
    public Guid? GoalTypeGuid { get; set; }
    public Guid? IntervalSourceGoalTypeGUID { get; set; }

}

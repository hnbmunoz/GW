namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement;

public class CampaignConfigurationModel
{
    public int CampaignConfigurationId { get; set; }
    public int CampaignId { get; set; }
    public int? SegmentId { get; set; }
    public bool IsAutoTag { get; set; }
    public int AutoTaggingId { get; set; }
    public int AgentId { get; set; }
    public int PrimaryGoalId { get; set; }
    public int ValidationRulesId { get; set; }
    public int LeaderValidationId { get; set; }
    public int GoalParameterPointSettingId { get; set; }
    public int PointValueSettingId { get; set; }
    public int EligibilityTypeId { get; set; }
    public long? VarianceId { get; set; }
    public string VarianceName { get; set; }
    public long? InPeriodId { get; set; }
}

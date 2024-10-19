namespace MLAB.PlayerEngagement.Core.Models.CampaignPerformance;

public class CampaignPerformanceRequestModel: BaseModel
{
    public int TimePeriod { get; set; }
    public int PeriodSelection { get; set; }
    public DateTime? DateFieldStart { get; set; }
    public DateTime? DateFieldEnd { get; set; }
    public int CampaignId { get; set; }
    public int CampaignGoalId { get; set; }
    public bool IncludeDiscardPlayerTo { get; set; }
    public int? CampaignTypeId { get; set; }
}

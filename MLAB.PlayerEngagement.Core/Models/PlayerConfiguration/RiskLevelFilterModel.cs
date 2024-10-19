namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class RiskLevelFilterModel : BaseModel
{
    public int? RiskLevelId { get; set; }
    public string RiskLevelName { get; set; }
    public string BrandName { get; set; }
}

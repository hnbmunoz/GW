namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class RiskLevelModel : BaseModel
{
    public int? RiskLevelId { get; set; }
    public string RiskLevelName { get; set; }
    public int? ICoreId { get; set; }
    public List<BrandRiskLevelModel> Brands { get; set; }
}

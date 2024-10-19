namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class RiskLevelRequestModel : BaseModel
{
    public int? PlayerConfigurationId { get; set; }
    public List<VIPLevelModel> VIPLevel { get; set; }
}

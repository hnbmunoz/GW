namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class VIPLevelModel
{
    public int VIPLevelId { get; set; }
    public string VIPLevelName { get; set; }
    public int? PlayerConfigurationId { get; set; }
    List<BrandIdModel> Brands { get; set; }
}

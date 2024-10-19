namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class PlayerConfigDetailsUdtModel : BaseModel
{
    public int? PlayerConfigurationId { get; set; }
    public string PlayerConfigurationCode { get; set; }
    public string PlayerConfigurationName { get; set; }
    public bool IsComplete { get; set; }
    public int? DataSourceId { get; set; }
    public int? Status { get; set; }
    public int? BrandId { get; set; }
    public int? ICoreId { get; set; }
}

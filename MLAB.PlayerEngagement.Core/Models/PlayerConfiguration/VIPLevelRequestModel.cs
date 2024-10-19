namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class VipLevelRequestModel : BaseModel
{
    public int? VIPLevelId { get; set; }
    public string VIPLevelName { get; set; }
    public int? BrandId { get; set; }
    public int? ICoreId { get; set; }
    public int VIPGroupId { get; set; }
}

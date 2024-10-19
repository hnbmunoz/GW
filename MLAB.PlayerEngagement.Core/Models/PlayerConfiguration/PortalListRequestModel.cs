namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class PortalListRequestModel : BaseModel
{
    public int PlayerConfigurationTypeId { get; set; }
    public List<PortalModel> PortalList { get; set; }
}

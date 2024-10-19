namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class PlayerConfigCodeListDetailsRequestModel : BaseModel
{
    public int PlayerConfigurationTypeId { get; set; }
    public List<PlayerConfigDetailsUdtModel> PlayerConfigCodeListDetailsType { get; set; }
}

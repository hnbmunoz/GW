namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class PlayerStatusListRequestModel : BaseModel
{
    public int PlayerConfigurationTypeId { get; set; }
    public List<PlayerStatusModel> PlayerStatusList { get; set; }
}

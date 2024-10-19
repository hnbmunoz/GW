namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class LanguageListRequestModel : BaseModel
{
    public int PlayerConfigurationTypeId { get; set; }
    public List<LanguageModel> LanguageList { get; set; }
}

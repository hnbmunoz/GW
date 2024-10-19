namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class PlayerConfigurationRequestModel : BaseModel
{
    public int PlayerConfigurationTypeId { get; set; }
    public int? PlayerConfigurationId { get; set; }
    public int? PlayerConfigurationICoreId { get; set; }
    public string PlayerConfigurationName { get; set; }
    public string PlayerConfigurationCode { get; set; }
    public int? PageSize { get; set; }
    public int? OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string PlayerConfigurationAction { get; set; }

}

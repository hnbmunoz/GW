namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class PlayerConfigCodeListValidatorRequestModel
{
    public int PlayerConfigurationTypeId { get; set; }
    public int? PlayerConfigurationId { get; set; }
    public string PlayerConfigurationName { get; set; }
    public string PlayerConfigurationCode { get; set; }
    public int? PlayerConfigurationICoreId { get; set; }
    public string PlayerConfigurationAction { get; set; }
    public int? PlayerConfigurationBrandId { get; set; }
}

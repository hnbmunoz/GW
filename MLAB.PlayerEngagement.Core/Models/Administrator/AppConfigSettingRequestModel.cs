namespace MLAB.PlayerEngagement.Core.Models.Administrator;

public class AppConfigSettingRequestModel : BaseModel
{
    public int AppConfigSettingId { get; set; }
    public int ApplicationId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string DataType { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.Administrator;

public class AppConfigSettingFilterRequestModel: BaseModel
{
    public int AppConfigSettingId { get; set; }
    public int? ApplicationId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string DataType { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

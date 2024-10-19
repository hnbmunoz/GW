namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class ScheduleTemplateLanguageRequestModel: BaseModel
{
    public int ScheduleTemplateSettingId { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class ScheduleTemplateListRequestModel: BaseModel
{
    public string ScheduleTemplateName { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Response;

public class ScheduleTemplateResponseModel
{
    public int ScheduleTeplateSettingId { get; set; }
    public string ScheduleTemplateName { get; set; }
    public string ScheduleTemplateDescription { get; set; }
    public int CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public string UpdatedByName { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

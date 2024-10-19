namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class ValidateTemplateRequestModel
{
    public int ScheduleTemplateSettingId { get; set; }
    public string ScheduleTemplateName { get; set; }
    public int IsAdd { get; set; }
}

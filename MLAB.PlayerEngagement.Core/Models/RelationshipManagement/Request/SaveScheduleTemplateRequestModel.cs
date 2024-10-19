using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Udt;

namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class SaveScheduleTemplateRequestModel: BaseModel
{
    public int ScheduleTemplateSettingId { get; set; }
    public string ScheduleTemplateName { get; set; }
    public string ScheduleTemplateDescription { get; set; }
    public List<ScheduleTemplateLanguageTypeUdtModel> ScheduleTemplateLanguageSettingType { get; set; }
    public bool IsDirty { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Udt;

public class ScheduleTemplateLanguageTypeUdtModel
{
    public int ScheduleTemplateLanguageSettingId { get; set; }
    public int ScheduleTemplateSettingId { get; set; }
    public int LanguageId { get; set; }
    public string LanguageLocalTranslation { get; set; }
}

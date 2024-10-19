namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Response;

public class ScheduleTemplateLanguageResponseModel
{
    public int ScheduleTemplateLanguageSettingId { get; set; }
    public int ScheduleTemplateSettingId { get; set; }
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
    public string LanguageLocalTranslation { get; set; }
}

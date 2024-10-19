namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;

public class UserValidationRequest
{
    public string UserName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string SkillName { get; set; } = string.Empty;
    public long? LicenseId { get; set; } 
    public long UserId { get; set; }
}

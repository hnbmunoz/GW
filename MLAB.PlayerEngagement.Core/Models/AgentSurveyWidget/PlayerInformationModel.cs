namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;

public class PlayerInformationModel
{
    public string BrandName { get; set; } = string.Empty;
    public string PlayerId { get; set; }   = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string VIPLevelName { get; set; } = string.Empty;
    public string PaymentGroup { get;set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public bool BonusAbuser { get; set; }
}

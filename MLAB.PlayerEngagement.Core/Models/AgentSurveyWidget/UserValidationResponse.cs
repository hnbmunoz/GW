namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget;

public class UserValidationResponse
{
    public bool IsDataAccessible { get; set; }
    public bool IsValidUser { get; set; }
    public bool IsValidBrand { get; set; }
    public string PlayerCurrency { get; set; } 
    public PlayerInformationModel PlayerInfoDetails { get; set; }   
    public List<PlayerTransactionModel> PlayerTransactionDetails { get; set; }

}

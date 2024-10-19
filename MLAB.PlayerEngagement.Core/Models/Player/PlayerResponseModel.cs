namespace MLAB.PlayerEngagement.Core.Models;

public class PlayerResponseModel
{
    public string PlayerId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Username { get; set; }
    public string Brand { get; set; }
    public string Currency { get; set; }
    public string RegistrationDate { get; set; }
    public string Status { get; set; }
    public string VIPLevel { get; set; }
    public string RiskLevelName { get; set; }
    public string PaymentGroup { get; set; }
    public bool? Deposited { get; set; }
    public string Country { get; set; }
    public string Language { get; set; }
    public bool? BonusAbuser { get; set; }
    public bool? ReceiveBonus { get; set; }
    public bool? InternalAccount { get; set; }
    public string MobilePhone { get; set; }
    public string Email { get; set; }
    public string MarketingChannel { get; set; }
    public string MarketingSource { get; set; }
    public string CampaignName { get; set; }
    public string CouponCode { get; set; }
    public string ReferredBy { get; set; }
    public string ReferrerURL { get; set; }
    public string BTAG { get; set; }
    public bool? BlindAccount { get; set; }
    public bool? IsCensoredMobile { get; set; }
    public bool? IsCensoredEmail { get; set; }
    public long MlabPlayerId { get; set; }
}

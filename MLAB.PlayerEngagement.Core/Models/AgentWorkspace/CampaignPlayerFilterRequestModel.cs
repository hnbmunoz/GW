namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace;

public class CampaignPlayerFilterRequestModel : BaseModel
{
    public int CampaignId { get; set; }
    public int CampaignTypeId { get; set; }
    public int CampaignStatus { get; set; }
    public string AgentId { get; set; }
    public string PlayerId { get; set; } //CSV
    public int PlayerStatus { get; set; }
    public string Username { get; set; } //CSV
    public string MarketingSource { get; set; } //CSV
    public string Currency { get; set; } //CSV, currencyId
    public string RegisteredDateStart { get; set; }
    public string RegisteredDateEnd { get; set; }
    public string LastLoginDateStart { get; set; }
    public string LastLoginDateEnd { get; set; }
    public string Deposited { get; set; } //CSV, YES/NO
    public decimal? FtdAmountFrom { get; set; }
    public decimal? FtdAmountTo { get; set; }
    public string FtdDateStart { get; set; }
    public string FtdDateEnd { get; set; }
    public string TaggedDateStart { get; set; }
    public string TaggedDateEnd { get; set; }
    public string PrimaryGoalReached { get; set; } //CSV, YES,NO
    public int? PrimaryGoalCountFrom { get; set; }
    public int? PrimaryGoalCountTo { get; set; }
    public decimal? PrimaryGoalAmountFrom { get; set; }
    public decimal? PrimaryGoalAmountTo { get; set; }
    public string CallListNotes { get; set; } // with * wildcard search
    public string MobileNumber { get; set; } // with * wildcard search
    public decimal? InitialDepositAmount { get; set; }
    public string InitialDepositDateStart { get; set; }
    public string InitialDepositDateEnd { get; set; }
    public string IntialDepositMethod { get; set; } //CSV, Deposit Methods
    public string InitialDeposited { get; set; } //CSV, YES/NO
    public string MessageResponseAndStatus { get; set; } // CSV
    public string CallCaseCreatedDateStart { get; set; }
    public string CallCaseCreatedDateEnd { get; set; }
    public long MobileVerificationStatusId { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

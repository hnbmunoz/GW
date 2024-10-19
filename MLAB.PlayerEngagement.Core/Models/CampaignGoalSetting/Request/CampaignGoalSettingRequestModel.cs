namespace MLAB.PlayerEngagement.Core.Models.CampaignGoalSetting.Request;

public class CampaignGoalSettingRequestModel: BaseModel
{
    public long CampaignSettingId { get; set; }
    public string CampaignSettingName { get; set; }
    public string CampaignSettingDescription { get; set; }
    public bool IsActive { get; set; }
    public List<GoalTypeCommunicationRecordUdtModel> GoalTypeCommunicationRecordList { get; set; }
    public List<GoalTypeDepositUdtModel> GoalTypeDepositList { get; set; }
    public List<GoalTypeDepositCurrencyMinMaxUdtModel> GoalTypeDepositCurrencyMinMaxList { get; set; }
    public List<GoalTypeGameActivityCurrencyMinMaxUdtModel> GoalTypeGameActivityCurrencyMinMaxList { get; set; }
    public List<GoalTypeGameActivityTypeUdtModel> GoalTypeGameActivityList { get; set; }
    public long GoalParameterAmountId { get; set; }
    public long GoalParameterCountId { get; set; }
    public long CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public long UpdatedBy { get; set; }
    public string UpdatedDate { get; set; }

}

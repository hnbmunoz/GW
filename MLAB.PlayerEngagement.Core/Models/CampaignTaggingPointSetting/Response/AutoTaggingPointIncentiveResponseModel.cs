namespace MLAB.PlayerEngagement.Core.Models;

public class AutoTaggingPointIncentiveResponseModel 
{
    public int CampaignSettingId { get; set; }
    public string CampaignSettingName { get; set; }
    public int? IsActive { get; set; }

    public string SettingStatusName { get; set; }
    public int CampaignSettingTypeId { get; set; }
    public string CampaignSettingTypeName { get; set; }  // identifier : ID: Auto Tagging/Point Incentive Setting/Campaign Goal 

    public int? SettingTypeId { get; set; }
    public string SettingTypeName { get; set; }
    public string CampaignSettingDescription { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public int? RecordCount { get; set; }

    //public int PageSize { get; set; }
    //public int OffsetValue { get; set; }
}

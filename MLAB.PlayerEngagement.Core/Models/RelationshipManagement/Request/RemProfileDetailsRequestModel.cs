using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Udt;

namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;

public class RemProfileDetailsRequestModel : BaseModel
{

    public int RemProfileId { get; set; }
    public string RemProfileName { get; set; }
    public int AgentId { get; set; }
    public string PseudoNamePP { get; set; }
    public int ScheduleTemplateSettingId { get; set; }
    public int OnlineStatusId { get; set; }
    public int AgentConfigStatusId { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public List<RemContactDetailsUDTModel> RemContactDetailsList { get; set; }
    public List<RemLiveChatUDTModel> RemLiveChatList { get; set; }
    public List<RemLivePersonUDTModel> RemLivePersonList { get; set; }
    public bool IsDirty { get; set; }

}

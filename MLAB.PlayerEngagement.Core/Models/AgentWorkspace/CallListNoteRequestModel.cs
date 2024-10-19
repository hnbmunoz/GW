namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace;

public class CallListNoteRequestModel : BaseModel
{
    public int CallListNoteId { get; set; }
    public int CampaignPlayerId { get; set; }
    public string Note { get; set; }
}

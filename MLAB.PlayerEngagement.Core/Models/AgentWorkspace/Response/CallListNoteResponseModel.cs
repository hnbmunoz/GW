namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;

public class CallListNoteResponseModel
{
    public int CallListNoteId { get; set; }
    public int CampaignPlayerId { get; set; }
    public string Note { get; set; }
    public int CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public string UpdatedByName { get; set; }
    public DateTime UpdatedDate { get; set; }
}

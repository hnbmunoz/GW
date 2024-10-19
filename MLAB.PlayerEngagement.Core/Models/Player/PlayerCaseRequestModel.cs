namespace MLAB.PlayerEngagement.Core.Models;

public class  PlayerCaseRequestModel
{
    public long MlabPlayerId { get; set; }
    public string CreatedDate { get; set; }
    public int? CaseId { get; set; }
    public int? CommunicationId { get; set; }
    public int? CaseStatus { get; set; }
    public int? MessageTypeId { get; set; }
    public int? MessageStatusId { get; set; }
    public int? MessageResponseId { get; set; }
    public int? CampaignNameId { get; set; }
    public int? CreatedBy { get; set; }
    public int? CaseTypeId { get; set; }
    public int? CampaignTypeId { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

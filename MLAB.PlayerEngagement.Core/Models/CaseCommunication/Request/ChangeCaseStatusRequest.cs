namespace MLAB.PlayerEngagement.Core.Models;

public class ChangeCaseStatusRequest : BaseModel
{
    public string CaseInformationId { get; set; }
    public int CaseStatusId { get; set; }
}

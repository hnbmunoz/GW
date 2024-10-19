namespace MLAB.PlayerEngagement.Core.Request;

public class OperatorRequest
{
    public int OperatorId { get; set; }
    public string OperatorName { get; set; }
    public int OperatorStatus { get; set; }
    public List<BrandRequest> Brands { get; set; }
    public int CreatedBy { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Response;

public class OperatorResponse
{
    public int OperatorId { get; set; }
    public string OperatorName { get; set; }
    public int OperatorStatus { get; set; }
    public List<BrandResponse> Brands { get; set; }
    public int CreatedBy { get; set; }
}

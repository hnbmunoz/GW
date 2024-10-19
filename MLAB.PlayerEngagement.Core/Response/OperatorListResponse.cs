namespace MLAB.PlayerEngagement.Core.Response;

public class OperatorListResponse
{
    public int OperatorId { get; set; }
    public string OperatorName { get; set; }
    public int OperatorStatus { get; set; }
    public List<BrandListResponse> Brands { get; set; }
}

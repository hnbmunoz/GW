namespace MLAB.PlayerEngagement.Core.Request;

public class BrandRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public List<CurrencyRequest> Currencies { get; set; }
    public int CreateStatus { get; set; }
}

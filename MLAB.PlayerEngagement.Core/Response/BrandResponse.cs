namespace MLAB.PlayerEngagement.Core.Response;

public class BrandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public List<BrandCurrencyResponse> Currencies { get; set; }
    public int CreateStatus { get; set; }
}

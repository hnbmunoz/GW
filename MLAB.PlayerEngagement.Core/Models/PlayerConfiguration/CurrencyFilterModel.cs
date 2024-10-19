namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class CurrencyFilterModel : BaseModel
{
    public int? CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public string CurrencyCode { get; set; }
    public bool? IsComplete { get; set; }
    public int? Status { get; set; }
    public int? ICoreId { get; set; }

}

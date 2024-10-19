namespace MLAB.PlayerEngagement.Core.Models;

public class OperatorBrandsModel
{
   public int CreateStatus { get; set; }
    public List<BrandCurrenciesModel> Currencies { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
}

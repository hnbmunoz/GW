namespace MLAB.PlayerEngagement.Core.Models;

public  class OperatorDetailModel
{
    public int OperatorId { get; set; }
    public string OperatorName { get; set; }
    public int OperatorStatus { get; set; }
    public List<OperatorBrandsModel> Brands { get; set; }

}

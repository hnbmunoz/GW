namespace MLAB.PlayerEngagement.Core.Models;

public class BrandInfoModel
{
	public int BrandId { get; set; }
	public string BrandName { get; set; }
	public int Status { get; set; }
	public int CreatedBy { get; set; }
	public DateTime CreatedDate { get;set;}
	public int UpdatedBy { get; set; }
	public DateTime UpdatedDate { get; set; }
}

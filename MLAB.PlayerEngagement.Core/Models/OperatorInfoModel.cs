namespace MLAB.PlayerEngagement.Core.Models;

public class OperatorInfoModel
    {
	public int OperatorId { get; set; }
	public string OperatorName { get; set; }
	public int Status { get; set; }
	public int CreatedBy { get; set; }
	public DateTime CreatedDate { get; set; }
	public int UpdatedBy { get; set; }
	public DateTime UpdatedDate { get; set; }
}

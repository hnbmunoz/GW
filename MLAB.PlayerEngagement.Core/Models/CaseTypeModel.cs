namespace MLAB.PlayerEngagement.Core.Models;

public class CaseTypeModel
    {
	public int Id { get; set; }
	public string CaseTypeName { get; set; }
	public int CreatedBy { get; set; }
	public DateTime CreatedDate { get; set; }
	public int UpdatedBy { get; set; }
	public DateTime UpdatedDate { get; set; }
}

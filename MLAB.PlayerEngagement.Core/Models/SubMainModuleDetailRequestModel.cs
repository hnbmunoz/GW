namespace MLAB.PlayerEngagement.Core.Models;

public class SubMainModuleDetailRequestModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

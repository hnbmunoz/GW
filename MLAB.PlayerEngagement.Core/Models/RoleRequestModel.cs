namespace MLAB.PlayerEngagement.Core.Models;

public class RoleRequestModel : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public int CreatedBy { get; set; }
    public int UpdateBy { get; set; }
    public List<MainModuleRequestModel> SecurableObjects { get; set; }

}

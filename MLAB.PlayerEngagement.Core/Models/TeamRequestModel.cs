
namespace MLAB.PlayerEngagement.Core.Models;

public class TeamRequestModel : BaseModel
{
    public int TeamId { get; set; }
    public List<OperatorDetailModel> OperatorDetail { get; set; }
    public List<TeamRestrictionRequestModel> TeamRestrictionDetail { get; set; }
    public List<RoleSelectedModel> Roles { get; set; }
    public string TeamDescription { get; set; }
    public string TeamName { get; set; }
    public int TeamStatus { get; set; }
    public int CreatedBy { get; set;}
    public int UpdatedBy  { get; set; }
}

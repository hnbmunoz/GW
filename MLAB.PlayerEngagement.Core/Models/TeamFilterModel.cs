namespace MLAB.PlayerEngagement.Core.Models;

public class TeamFilterModel : BaseModel
{

    public string Brands { get; set; }
    public string Currencies { get; set; }
    public string Operators { get; set; }
    public string Roles { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public string TeamStatuses { get; set; }

}

namespace MLAB.PlayerEngagement.Core.Models;

public class UserGridCustomDisplayModel : BaseModel
{
    public string Module { get; set; }
    public string Display { get; set; }
    public bool IsForFilter { get; set; }
    public long Section {  get; set; }
}

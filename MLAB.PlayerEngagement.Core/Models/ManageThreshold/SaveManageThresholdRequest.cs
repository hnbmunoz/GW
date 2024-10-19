namespace MLAB.PlayerEngagement.Core.Models;

public class SaveManageThresholdRequest: BaseModel
{
    public List<ManageThresholdRequest> ManageThresholds { get; set; }
}

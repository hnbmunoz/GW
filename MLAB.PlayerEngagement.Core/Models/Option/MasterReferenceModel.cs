namespace MLAB.PlayerEngagement.Core.Models.Option;

public class MasterReferenceModel
{
    public int MasterReferenceId { get; set; }
    public string MasterReferenceName { get; set; }
    public bool IsParent { get; set; }
    public int MasterReferenceParentId { get; set; }
    public string MasterReferenceChildName { get; set; }
}

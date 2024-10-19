namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Response;

public class RemLookupsResponseModel
{
    public List<LookupModel> RemProfileNames { get; set; }
    public List<LookupModel> RemAgentNames { get; set; }
    public List<LookupModel> RemPseudoNames { get; set; }
    public List<LookupModel> RemAssignedBys { get; set;  }
    public List<LookupModel> RemActionTypes { get; set; }
    public List<LookupModel> Users { get; set; }
    public List<LookupModel> ActiveRemProfileNames { get; set; }
}

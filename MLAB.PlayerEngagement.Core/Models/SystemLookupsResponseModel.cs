namespace MLAB.PlayerEngagement.Core.Models;

public class SystemLookupsResponseModel
{
    public List<LookupModel> Brands { get; set; }
    public List<LookupModel> PlayerStatuses { get; set; }
    public List<LookupModel> Currencies { get; set; }
    public List<LookupModel> MarketingChannels { get; set; }
    public List<LookupModel> RiskLevels { get; set; }
    public List<LookupModel> VIPLevels { get; set; }
    public List<LookupModel> MessageResponses { get; set; }
    public List<LookupModel> MessageTypes { get; set; }
    public List<LookupModel> MessageStatuses { get; set; }
    public List<LookupModel> Countries { get; set; }
    public List<LookupModel> SignUpPortals { get; set; }
    public List<LookupModel> PaymentGroups { get; set; }
    public List<LookupModel> FieldTypes { get; set; }
    public List<LookupModel> CaseTypes { get; set; }
    public List<LookupModel> CodeLists { get; set; }
    public List<LookupModel> MobileVerificationStatus { get; set; }

}

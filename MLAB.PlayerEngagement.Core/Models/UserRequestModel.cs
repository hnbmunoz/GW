using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Models.Users.Udt;

namespace MLAB.PlayerEngagement.Core.Models;

public class UserRequestModel : BaseModel
{
    public int UserIdRequest { get; set; }
    public int CreatedBy { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public int Status { get; set; }
    public List<TeamSelectedModel> Teams {get;set;}
    public int UpdatedBy { get; set; }
    public string UserPassword { get; set; }
    public List<CommunicationProviderUdtModel> CommunicationProviders { get; set; }
    public List<OptionSelectedModel> TicketTeamAssignmentId { get; set; }
    public List<OptionSelectedModel> TicketCurrencyAssignmentId { get; set; }
    public string MCoreUserId { get; set; }
}

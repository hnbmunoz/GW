namespace MLAB.PlayerEngagement.Core.Models;

public class UserFilterModel : BaseModel
{
    public string Email { get; set; }
    public string Fullname { get; set; }
#nullable enable
    public string? Statuses { get; set; }
#nullable disable
    public string Teams { get; set; }
    public int UserIdRequest { get; set; }
    public long? CommunicationProviderMessageTypeId { get; set; }
    public string CommunicationProviderAccountId { get; set; }
}

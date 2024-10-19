namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Udt;

public class RemContactDetailsUDTModel
{


    public int RemContactDetailsId { get; set; }
    public int RemProfileId { get; set; }
    public int MessageTypeId { get; set; }
    public string ContactDetailValue { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

}

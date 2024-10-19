namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class PortalModel
{
    public int Id { get; set; }
    public string SignUpPortalName { get; set; }
   
    public string CreatedBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

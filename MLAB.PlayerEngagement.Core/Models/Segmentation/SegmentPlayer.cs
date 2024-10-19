namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentPlayer
{
    public int Id { get; set; }
    public int MlabPlayerId { get; set; }
    public string PlayerId { get; set; }
    public string UserName { get; set; }
    public string BrandName { get; set; }
    public string CurrencyName { get; set; }
    public string VipLevelName { get; set; }
    public string AccountStatus { get; set; }
    public DateTime? RegistrationDate { get; set; }
}

namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class InFilePlayersValidationCount
{
    public string ValidBrandId { get; set; }
    public int ValidPlayersCnt { get; set; }
    public int InvalidPlayersCnt { get; set; }
    public int DuplicateCnt { get; set; }
    public string ValidPlayerIdList { get; set;}
}

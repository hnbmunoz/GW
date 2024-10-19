namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class InFileSegmentPlayerResponseModel
{
    public string ValidBrandId { get; set; }
    public int ValidPlayerCount { get; set; }
    public int InvalidPlayerCount { get; set; }
    public int DuplicatePlayerCount { get; set; }
    public List<InFilePlayersInvalidRemarks> RemarksForInvalidPlayers{ get; set; }
    public string ValidPlayerIdList { get; set; }
}

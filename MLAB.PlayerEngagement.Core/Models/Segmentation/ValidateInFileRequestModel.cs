namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class ValidateInFileRequestModel : BaseModel
{
    public InFileSegmentModel playerList { get; set; }
}

public class InFileSegmentModel
{
    public List<InFileSegmentPlayerModel> playerIds { get; set; }
    public string brandName { get; set; }
}


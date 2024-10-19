namespace MLAB.PlayerEngagement.Core.Models.CampaignJourney;

public class JourneyRequestModel : BaseModel 
{
    public int JourneyId { get; set; }
    public string JourneyName { get; set; }
    public string JourneyDescription { get; set; }
    public string JourneyCampaignIds { get; set; }
}


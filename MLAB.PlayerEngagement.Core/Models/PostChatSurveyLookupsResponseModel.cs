using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Response;

namespace MLAB.PlayerEngagement.Core.Models;

public class PostChatSurveyLookupsResponseModel
{
    public List<LookupModel> Licenses { get; set; }
    public List<LookupModel> Skills { get; set; }
    public List<LicenseResponseModel> LicenseByBrandMessageType { get; set; }
    public List<SkillsResponseModel> SkillsByLicense { get; set; }
}

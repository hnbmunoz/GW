namespace MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;

public class PostChatSurveyFilterRequestModel : BaseModel
{
    public long? BrandId { get; set; }
    public string LicenseId { get; set; }
    public string SkillIds { get; set; }
    public long? MessageTypeId { get; set; }
    public string QuestionId { get; set; }
    public string QuestionMessage { get; set; }
    public string QuestionMessageEN { get; set; }
    public long? Status { get; set; }
    public string SurveyId { get; set; }
    public int PageSize { get; set; }
    public int OffsetValue { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
}

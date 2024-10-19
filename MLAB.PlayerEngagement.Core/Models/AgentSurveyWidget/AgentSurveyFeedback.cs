namespace MLAB.PlayerEngagement.Core.Models.AgentSurveyWidget
{
    public class AgentSurveyFeedback : BaseModel
    {
        public int? CommunicationFeedbackId { get; set; }
        public int FeedbackTypeId { get; set; }
        public string FeedbackType { get; set; }
        public int FeedbackCategoryId { get; set; }
        public string FeedbackCategory { get; set; }
        public int FeedbackAnswerId { get; set; }
        public string FeedbackAnswer { get; set; }
        public string FeedbackDetails { get; set; }
        public string SolutionProvided { get; set; }
    }
}

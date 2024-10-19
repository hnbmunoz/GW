namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class CustomerCaseCommunicationRequestModel
    {
        public int CaseCommunicationId { get; set; }
        public int CaseInformationId { get; set; }
        public int PurposeId { get; set; }
        public int MessageTypeId { get; set; }
        public int MessageStatusId { get; set; }
        public int MessageReponseId { get; set; }
        public string StartCommunicationDate { get; set; }
        public string EndCommunicationDate { get; set; }
        public string CommunicationContent { get; set; }
        public long CommunicationOwner { get; set; }
        public int? SurveyTemplateId { get; set; }
        public List<AddCommunicationSurveyRequest> CommunicationSurveyQuestion { get; set; }
        public List<AddCommunicationFeedbackRequest> CommunicationFeedBackType { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Duration { get; set; }
    }
}

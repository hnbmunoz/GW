namespace MLAB.PlayerEngagement.Core.Models.Samespace.Request
{
    public class UpdateSamespaceCdrByCallingCodeRequestModel
    {
        public string CallingCode { get; set; }
        public string SamespaceId { get; set; }
        public string CallerNumber { get; set; }
        public string TeamSystemId { get; set; }
        public string TeamName { get; set; }
        public string UserSystemId { get; set; }
        public string UserDisplayName { get; set; }
        public string UserLoginId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? AnswerTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public string TerminatedBy { get; set; }
        public string TerminatedCause { get; set; }
        public string RecordingFilename { get; set; }
        public string RecordingURL { get; set; }
        public string SpaceId { get; set; }
        public string Type { get; set; }
        public string Direction { get; set; }
        public long DomainId { get; set; }
        public string Channel { get; set; }
        public string Notes { get; set; }
    }
}

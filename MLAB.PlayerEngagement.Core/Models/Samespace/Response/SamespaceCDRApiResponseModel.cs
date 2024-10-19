using System.Text.Json.Serialization;

namespace MLAB.PlayerEngagement.Core.Models.Samespace.Response
{
    public class SamespaceCdrApiResponseModel
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("data")]
        public List<SamespaceCallHistoryData> Data { get; set; }
    }

    public class SamespaceCallHistoryData
    {
        [JsonPropertyName("uuid")]
        public string UUID { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("user_system_id")]
        public string UserSystemId { get; set; }

        [JsonPropertyName("user_display_name")]
        public string UserDisplayName { get; set; }

        [JsonPropertyName("user_login_id")]
        public string UserLoginId { get; set; }

        [JsonPropertyName("caller")]
        public string Caller { get; set; }

        [JsonPropertyName("callee")]
        public string Callee { get; set; }

        [JsonPropertyName("team_system_id")]
        public string TeamSystemId { get; set; }

        [JsonPropertyName("team_name")]
        public string TeamName { get; set; }

        [JsonPropertyName("recording_filename")]
        public string RecordingFilename { get; set; }

        [JsonPropertyName("recording_url")]
        public string RecordingURL { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("answer_time")]
        public DateTime? AnswerTime { get; set; }

        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("terminated_by")]
        public string TerminatedBy { get; set; }

        [JsonPropertyName("termination_cause")]
        public string TerminatedCause { get; set; }

        [JsonPropertyName("domain_id")]
        public long DomainId { get; set; }

        [JsonPropertyName("space_id")]
        public string SpaceId { get; set; }

        [JsonPropertyName("custom_data")]
        public SamespaceCallHistoryCustomData CustomData { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }
    }

    public class SamespaceCallHistoryCustomData
    {
        [JsonPropertyName("dial_id")]
        public string DialId { get; set; }
    }
}


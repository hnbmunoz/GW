using System.Text.Json.Serialization;

namespace MLAB.PlayerEngagement.Core.Models.Samespace.Response
{
    public class SamespaceMakeACallResponseData
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("callId")]
        public string CallId { get; set; }
    }
}

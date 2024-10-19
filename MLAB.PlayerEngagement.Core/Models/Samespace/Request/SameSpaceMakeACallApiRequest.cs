using System.Text.Json.Serialization;

namespace MLAB.PlayerEngagement.Core.Models.Samespace.Request
{
    public class Custom
    {
        [JsonPropertyName("dial_id")]
        public string DialId { get; set; }
    }

    public class SamespaceMakeACallApiRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("custom")]
        public Custom Custom { get; set; }
    }
}

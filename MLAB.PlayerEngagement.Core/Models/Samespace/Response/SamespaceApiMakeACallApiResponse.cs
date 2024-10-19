using System.Text.Json.Serialization;

namespace MLAB.PlayerEngagement.Core.Models.Samespace.Response
{
    public class SamespaceApiMakeACallApiResponse
    {
        [JsonPropertyName("responseData")]
        public SamespaceMakeACallResponseData ResponseData { get; set; }
    }
}

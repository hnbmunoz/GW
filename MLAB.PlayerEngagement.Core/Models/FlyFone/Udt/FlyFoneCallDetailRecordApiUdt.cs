using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Udt
{
    public class FlyFoneCallDetailRecordApiUdt
    {
        [JsonPropertyName("call_date")]
        public string CallDate { get; set; }
        [JsonPropertyName("ext_team")]
        public string ExtTeam { get; set; }
        [JsonPropertyName("calling_code")]
        public string CallingCode { get; set; }
        [JsonPropertyName("ext_number")]
        public string ExtNumber { get; set; }
        [JsonPropertyName("ext_name")]
        public string ExtName { get; set; }
        [JsonPropertyName("cms_user")]
        public string CmsUser { get; set; }
        [JsonPropertyName("called_number")]
        public string CalledNumber { get; set; }
        [JsonPropertyName("call_disposition")]
        public string CallDisposition { get; set; }
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
        [JsonPropertyName("call_route")]
        public string CallRoute { get; set; }
        [JsonPropertyName("call_recording")]
        public string CallRecording { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CloudTalk.Response
{
    public class CloudTalkMakeACallResponseData
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}

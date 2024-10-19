using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CloudTalk.Request
{
    public class CloudTalkMakeACallApiRequest
    {
        [JsonPropertyName("agent_id")]
        public string AgentId { get; set; }
        [JsonPropertyName("callee_number")]
        public string CalleeNumber { get; set; }
    }
}

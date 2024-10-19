using MLAB.PlayerEngagement.Core.Models.FlyFone.Udt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Response
{
    public class FlyFoneCallDetailRecordApiResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("data")]
        public List<FlyFoneCallDetailRecordApiUdt> Data { get; set; }
    }
}

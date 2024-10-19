using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Request
{
    public class FlyFoneOutboundCallRequestModel
    {
        public string Outnumber { get; set; }
        public string Ext { get; set; }
        public string Department { get; set; }
        public string UserId { get; set; }
        public long MlabPlayerId { get; set; }
    }
}

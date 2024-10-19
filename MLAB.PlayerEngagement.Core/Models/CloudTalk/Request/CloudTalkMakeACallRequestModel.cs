using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CloudTalk.Request
{
    public class CloudTalkMakeACallRequestModel
    {
        public string AgentId { get; set; }
        public long MlabPlayerId { get; set; }
        public int UserId { get; set; }
    }
}

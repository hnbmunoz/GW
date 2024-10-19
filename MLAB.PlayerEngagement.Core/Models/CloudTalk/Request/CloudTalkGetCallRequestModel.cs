using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CloudTalk.Request
{
    public class CloudTalkGetCallRequestModel
    {
        public int UserId { get; set; }
        public string AgentId { get; set; }
        public string DialId { get; set; }
    }
}

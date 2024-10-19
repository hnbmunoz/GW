using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public  class SetStatusRequest
    {
        public string conversationID { get; set; }
        public int? caseID { get; set; }
        public string newStatus { get; set; }
    }
}

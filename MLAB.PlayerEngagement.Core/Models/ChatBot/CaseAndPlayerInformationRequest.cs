using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class CaseAndPlayerInformationRequest
    {
        public string ConversationId { get; set; }
        public string ProviderId { get; set; }
        public string License { get; set; }
        public string Skill { get; set; }
        public string BrandName { get; set; }
    }
}

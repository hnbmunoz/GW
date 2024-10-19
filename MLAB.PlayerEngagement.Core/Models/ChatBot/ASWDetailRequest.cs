using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class ASWDetailRequest
    {
        public string ConversationID { get; set; }
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public string BrandName { get; set; }
        public int CaseID { get; set; }
        public int LanguageID { get; set; }
        public int TopicID { get; set; }
        public int SubtopicID { get; set; }
        public string ProviderID { get; set; }
        public string License { get; set; }
        public long UserId { get; set; }
    }
}

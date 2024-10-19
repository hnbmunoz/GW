using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class TopicResponse: BaseResponse
    {
        public int TopicID { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.Message
{
    public class MessageResponseListFilterModel : BaseModel
    {
        public string MessageResponseName { get; set; }
        public string MessageResponseStatus { get; set; }
        public string MessageStatusIds { get; set; }
        public int MessageStatusId { get; set; }
    }
}

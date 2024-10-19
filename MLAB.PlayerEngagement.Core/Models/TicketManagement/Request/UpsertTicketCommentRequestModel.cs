using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class UpsertTicketCommentRequestModel : BaseModel
    {
        public string Comment { get; set; }
        public long TicketId { get; set; }
        public long TicketCommentId { get; set; }
        public int TicketTypeId { get; set; }
    }
}

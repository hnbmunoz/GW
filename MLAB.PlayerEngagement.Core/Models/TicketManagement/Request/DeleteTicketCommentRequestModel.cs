using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class DeleteTicketCommentRequestModel : BaseModel
    {
        public long TicketCommentId { get; set; }
        public int TicketTypeId { get; set; }
    }
}

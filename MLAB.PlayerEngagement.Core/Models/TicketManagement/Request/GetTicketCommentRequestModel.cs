using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class GetTicketCommentRequestModel : BaseModel
    {
        public long TicketId { get; set; }
        public bool ViewOldComment { get; set; }
        public int TicketTypeId { get; set; }
    }
}

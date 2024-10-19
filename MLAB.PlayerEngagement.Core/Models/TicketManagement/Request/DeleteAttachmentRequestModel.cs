using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class DeleteAttachmentRequestModel : BaseModel
    {
        public int TicketAttachmentId { get; set; }
        public int TicketTypeId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketCommentResponseModel
    {
        public string TicketCommentId { get; set; }
        public string Comment { get; set; }
        public bool IsEdited { get; set; }
        public string Timestamp { get; set; }
        public string CreatedBy { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketTypesResponseModel
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public string TicketCode { get; set; }
        public string Status { get; init; }

    }
}

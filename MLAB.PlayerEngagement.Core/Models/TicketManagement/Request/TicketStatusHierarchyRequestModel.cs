using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class TicketStatusHierarchyRequestModel
    {
        public int TicketTypeId { get; set; }
        public bool IsForVip { get; set; }
        public int? UserId { get; set; }
    }
}

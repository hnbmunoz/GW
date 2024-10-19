using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class TicketPlayerRequestModel
    {
        public int BrandId { get; set; }
        public string PlayerId { get; set; }
        public string PlayerUsername { get; set; }
    }
}

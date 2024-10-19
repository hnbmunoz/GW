using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class TransactionDataRequestModel
    {
        public string PlayerId { get; set; }
        public string TransactionId { get; set; }
        public int UserId { get; set; }
    }
}

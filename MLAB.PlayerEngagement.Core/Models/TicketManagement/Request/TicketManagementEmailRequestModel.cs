using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class TicketManagementEmailRequestModel
    {
        public string Source { get; set; }
        public string AffectedField { get; set; }
        public string AffectedValue { get; set; }
        public string ErrorDetails { get; set; }
        public string userFullName { get; set; }
        public string emailSignature { get; set; }
    }
}

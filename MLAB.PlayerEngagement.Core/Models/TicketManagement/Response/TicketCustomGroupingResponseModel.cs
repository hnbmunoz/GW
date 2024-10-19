using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketCustomGroupingResponseModel
    {
        public int TicketCustomId { get; set; }
        public string TicketCustomName { get; set; }
        public bool hasAdd { get; set; }
        public bool hasEdit { get; set; }
        public bool hasView { get; set; }
    }
}

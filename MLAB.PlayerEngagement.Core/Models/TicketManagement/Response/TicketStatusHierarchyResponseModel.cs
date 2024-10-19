using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketStatusHierarchyResponseModel
    {
        public int ParentStatusId { get; set; }
        public string ParentStatusName { get; set; }
        public int ChildStatusId { get; set; }
        public string ChildStatusName { get; set; }
        public string ChildStatusColorCode { get; set; }
        public bool IsForTransactionVerification { get; set; }
    }
}

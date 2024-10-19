using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CustomerCaseCommunicationListModel
    {
        public List<CustomerCaseCommunicationModel> CaseCommunicationList { get; set; }
        public int RecordCount { get; set; }
    }
}

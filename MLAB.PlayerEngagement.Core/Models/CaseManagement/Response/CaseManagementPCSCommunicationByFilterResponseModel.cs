using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class CaseManagementPcsCommunicationByFilterResponseModel
    {
        public int RecordCount { get; set; }
        public List<CaseManagementPcsCommunicationResponseModel> CaseManagementPCSCommunications { get; set; }
    }
}

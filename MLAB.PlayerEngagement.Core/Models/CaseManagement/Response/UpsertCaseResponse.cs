using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class UpsertCaseResponse
    {
        public int CaseId { get; set; }
        public string CaseStatus { get; set; }
        public string CaseMissingFields { get; set; }
    }
}

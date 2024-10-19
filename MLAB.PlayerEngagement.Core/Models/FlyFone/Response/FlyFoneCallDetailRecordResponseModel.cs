using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Response
{
    public class FlyFoneCallDetailRecordResponseModel
    {
        public int RecordId { get; set; }
        public string CreatedBy { get; set; }
        public string Outnumber { get; set; }
        public string Extension { get; set; }
        public string Department { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CallingCode { get; set; }
        public string ResponsePayload { get; set; }
    }
}

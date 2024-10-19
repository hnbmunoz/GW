using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Request
{
    public class FlyFoneCallDetailRecordRequestModel
    {
        public string CalledNumber { get; set; }
        public string CallingCode { get; set; }
        public string EndTime { get; set; }
        public string UserId { get; set; }
    }
}

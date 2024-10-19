using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Response
{
    public class FlyFoneOutboundCallResponsetModel
    {
        public int ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public string DialId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models
{
    public class BaseResponse
    {
        public String Status { get; set; }
        public String Errormessage { get; set; }
        public int ErrorCode { get; set; }
    }
}

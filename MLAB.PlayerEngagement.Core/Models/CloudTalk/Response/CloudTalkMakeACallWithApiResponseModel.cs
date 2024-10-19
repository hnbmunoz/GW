using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CloudTalk.Response
{
    public class CloudTalkMakeACallWithApiResponseModel
    {
        public string DialId { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}

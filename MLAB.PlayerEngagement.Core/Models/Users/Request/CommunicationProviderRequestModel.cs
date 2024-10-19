using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.Users.Request
{
    public class CommunicationProviderRequestModel
    {
        public long UserId { get; set; }
        public int ProviderId { get; set; }
        public string ProviderAccount { get; set; }
        public string Action { get; set; }
    }
}

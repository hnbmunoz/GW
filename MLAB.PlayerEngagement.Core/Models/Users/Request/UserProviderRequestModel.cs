using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.Users.Request
{
    public class UserProviderRequestModel
    {
        public long UserId { get; set; }
        public int ProviderId { get; set; }
        public string ProviderAccount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.Users.Udt
{
    public class CommunicationProviderAccountUdt
    {
        public int ChatUserAccountId { get; set; }
        public string MessageTypeId { get; set; }
        public string MessageTypeName { get; set; }
        public string AccountID { get; set; }
        public string ChatUserAccountStatus { get; set; }
        public string Department { get; set; }
        public int MessageGroupId { get; set; }
        public int? SubscriptionId { get; set; }
    }
}

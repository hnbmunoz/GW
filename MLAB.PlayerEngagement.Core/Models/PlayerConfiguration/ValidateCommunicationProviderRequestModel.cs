using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration
{
    public class ValidateCommunicationProviderRequestModel
    {
        public long PaymentMethodExtId { get; set; }
        public int MessageTypeId { get; set; }
        public string ProviderAccount { get; set; }
    }
}

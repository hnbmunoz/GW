using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration
{
    public class SavePaymentMethodRequestModel: BaseModel
    {
        public long? reqPaymentMethodId { get; set; }
        public long reqPaymentMethodICoreId { get; set; }
        public string reqPaymentMethodName {  get; set; }
        public int reqPaymentMethodVerifier { get; set; }
        public bool reqPaymentMethodStatus{ get; set; }
        public int? reqPaymentMethodMessageTypeId { get; set; }
        public string reqPaymentMethodProviderId {get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration
{
    public class ValidatePaymentMethodNameRequestModel
    {
        public long PaymentMethodExtId { get; set; }
        public string PaymentMethodName { get; set; }
        public long IcoreId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketPlayerResponseModel
    {
        public long MlabPlayerId { get; set; }
        public string PlayerId { get; set; }
        public string Username { get; set; }
        public string CurrencyCode { get; set; }
        public string CountryName { get; set; }
        public string VipLevel { get; set; }
        public bool IsForVipCredit { get; set; }
    }
}

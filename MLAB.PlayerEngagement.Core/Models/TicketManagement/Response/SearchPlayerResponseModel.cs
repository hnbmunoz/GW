using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class SearchPlayerResponseModel
    {
		public int PlayerId { get; set; }
        public string PlayerUsername { get; set; }
        public string CurrencyCode { get; set; }
        public string CountryName { get; set; }
        public string VipLevel { get; set; }
    }
}

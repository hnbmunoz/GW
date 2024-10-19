using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.ChatBot
{
    public class PlayerTransactionRequest
    {
        public string Username { get; set; }
        public string BrandName { get; set; }
        public string PlayerID { get; set; }
    }
}

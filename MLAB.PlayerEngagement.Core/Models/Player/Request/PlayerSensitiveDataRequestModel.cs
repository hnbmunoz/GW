using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.Player.Request
{
    public class PlayerSensitiveDataRequestModel
    {
        public int UserId { get; set; }
        public bool HasAccess { get; set; }
        public long MlabPlayerId { get; set; }
        public string SensitiveField { get; set; }
    }
}

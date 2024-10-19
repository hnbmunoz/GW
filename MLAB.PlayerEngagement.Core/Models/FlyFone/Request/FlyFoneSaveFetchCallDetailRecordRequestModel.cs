using MLAB.PlayerEngagement.Core.Models.FlyFone.Udt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Request
{
    public class FlyFoneSaveFetchCallDetailRecordRequestModel
    {
        public string UserId { get; set; }
        public List<FlyFoneCallDetailRecordApiUdt> FlyFoneCallDetailRecordType { get; set; }
    }
}

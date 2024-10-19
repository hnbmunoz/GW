using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.FlyFone.Udt
{
    public class FormattedFlyFoneCdrUdt
    {
        public string CallDate { get; set; }
        public string ExtTeam { get; set; }
        public string CallingCode { get; set; }
        public string ExtNumber { get; set; }
        public string ExtName { get; set; }
        public string CmsUser { get; set; }
        public string CalledNumber { get; set; }
        public string CallDisposition { get; set; }
        public string Duration { get; set; }
        public string CallRoute { get; set; }
        public string CallRecording { get; set; }
    }
}

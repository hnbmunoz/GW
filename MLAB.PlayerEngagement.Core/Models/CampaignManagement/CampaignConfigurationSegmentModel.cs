using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CampaignManagement
{
    public class CampaignConfigurationSegmentModel
    {
        public int SegmentId { get; set; }
        public string SegmentName { get; set; }
        public bool SegmentStatus { get; set; }
        public long? VarianceId { get; set; }
        public string VarianceName { get; set; }
    }
}

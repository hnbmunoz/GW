using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CaseCommunicationAnnotationModel
    {
        public long CaseCommunicationAnnotationId { get; set; }
        public long CaseCommunicationId { get; set; }
        public string PositionGroup { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
        public string Text { get; set; }
        public string Tag { get; set; }
        public bool IsValid { get; set; }

    }
}

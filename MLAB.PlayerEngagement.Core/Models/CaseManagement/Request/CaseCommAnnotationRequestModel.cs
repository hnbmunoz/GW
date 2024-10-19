using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class CaseCommAnnotationRequestModel : BaseModel
    {
        public List<CaseCommunicationAnnotationModel> CaseCommunicationAnnotationUdt { get; set; }
        public long CaseCommunicationId { get; set; }

    }
}

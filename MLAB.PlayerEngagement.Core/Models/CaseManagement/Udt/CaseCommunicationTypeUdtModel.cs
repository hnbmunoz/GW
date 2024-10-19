using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Udt
{
    public class CaseCommunicationTypeUdtModel
    {
        public int CaseCommunicationId { get; set; }
        public int CaseInformationId { get; set; }
        public int PurposeId { get; set; }
        public int MessageTypeId { get; set; }
        public int MessageStatusId { get; set; }
        public int MessageReponseId { get; set; }
        public string StartCommunicationDate { get; set; }
        public string EndCommunicationDate { get; set; }
        public long CommunicationOwner { get; set; }
        public string CommunicationContent { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Duration { get; set; }
        public int? SurveyTemplateId { get; set; }
    }
}

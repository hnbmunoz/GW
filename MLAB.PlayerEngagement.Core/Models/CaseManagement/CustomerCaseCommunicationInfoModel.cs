using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement
{
    public class CustomerCaseCommunicationInfoModel
    {
        public long CaseInformationId { get; set; }
        public long CaseCommunicationId { get; set; }
        public string ExternalCommunicationId { get; set; }
        public string Purpose { get; set; }
        public int PurposeId { get; set; }
        public string MessageType { get; set; }
        public int MessageTypeId { get; set; }
        public string CommunicationOwnerName { get; set; }
        public int CommunicationOwner { get; set; }
        public string MessageStatus { get; set; }
        public int MessageStatusId { get; set; }
        public string MessageResponse { get; set; }
        public int MessageResponseId { get; set; }
        public string StartCommunicationDate { get; set; }
        public string EndCommunicationDate { get; set; }
        public int Duration { get; set; }
        public string CommunicationContent { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public string UpdatedBy { get; set; }
        public int? SurveyTemplateId { get; set; }
        public string ReportedDate { get; set; }
    }
}

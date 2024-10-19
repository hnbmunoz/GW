using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public  class CustomerCaseCommunicationFeedbackResponseModel
    {
        public int CommunicationFeedbackId { get; set; }
        public int CaseCommunicationId { get; set; }
        public int CommunicationFeedbackNo { get; set; }
        public int FeedbackTypeId { get; set; }
        public string FeedbackTypeName { get; set; }
        public int FeedbackCategoryId { get; set; }
        public string FeedbackCategoryName { get; set; }
        public int FeedbackAnswerId { get; set; }
        public string FeedbackAnswerName { get; set; }
        public string CommunicationFeedbackDetails { get; set; }
        public string CommunicationSolutionProvided { get; set; }
    }
}

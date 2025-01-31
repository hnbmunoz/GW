﻿using MLAB.PlayerEngagement.Core.Models.CaseManagement.Udt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class CaseManagementPCSCommunicationByFilterRequestModel : BaseModel
    {
        public string BrandId { get; set; }
        public string MessageTypeId { get; set; }
        public string License { get; set; }
        public string SkillId { get; set; }
        public string SummaryAction { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int PageSize { get; set; }
        public int OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<PCSCommunicationQuestionAnswerUdtModel> PCSCommunicationQuestionAnswerType { get; set; }
    }
}

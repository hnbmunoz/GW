using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class CaseManagementPCSQuestionsByFilterRequestModel: BaseModel
    {
        public string BrandId { get; set; }
        public string? MessageTypeId { get; set; }
        public string? License { get; set; }
        public string? SkillId { get; set; }
        public string? SummaryAction { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}

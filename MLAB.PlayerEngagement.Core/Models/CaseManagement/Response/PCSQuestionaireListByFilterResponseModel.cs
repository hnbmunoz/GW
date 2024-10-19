using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class PCSQuestionaireListByFilterResponseModel
    {
        public string QuestionId { get; set; }
        public string QuestionMessageEN { get; set; }
        public string Answer { get; set; }
        public bool FreeText { get; set; }
    }
}

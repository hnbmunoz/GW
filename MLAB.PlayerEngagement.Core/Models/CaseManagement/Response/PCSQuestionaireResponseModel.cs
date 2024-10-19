using MLAB.PlayerEngagement.Core.Models.CaseManagement.Udt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Response
{
    public class PcsQuestionaireResponseModel
    {
        public List<ExportPcsResponseModel> RecordList { get; set; }
        public List<ExportQuestionAnswerUdtModel> QuestionAnswer { get; set; }
    }
}

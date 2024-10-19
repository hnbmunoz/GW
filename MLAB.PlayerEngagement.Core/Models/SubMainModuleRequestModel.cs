using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models
{
    public class SubMainModuleRequestModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public List<SubMainModuleDetailRequestModel> SubModuleDetails { get; set; }
    }
}

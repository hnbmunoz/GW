using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models
{
    public class CustomLookModel : LookupModel
    {
        public bool HasTableau { get; set; } = false;
        public int DataSourceId { get; set; }
    }
}

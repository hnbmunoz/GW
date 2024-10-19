using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.Azure.Response
{
    public class FileListResponseModel
    {
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string Base64Text { get; set; }
    }

}

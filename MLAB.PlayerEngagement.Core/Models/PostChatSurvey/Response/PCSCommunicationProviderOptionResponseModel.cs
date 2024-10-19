using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Response
{
    public class PCSCommunicationProviderOptionResponseModel
    {
        public string LicenseId { get; set; }
        public int BrandId { get; set; }
        public int MessageTypeId { get; set; }
        public string MessageTypeName { get; set; }
    }
}

using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketInfoResponseModel : TicketInformationModel
    {
        public List<TicketDetailsResponseModel> TicketDetails { get; set; }
        public List<TicketCustomPlayerResponseModel> TicketPlayer { get; set; }
        public List<TicketAttachmentResponseModel> TicketAttachments { get; set; }
    }

    public class TicketInformationModel
    {
        public int TicketId { get; set; }
    }

    public class TicketDetailsResponseModel
    {
        public int TicketTypeFieldMappingId { get; set; }
        public int TicketFieldId { get; set; }
        public string TicketFieldName { get; set; }
        public string TicketTypeFieldMappingValue { get; set; }
        public int IsTransactionFieldMapping { get; set; }
    }

    public class TicketCustomPlayerResponseModel
    {
        public int TicketPlayerId { get; set; }
        public int BrandID { get; set; }
        public string PlayerId { get; set; }
        public long MlabPlayerId { get; set; }
    }

    public class TicketAttachmentResponseModel
    {
        public int TicketAttachmentId { get; set; }
        public int TypeId { get; set; }
        public string URL { get; set; }
    }


}

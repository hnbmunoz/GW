using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration
{
    public class SaveTicketFieldsRequestModel : BaseModel
    {
        public long PaymentMethodId { get; set; }
        public string SelectedTicketFields { get; set; }
    }
}

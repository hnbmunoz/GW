using System;
using System.Collections.Generic;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class TicketStatusPopupMappingResponseModel
    {
        public long ParentStatusId { get; set; }
        public long ChildStatusId { get; set; }
        public long TicketTypeId { get; set; }
        public long TicketTypeFieldMappingId { get; set; }
        public bool IsForReview { get; set; }
        public long Order { get; set; }
        public string AlternativeLabel { get; set; }
        public bool IsRequired { get; set; }
    }
}

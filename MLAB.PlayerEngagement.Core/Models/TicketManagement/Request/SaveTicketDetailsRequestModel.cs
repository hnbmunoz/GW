namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class SaveTicketDetailsRequestModel : BaseModel
    {
        public int TicketId { get; set; }
        public int TicketTypeId { get; set; }
        public List<TicketPlayerModel> TicketPlayerIds { get; set; }
        public List<TicketAttachmentModel> TicketAttachments { get; set; }
        public List<TicketFieldDefinition> TicketDetails { get; set; }
        public List<TicketHistoryLabelTypeUDTModel> TicketHistoryLabelType { get; set; }
    }

    public class TicketPlayerModel
    {
        public int TicketPlayerId { get; set; }        
        public long MlabPlayerId { get; set; }
    }

    public class TicketAttachmentModel
    {
        public int TicketAttachmentId { get; set; }
        public int TypeId { get; set; }
        public string URL { get; set; }
    }

    public class TicketFieldDefinition
    {
        public int TicketFieldMappingId { get; set; }
        public string TicketFieldMappingValue { get; set; }
    }
}

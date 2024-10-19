namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class UpsertPopupTicketDetailsRequestModel : BaseModel
    {
        public int TicketId { get; set; }
        public int TicketTypeId { get; set; }
        public List<TicketFieldDefinition> TicketDetails { get; set; }
        public string Comment { get; set; }
        public long ICoreStatusId {  get; set; }
        public long FmboStatusId { get; set; }
        public bool SendUpdateEmail { get; set; }
        public List<TicketHistoryLabelTypeUDTModel> TicketHistoryLabelType { get; set; }
        public long ParentStatusId { get; set; }
        public long ChildStatusId { get; set; }
    }
}

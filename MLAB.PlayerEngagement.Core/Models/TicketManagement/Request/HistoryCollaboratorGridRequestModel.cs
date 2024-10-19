namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class HistoryCollaboratorGridRequestModel : BaseModel
    {
        public int TicketId { get; set; }
        public int TicketTypeId { get; set; }
        public int? OffsetValue { get; set; }
        public int? PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}

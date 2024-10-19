namespace MLAB.PlayerEngagement.Core.Models.EngagementHub
{
    public class BroadcastFilterRequestModel : BaseModel
    {
        public long? BroadcastId { get; set; }
        public string BroadcastName { get; set; }
        public string BroadcastStartDate { get; set; }
        public string BroadcastEndDate { get; set; }
        public string BroadcastStatusId { get; set; }
        public string MessageTypeId { get; set; }
        public int PageSize { get; set; }
        public int OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}

namespace MLAB.PlayerEngagement.Core.Models.EngagementHub
{
    public class BotAutoReplyFilterRequestModel : BaseModel
    {
        public int BotDetailId { get; set; }        
        public int PageSize { get; set; }
        public int OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}
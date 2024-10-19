namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request
{
    public class AutoDistributionSettingFilterRequestModel : BaseModel
    {
        public string ConfigurationName { get; set; }
        public string RemProfileIds { get; set; }
        public bool? Status { get; set; }
        public int PageSize { get; set; }
        public int OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}

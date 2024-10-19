

namespace MLAB.PlayerEngagement.Core.Models.AppConfigSettings
{
    public class AppConfigSettingResponseModel
    {
        public int AppConfigSettingId { get; set; }
        public int? ApplicationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public int PageSize { get; set; }
        public int OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

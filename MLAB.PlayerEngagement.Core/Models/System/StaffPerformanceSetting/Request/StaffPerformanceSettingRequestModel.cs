namespace MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request
{
    public class StaffPerformanceSettingRequestModel
    {
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }
        public int OffsetValue { get; set; }
        public int PageSize { get; set; }
    }
}

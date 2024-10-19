namespace MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Response
{
    public class StaffPerformaneSettingResponseModel
    {
        public List<StaffPerformanceSettingDetails> StaffPerformanceSettingList {  get; set; }
        public int RowCount {  get; set; }
    }

    public class StaffPerformanceSettingDetails
    {
        public int Id { get; set; }
        public string SettingName { get; set; }
        public string Parent { get; set; }
        public int Position { get; set; }
    }
}

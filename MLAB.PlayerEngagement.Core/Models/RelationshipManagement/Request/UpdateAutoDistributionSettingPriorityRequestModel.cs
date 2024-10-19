namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request
{
    public class UpdateAutoDistributionSettingPriorityRequestModel
    {
        public List<AutoDistributionSettingPriorityModel> AutoConfigurations { get; set; }
        public long UserId { get; set; }
    }
}

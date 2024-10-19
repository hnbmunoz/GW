namespace MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request
{
    public class UpdateAutoDistributionConfigStatusRequestModel
    {
        public long AutoDistributionSettingId { get; set; }
        public bool? StatusId { get; set; }
        public long Userid { get; set; }
    }
}

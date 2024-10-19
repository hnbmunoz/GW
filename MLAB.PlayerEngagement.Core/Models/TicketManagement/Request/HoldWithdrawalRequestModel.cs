namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class HoldWithdrawalRequestModel
    {
        public int UserId { get; set; }
        public List<UpdatePlayerSetting> UpdatePlayerSettings {  get; set; }
        public List<long> PlayerIds {  get; set; }
    }

    public class UpdatePlayerSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

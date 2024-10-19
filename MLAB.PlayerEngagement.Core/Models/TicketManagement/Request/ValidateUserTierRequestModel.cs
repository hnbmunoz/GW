
namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Request
{
    public class ValidateUserTierRequestModel
    {
        public long TicketTypeId { get; set; }
        public long UserId { get; set; }
        public long MlabPlayerId { get; set;}
        public decimal AdjustmentAmount { get; set; }
    }
}

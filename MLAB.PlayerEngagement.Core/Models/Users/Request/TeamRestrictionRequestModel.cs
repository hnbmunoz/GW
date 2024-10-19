using System.Text.Json.Serialization;

namespace MLAB.PlayerEngagement.Core.Models
{
    public class TeamRestrictionRequestModel
    {
        public int OperatorId { get; set; }
        public int TeamId { get; set; }
        public int AccessRestrictionFieldId { get; set; }
        public int AccessRestrictionFieldValue { get; set; }

    }
}

using System.ComponentModel;

namespace MLAB.PlayerEngagement.Core.Constants;

public enum RemIntegrationEvents
{
    [Description("Assign")]
    Assign=1,
    [Description("Remove")]
    Remove=2,
    [Description("Reassign")]
    Reassign=3,
    UpdateRemSetting=4,
    SetRemOnlineStatus=5,
    UpdateRemProfile=6
}

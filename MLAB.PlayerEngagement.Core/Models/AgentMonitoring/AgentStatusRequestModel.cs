using MLAB.PlayerEngagement.Core.Models.TicketManagement.Response;

namespace MLAB.PlayerEngagement.Core.Models.AgentMonitoring;

public class AgentStatusRequestModel : BaseModel
{  
    public List<AgentStatusListModel> AgentStatusList { get; set; }  
}

public class AgentStatusListModel
{
    public int CampaignAgentId { get; set; }  
}

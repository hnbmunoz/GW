using MLAB.PlayerEngagement.Core.Models.RelationshipManagementIntegration;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IRemIntegrationPublisherService
{
    Task<bool> SendAssignPlayerToRemProfile(RemDistributionEventRequestModel request);
    Task<bool> SendRemovePlayerFromRemProfile(RemDistributionEventRequestModel request);
    Task<bool> SendReassignPlayerFromRemProfile(RemDistributionEventRequestModel request);
    Task<bool> SendUpdateRemProfile(RemProfileEventRequestModel request);
    Task<bool> SendSetOnlineStatus(RemOnlineStatusRequestModel request);
    Task<bool> SendUpdateRemSetting(RemUpdateSettingRequestModel request);
}

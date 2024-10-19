using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Response;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IRelationshipManagementService
{
    Task<bool> UpdateRemProfileStatus(UpdateRemProfileRequestModel request);
    Task<RemDistributionPlayerResponseModel> GetRemDistributionPlayerAsync(RemProfileFilterRequestModel request);
    Task<bool> RemoveRemDistributionAsync(long remDistributionId, long userId);
    Task<bool> ValidateTemplateSettingAsync(ValidateTemplateRequestModel request);
    Task<bool> ValidateRemProfileNameAsync(ValidateRemProfileNameRequestModel request);
    Task<List<RemProfileReusableResponseModel>> GetReusableRemProfileDetails();
    Task<bool> UpsertRemDistributionAsync(UpsertRemDistributionRequestModel[] request);
    Task<RemLookupsResponseModel> GetRemLookupsAsync();
    Task<bool> ValidateRemProfileIfHasPlayer(int remProfileId);
    Task<List<ScheduleTemplateModel>> GetAllScheduleTemplateListAsync();
    Task<List<MessageTypeContactDetailListResponseModel>> GetMessageTypeChannelListAsync();
    Task<Tuple<int, List<RemProfileFilterModel>>> GetRemProfileByFilterAsync(RemProfileFilterRequestModel request);
    Task<RemDistributionFilterResponseModel> GetRemDistributionByFilterAsync(RemDistributionFilterRequestModel request);
    Task<RemHistoryFilterResponseModel> GetRemHistoryByFilterAsync(RemHistoryFilterRequestModel request);
    Task<ScheduleTemplateListResponseModel> GetScheduleTemplateSettingListAsync(ScheduleTemplateListRequestModel request);
    Task<bool> UpdateMaxPlayerCountConfigAsync(UpdateMaxPlayerCountConfigRequestModel request);
    Task<bool> RemoveDistributionbyVipLevelAsync(RemoveDistributionByVIPLevelRequestModel request);
    Task<bool> UpdateAutoDistributionSettingPriorityAsync(UpdateAutoDistributionSettingPriorityRequestModel request);
    Task<bool> UpdateAutoDistributionConfigurationStatusAsync(UpdateAutoDistributionConfigStatusRequestModel request);
    Task<bool> DeleteAutoDistributionConfigurationByIdAsync(int autoDistributionSettingId);
    Task<bool> ValidateAutoDistributionConfigurationNameAsync(int autoDistributionSettingId, string configurationName);
    Task<AutoDistributionConfigurationCountResponseModel> GetAutoDistributionConfigurationCountAsync(int userId);
    Task<List<DistributionRemovedVipLevelsResponseModel>> GetRemovedVipLevels();
    Task<List<AutoDistributionSettingConfigsListOrder>> GetAllAutoDistributionConfigListOrderAsync();
}

using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

namespace MLAB.PlayerEngagement.Core.Repositories;

public interface IPlayerConfigurationFactory
{
    Task<int> ValidateVIPLevelNameAsync(string VIPLevelName);
    Task<bool> CheckExistingIDNameCodeListAsync(PlayerConfigCodeListValidatorRequestModel request);
    Task<bool> ValidatePlayerConfigurationRecordAsync(PlayerConfigurationRequestModel request);
    Task<List<LanguageModel>> GetLanguageOptionList();
    Task<List<TicketFieldsModel>> GetTicketFieldsList();
}

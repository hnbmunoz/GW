using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.Users.Request;

namespace MLAB.PlayerEngagement.Core.Services;

public interface IPlayerConfigurationService
{
    Task<Tuple<int, string>> ValidateVIPLevelNameAsync(string VIPLevelName);
    Task<bool> CheckExistingIDNameCodeListAsync(PlayerConfigCodeListValidatorRequestModel requestModel);
    Task<bool> ValidatePlayerConfigurationRecordAsync(PlayerConfigurationRequestModel request);
    Task<List<LanguageModel>> GetLanguageOptionList();
    Task<List<TicketFieldsModel>> GetTicketFieldsList();
    Task<bool> ValidatePaymentMethodCommunicationProviderAsync(ValidateCommunicationProviderRequestModel request);
    Task<bool> ValidatePaymentMethodNameAsync(ValidatePaymentMethodNameRequestModel request);
}

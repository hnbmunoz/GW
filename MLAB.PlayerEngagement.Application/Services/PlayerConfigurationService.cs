using MediatR;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Application.Services;

public class PlayerConfigurationService : IPlayerConfigurationService
{
    private readonly IMediator _mediator;
    private readonly ILogger<PlayerConfigurationService> _logger;
    private readonly IMainDbFactory _mainDbFactory;
    public readonly IPlayerConfigurationFactory _playerConfigurationFactory;

    public PlayerConfigurationService(IMediator mediator, ILogger<PlayerConfigurationService> logger, IMainDbFactory mainDbFactory, IPlayerConfigurationFactory playerConfigurationFactory)
    {
        _mediator = mediator;
        _logger = logger;
        _mainDbFactory = mainDbFactory;
        _playerConfigurationFactory = playerConfigurationFactory;
    }
    public async Task<Tuple<int, string>> ValidateVIPLevelNameAsync(string VIPLevelName)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (   DatabaseFactories.MLabDB,
                                StoredProcedures.USP_GetVIPLevelId,
                                 new
                                 {
                                     FilterValue = VIPLevelName
                                 }

                            ).ConfigureAwait(false);

            if (result != 0)
            {
                string errorMessage = String.Format($"VIP Level Name Already exist");
                return Tuple.Create(409, errorMessage);
            }

        }
        catch (Exception ex)
        {
            string errorDetail = ex.Message;
        }
        return Tuple.Create(200, "");
    }

    public async Task<bool> CheckExistingIDNameCodeListAsync(PlayerConfigCodeListValidatorRequestModel request)
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<int>
                (   DatabaseFactories.MLabDB,
                    StoredProcedures.USP_CheckExistingIDNameCodeList,
                    new
                    {
                        PlayerConfigurationTypeId = request.PlayerConfigurationTypeId,
                        PlayerConfigurationId = request.PlayerConfigurationId,
                        PlayerConfigurationName = request.PlayerConfigurationName,
                        PlayerConfigurationCode = request.PlayerConfigurationCode,
                        PlayerConfigurationICoreId = request.PlayerConfigurationICoreId,
                        PlayerConfigurationAction = request.PlayerConfigurationAction,
                        PlayerConfigurationBrandId = request.PlayerConfigurationBrandId,
                    }

                ).ConfigureAwait(false);

            return result == 1 ? true : false;
        }
        catch (Exception ex)
        {
            string errorDetail = ex.Message;
            return false;
        }
    }

    public async Task<bool> ValidatePlayerConfigurationRecordAsync(PlayerConfigurationRequestModel request)
    {
        return await _playerConfigurationFactory.ValidatePlayerConfigurationRecordAsync(request);
    }

    public async Task<List<TicketFieldsModel>> GetTicketFieldsList()
    {
        return await _playerConfigurationFactory.GetTicketFieldsList();
    }

    public async Task<bool> ValidatePaymentMethodCommunicationProviderAsync(ValidateCommunicationProviderRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | ValidatePaymentMethodCommunicationProviderAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (DatabaseFactories.MLabDB,
                                StoredProcedures.USP_ValidatePaymentMethodCommunicationProvider, new
                                {
                                    @PaymentMethodId = request.PaymentMethodExtId,
                                    MessageTypeId = request.MessageTypeId,
                                    ProviderAccount = request.ProviderAccount,
                                }

                            ).ConfigureAwait(false);
            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | ValidatePaymentMethodCommunicationProviderAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<bool> ValidatePaymentMethodNameAsync(ValidatePaymentMethodNameRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.UserFactor} | ValidatePaymentMethodNameAsync - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<bool>
                            (DatabaseFactories.MLabDB,
                                StoredProcedures.USP_ValidatePaymentMethodName, new
                                {
                                    @PaymentMethodId = request.PaymentMethodExtId,
                                    @PaymentMethodName = request.PaymentMethodName,
                                    @IcoreId = request.IcoreId,
                                }

                            ).ConfigureAwait(false);
            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.UserFactor} | ValidatePaymentMethodCommunicationProviderAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<List<LanguageModel>> GetLanguageOptionList()
    {
        return await _playerConfigurationFactory.GetLanguageOptionList();
    }
}

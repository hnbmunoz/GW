using MediatR;
using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Player.Request;
using MLAB.PlayerEngagement.Core.Models.Player.Response;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using MLAB.PlayerEngagement.Infrastructure.Config;

namespace MLAB.PlayerEngagement.Application.Services;

public  class PlayerManagementService : IPlayerManagementService
{

    private readonly ILogger<PlayerManagementService> _logger;
    private readonly IPlayerFactor _playerFactor;
    private readonly EmailConfig _emailConfig;
    private const int ContactLogLogUser = 2;
    private const int ContactLogLogTeam = 1;
    private const int ContactLogSummary = 0;

    public PlayerManagementService(IMediator mediator, ILogger<PlayerManagementService> logger, IPlayerFactor playerFactor, IConfiguration configuration, EmailConfig emailConfig)
    {
        _logger = logger;
        _playerFactor = playerFactor;
        Configuration = configuration;
        _emailConfig = emailConfig;
    }
    public IConfiguration Configuration { get; }

    public async Task<PlayerResponseModel> GetPlayerAsync(PlayerByIdRequestModel request)
    {
        var result = await _playerFactor.GetPlayerAsync(request);
        return result;
    }

    public async Task<List<LookupModel>> GetPlayerCampaignLookupsAsync(int? campaignType)
    {
        var result = await _playerFactor.GetPlayerCampaignLookupsAsync(campaignType);
        return result;
    }

    public async Task<PlayerCaseFilterResponseModel> GetPlayerCasesAsync(PlayerCaseRequestModel request)
    {
        var result = await _playerFactor.GetPlayerCasesAsync(request);
        return result;
    }

    public async Task<PlayerFilterResponseModel> GetPlayersAsync(PlayerFilterRequestModel request)
    {
        var result = await _playerFactor.GetPlayersAsync(request);
        return result;
    }

    public async Task<Tuple<int,ContactLogThresholdModel>> SavePlayerContactAsync(PlayerContactRequestModel request)
    {
        try
        {
            var result = await _playerFactor.SavePlayerContactAsync(request);
            if (result.Item2 != null)
            {

                string subject = "MLAB Notification - User reach the threshold with action to Send Email";
                if (result.Item2.ThresholdAction == "Send Email")
                {
                    subject = "MLAB Notification - User reach the threshold with action to Send Email";
                }
                else if (result.Item2.ThresholdAction == "Deactivate User Account")
                {
                    subject = "	MLAB Notification - User reach the threshold with action to Deactivate User Account";
                }
                EmailRequestModel emailRequest = new EmailRequestModel()
                {
                    Content = result.Item2.EmailContent,
                    UserEmail = result.Item2.EmailRecipient,
                    EmailType = EmailType.emailCreate,
                    Subject = subject,
                    From = _emailConfig.Email,
                    CC = _emailConfig.Cc,
                    BCC = _emailConfig.Bcc,
                    IsSMTPWithAuth = Convert.ToBoolean(_emailConfig.IsSMTPWithAuth),
                    Email = _emailConfig.Email,
                    SmtpHost = _emailConfig.SmtpHost,
                    Port = Convert.ToInt32(_emailConfig.Port),
                    Password = _emailConfig.Password
                };

                EmailHelper.ProcessMail(emailRequest);
            }

            return Tuple.Create(result.Item1, result.Item2); 
        }
        catch (Exception ex)
        {
            _logger.LogError($"PlayerManagementService | SavePlayerContactAsync Exception: { ex.InnerException}");
        }
        return Tuple.Create(0, Enumerable.Empty<ContactLogThresholdModel>().FirstOrDefault());
    }

    public async Task<Tuple<int, string>> ValidateCaseCampaignPlayerAsync(CaseCampaigndPlayerIdRequest request)
    {
        var results = await _playerFactor.ValidateCaseCampaignPlayerAsync(request);
        if (results != 0)
        {
            string errorMessage = "Unable to proceed, player already have case record on this campaign";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, ""); 
    }

    public async Task<ContactLogSummaryResponseModel> GetViewContactLogListAsync(ContactLogListRequestModel request)
    { 
        var result = await _playerFactor.GetViewContactLogAsync<ContactLogSummaryModel>(request,ContactLogSummary);
        var response = new ContactLogSummaryResponseModel
        {
            ContactLogSummaryList = result.Item2,
            RecordCount = result.Item1
        };

        return response;
    }

    public async Task<ContactLogTeamResponseModel> GetViewContactLogTeamListAsync(ContactLogListRequestModel request)
    {
        var result = await _playerFactor.GetViewContactLogAsync<ContactLogTeamModel>(request,ContactLogLogTeam);

        var response = new ContactLogTeamResponseModel
        {
            ContactLogTeamList = result.Item2,
            RecordCount = result.Item1
        };

        return response;
    }

    public async Task<ContactLogUserResponseModel> GetViewContactLogUserListAsync(ContactLogListRequestModel request)
    {
        var result = await _playerFactor.GetViewContactLogAsync<ContactLogUserModel>(request, ContactLogLogUser);

        var response = new ContactLogUserResponseModel
        {
            ContactLogUserList = result.Item2,
            RecordCount = result.Item1
        };

        return response;
    }

    public async Task<List<GetManageThresholdsResponse>> GetManageThresholdsAsync()
    {
        var result = await _playerFactor.GetManageThresholdsAsync();
        return result;
    }

    public async Task<PlayerSensitiveDataResponseModel> GetPlayerSensitiveDataAsync(PlayerSensitiveDataRequestModel request)
    {
        var getPlayerSensitiveDataResult = await _playerFactor.GetPlayerSensitiveDataAsync(request);
        return getPlayerSensitiveDataResult;
    }
}

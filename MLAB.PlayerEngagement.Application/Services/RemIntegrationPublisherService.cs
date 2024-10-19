using MediatR;
using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagementIntegration;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class RemIntegrationPublisherService : IRemIntegrationPublisherService
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly bool _isRemIntegrationEnabled;
    private readonly string _rabbitEnvironment;
    private readonly string _exchangeBinding = "?bind=true&";
    private readonly string _remIntegarationExchangeUri;

    public RemIntegrationPublisherService(IMediator mediator, ILogger logger, IConfiguration configuration)
    {
        _mediator = mediator;
        _logger = logger;
        _configuration = configuration;
        _isRemIntegrationEnabled = bool.Parse(_configuration["IsRemIntegrationEnabled"]);
        _rabbitEnvironment = _configuration.GetConnectionString("RabbitEnvironment");
        _remIntegarationExchangeUri = Exchanges.RemIntegration + _rabbitEnvironment + _exchangeBinding + QueueNames.remIntegrationQueue + _rabbitEnvironment;
    }
    public async Task<bool> SendUpdateRemProfile(RemProfileEventRequestModel request)
    {
        string remarks = "Sending Rem Profile Data to ICore";
        string eventName = Convert.ToString(RemIntegrationEvents.UpdateRemProfile);
        string exchangeUri = _remIntegarationExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SendSetOnlineStatus(RemOnlineStatusRequestModel request)
    {
        string remarks = "Sending Set Online Status to ICore";
        string eventName = Convert.ToString(RemIntegrationEvents.SetRemOnlineStatus);
        string exchangeUri = _remIntegarationExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SendUpdateRemSetting(RemUpdateSettingRequestModel request)
    {
        string remarks = "Sending Update Rem Setting to ICore";
        string eventName = Convert.ToString(RemIntegrationEvents.UpdateRemSetting);
        string exchangeUri = _remIntegarationExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SendAssignPlayerToRemProfile(RemDistributionEventRequestModel request)
    {
        string remarks = "Sending Assign Rem Distribution Data to ICore";
        string eventName = Convert.ToString(RemIntegrationEvents.Assign);
        string exchangeUri = _remIntegarationExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SendRemovePlayerFromRemProfile(RemDistributionEventRequestModel request)
    {
        string remarks = "Sending Remove Rem Distribution Data to ICore";
        string eventName = Convert.ToString(RemIntegrationEvents.Remove);
        string exchangeUri = _remIntegarationExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> SendReassignPlayerFromRemProfile(RemDistributionEventRequestModel request)
    {
        string remarks = "Sending Reassign Rem Distribution Data to ICore";
        string eventName = Convert.ToString(RemIntegrationEvents.Reassign);
        string exchangeUri = _remIntegarationExchangeUri;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    #region PrivateMethods
    private async Task<bool> BuildPublishMessage<T>(T userRequest, string queueId, string userId, string remarks, string eventName, string exchangeUri)
    {
        _logger.LogInfo($"Feature Flag [ IsRemIntegrationEnabled ]= {_isRemIntegrationEnabled}");

        if (_isRemIntegrationEnabled)
        {
            string redisCacheRequestId = Guid.NewGuid().ToString();

            var createMemoryCacheCommand = new CreateMemoryCacheCommand
            {
                Id = redisCacheRequestId,
                Data = userRequest

            };

            var resultCreateMemoryCacheCommand = await _mediator.Send(createMemoryCacheCommand);

            if (resultCreateMemoryCacheCommand.result)
            {
                var createQueueCommand = new CreateQueueCommand
                {
                    QueueId = Guid.Parse(queueId),
                    QueueName = eventName,
                    CreatedBy = userId,
                    RedisCacheRequestId = Guid.Parse(redisCacheRequestId),
                    QueueStatus = Convert.ToString(QueueStatus.PUBLISHED),
                    ServiceTypeId = Guid.Parse(_configuration.GetConnectionString("ServiceTypeId")),
                    Remarks = remarks,
                    Action = eventName,
                    UserId = userId
                };

                var resultCreateQueueCommand = await _mediator.Send(createQueueCommand);

                _logger.LogInfo(
                    $"RemIntegrationPublisherService | BuildPublishMessage | {eventName} : [Received {queueId}] - {resultCreateQueueCommand.result}");

                var createQueuePublishCommand = new CreateQueuePublishCommand
                {
                    Id = Guid.Parse(queueId),
                    Name = eventName,
                    Action = eventName,
                    CacheId = Guid.Parse(redisCacheRequestId),
                    ExchangeUri = exchangeUri,
                    CallbackUri = _configuration.GetConnectionString("CallbackUrl"),
                    QueueStatus = Convert.ToString(QueueStatus.PUBLISHED),
                    UserId = userId,
                    Remarks = remarks
                };
                var resultCreateQueuePublishCommand = await _mediator.Send(createQueuePublishCommand);

                _logger.LogInfo(
                    $"RemIntegrationPublisherService | BuildPublishMessage | {eventName} : [Response {queueId}] - {resultCreateQueuePublishCommand.result}");

                return true;
            }

            else
            {
                _logger.LogError(
                    $"RemIntegrationPublisherService | BuildPublishMessage | {eventName} : [Exception {queueId}] - Problem saving data in redis cache");
                return false;
            }
        }

        return false;
    }
    #endregion
}

using MediatR;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Application.Commands;
using Constants = MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Services;
using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Models.CampaignTaggingPointSetting;
using MLAB.PlayerEngagement.Core.Logging.Extensions;

namespace MLAB.PlayerEngagement.Application.Services;

public class CampaignTaggingPointSettingService: ICampaignTaggingPointSettingService
{
    private readonly IMediator _mediator;
    private readonly ILogger<MessagePublisherService> _logger;
    private readonly ICampaignTaggingPointSettingFactory _campaignSettingFactory;
    private string rabbitEnvironment = string.Empty;
    private readonly string exchangeBinding = "?bind=true&";

    public CampaignTaggingPointSettingService(IMediator mediator, IConfiguration configuration, ILogger<MessagePublisherService> logger, ICampaignTaggingPointSettingFactory campaignSettingFactory)
    {
        _mediator = mediator;
        Configuration = configuration;
        _logger = logger;
        _campaignSettingFactory = campaignSettingFactory;
        rabbitEnvironment = Configuration.GetConnectionString("RabbitEnvironment");
    }

    public IConfiguration Configuration { get; }

    //  THIS METHOD USES MASTERREFERENCE TABLE. CAN BE USED TO ALL DROPDOWNS, JUST ASSIGN MasterReferenceId, MasterReferenceIsParent params -  
    public async Task<List<CampaignSettingMasterReference>> GetCampaignSettingTypeSelectionAsync(int masterReferenceId, int masterReferenceIsParent)
    {
        var results = await _campaignSettingFactory.GetCampaignSettingTypeSelectionAsync(masterReferenceId, masterReferenceIsParent);
        return results;
    }

    public async Task<List<SegmentSelectionModel>> GetTaggingSegmentAsync()
    {
        var results = await _campaignSettingFactory.GetTaggingSegmentAsync();
        return results;
    }

    public async Task<List<UsersSelectionModel>> GetUsersByModuleAsync(int subMainModuleDetailId)
    {
        List<UsersSelectionModel> usersSelectionModels = new List<UsersSelectionModel>();
        var results = await _campaignSettingFactory.GetUsersByModuleAsync(subMainModuleDetailId);


        var response = results.GroupBy(item => new 
                        {
                            UserId = item.UserId,
                            FullName = item.FullName,
                            Email = item.Email,
                            Status = item.Status
                        }, y => y).Select(s => s.First()).Where(t=> t.Status == 1).ToList();

        foreach (var item in response)
        {
            var users = new UsersSelectionModel
            {
                UserId = item.UserId,
                FullName = item.FullName,
                Email = item.Email,
                Status = item.Status
            };

            usersSelectionModels.Add(users);
        }


        return usersSelectionModels;
    }

    //  GET CAMPAIGN RESULTS FOR AUTO TAGGING AND POINT INCENTIVE SETTING (GOAL PARAMETER AND POINT TO VALUE)
    public async Task<bool> GetCampaignSettingListAsync(AutoTaggingPointIncentiveFilterRequestModel request)
        {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Constants.Actions.GetCampaignSettingListAsync);
        string exchangeUri = Constants.Exchanges.CampaignSetting + rabbitEnvironment + exchangeBinding + Constants.QueueNames.campaignSettingQueue + rabbitEnvironment;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    //  GET AUTO TAGGING DETAILS BASED FROM SELECTED CAMPAIGN_SETTING_ID - FOR VIEW AND EDIT
    public async Task<bool> GetAutoTaggingDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Constants.Actions.GetAutoTaggingDetailsByIdAsync);
        string exchangeUri = Constants.Exchanges.CampaignSetting + rabbitEnvironment + exchangeBinding + Constants.QueueNames.campaignSettingQueue + rabbitEnvironment;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    //  GET POINT INCENTIVE DETAILS BASED FROM SELECTED CAMPAIGN_SETTING_ID - FOR VIEW AND EDIT
    public async Task<bool> GetPointIncentiveDetailsByIdAsync(AutoTaggingFilterByIdRequestModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Constants.Actions.GetPointIncentiveDetailsByIdAsync);
        string exchangeUri = Constants.Exchanges.CampaignSetting + rabbitEnvironment + exchangeBinding + Constants.QueueNames.campaignSettingQueue + rabbitEnvironment;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }



    public async Task<bool> GetPointIncentiveDetailsByIdAsync(PointIncentiveDetailsByIdRequestModel request)
    {
        string remarks = "Get Message Resonse";
        string eventName = Convert.ToString(Constants.Actions.GetPointIncentiveDetailsByIdAsync);
        string exchangeUri = Constants.Exchanges.SystemConfiguration + rabbitEnvironment + exchangeBinding + Constants.QueueNames.systemConfigurationQueue + rabbitEnvironment;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }


    public async Task<bool> AddAutoTaggingListAsync(AutoTaggingDetailsRequestModel request)
    {
        string remarks = "Add Auto Tagging Details";
        string eventName = Convert.ToString(Constants.Actions.AddAutoTaggingListAsync);
        string exchangeUri = Constants.Exchanges.CampaignSetting + rabbitEnvironment + exchangeBinding + Constants.QueueNames.systemConfigurationQueue + rabbitEnvironment;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    public async Task<bool> AddPointIncentiveSettingAsync(AddPointIncentiveDetailsRequestModel request)
    {
        string remarks = "Add Point Incentive Details";
        string eventName = Convert.ToString(Constants.Actions.AddPointIncentiveSettingAsync);
        string exchangeUri = Constants.Exchanges.CampaignSetting + rabbitEnvironment + exchangeBinding + Constants.QueueNames.systemConfigurationQueue + rabbitEnvironment;

        return await BuildPublishMessage(request, request.QueueId, request.UserId, remarks, eventName, exchangeUri);
    }

    
    private async Task<bool> BuildPublishMessage<T>(T userRequest, string queueId, string userId, string remarks, string eventName, string exchangeUri)
    {

        string redisCacheRequestId = Guid.NewGuid().ToString();

        var createMemoryCacheCommand = new CreateMemoryCacheCommand
        {
            Id = redisCacheRequestId,
            Data = userRequest

        };

        var resultCreateMemoryCacheCommand = await _mediator.Send(createMemoryCacheCommand);

        if (resultCreateMemoryCacheCommand.result == true)
        {
            var createQueueCommand = new CreateQueueCommand
            {
                QueueId = Guid.Parse(queueId),
                QueueName = eventName,
                CreatedBy = userId,
                RedisCacheRequestId = Guid.Parse(redisCacheRequestId),
                QueueStatus = Convert.ToString(Constants.QueueStatus.PUBLISHED),
                ServiceTypeId = Guid.Parse(Configuration.GetConnectionString("ServiceTypeId")),
                Remarks = remarks,
                Action = eventName,
                UserId = userId
            };

            var resultCreateQueueCommand = await _mediator.Send(createQueueCommand);

            _logger.LogInfo($"{Constants.Services.CampaignTaggingPointSettingService} | BuildPublishMessage : [Received {queueId}] - {resultCreateQueueCommand.result}");

            var createQueuePublishCommand = new CreateQueuePublishCommand
            {
                Id = Guid.Parse(queueId),
                Name = eventName,
                Action = eventName,
                CacheId = Guid.Parse(redisCacheRequestId),
                ExchangeUri = exchangeUri,
                CallbackUri = Configuration.GetConnectionString("CallbackUrl"),
                QueueStatus = Convert.ToString(Constants.QueueStatus.PUBLISHED),
                UserId = userId,
                Remarks = remarks
            };
            var resultCreateQueuePublishCommand = await _mediator.Send(createQueuePublishCommand);

            _logger.LogInfo($"{Constants.Services.CampaignTaggingPointSettingService} | BuildPublishMessage : [Response {queueId}] - {resultCreateQueuePublishCommand.result}");

            return true;
        }

        else
        {
            _logger.LogError($"{Constants.Services.CampaignTaggingPointSettingService} | BuildPublishMessage : [Exception {queueId}] - Problem saving data in redis cache");
            return false;
        }

    }

    public async Task<bool> CheckCampaignSettingByNameIfExistAsync(CampaignSettingNameRequestModel request)
    {
        var result = await _campaignSettingFactory.CheckCampaignSettingByNameIfExistAsync(request);
        return result;
    }

}

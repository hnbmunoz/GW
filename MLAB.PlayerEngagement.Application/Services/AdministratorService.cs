using MediatR;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Administrator;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;

namespace MLAB.PlayerEngagement.Application.Services;

public class AdministratorService : IAdminstratorService
{
    private readonly IMediator _mediator;
    private readonly ILogger<AdministratorService> _logger;
    private readonly IAdministratorFactory _adminFactory;
    private readonly IMessagePublisherService _messagePublisherService;
    public AdministratorService(IMediator mediator, ILogger<AdministratorService> logger, IAdministratorFactory adminFactory, IMessagePublisherService messagePublisherService)
    {
        _mediator = mediator;
        _logger = logger;
        _adminFactory = adminFactory;
        _messagePublisherService = messagePublisherService;
    }

    public async Task<QueueRequestResponse> GetQueueRequestAsync(QueueFilterRequestModel queueFilter)
    {
        try
        {
            var result = await _adminFactory.GetQueueRqstLstAsync(queueFilter);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Service | Administrator.GetQueueRequestAsync: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }

        return Enumerable.Empty<QueueRequestResponse>().First();
    }

    public async Task<QueueHistoryResponse> GetQueueHistoryAsync(QueueFilterRequestModel queueFilter)
    {
        try
        {
            var result = await _adminFactory.GetQueueHstryLst(queueFilter);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Service | Administrator.GetQueueHistoryAsync: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }

        return Enumerable.Empty<QueueHistoryResponse>().First();
    }

    public async Task<List<QueueStatusResponse>> GetDistinctQueueStatus()
    {
        try
        {
            var result = await _adminFactory.GetDistinctQueueStatus();
            return result;
        }
        catch (Exception ex)
        { 
           _logger.LogInfo("Gateway Service | Administrator.GetDistinctQueueStatus: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }

        return Enumerable.Empty<QueueStatusResponse>().ToList();
    }

    public async Task<List<QueueActionResponse>> GetDistinctQueueActions()
    {
        try
        {
            var result = await _adminFactory.GetDistinctQueueActions();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Service | Administrator.GetDistinctQueueActions: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }

        return Enumerable.Empty<QueueActionResponse>().ToList();
    }

    public async Task<QueueCountResponse> DeleteQueueByCreatedDateRange(DeleteQueueRequestModel queueFilter)
    {
        try
        {
            var result = await _adminFactory.DeleteQueueByCreatedDateRange(queueFilter);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogInfo("Gateway Service | Administrator.DeleteQueueByCreatedDateRange: Exception:" + ex.InnerException + "| Message: " + ex.Message);
        }

        return Enumerable.Empty<QueueCountResponse>().FirstOrDefault();
    }
}

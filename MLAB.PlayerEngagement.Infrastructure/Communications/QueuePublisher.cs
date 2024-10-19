using MassTransit;
using Microsoft.Extensions.Options;
using MLAB.PlayerEngagement.Core.Communications;
using MLAB.PlayerEngagement.Core.Entities;
using MLAB.PlayerEngagement.Infrastructure.Config;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;

namespace MLAB.PlayerEngagement.Infrastructure.Communications;

public class QueuePublisher : IQueuePublisher
{
    private readonly IBus _bus;
    private readonly ILogger<QueuePublisher> _logger;
    public QueuePublisher(IOptions<ConnectionString> config, IBus bus, ILogger<QueuePublisher> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task<bool> SendQueueAsync(string exchangeUri, ExchangeQueue exchangeQueue)
    {
        if (exchangeQueue is not null)
        {
            try
            {
                var endPoint = await _bus.GetSendEndpoint(new Uri(exchangeUri));                    
                await endPoint.Send(exchangeQueue);
                return true;
            } catch(Exception ex)
            {
                _logger.LogError("Problem sending in queue : ", ex);
                return false;
            }         
        }
        else
        {
            return false;
        }
    }
}

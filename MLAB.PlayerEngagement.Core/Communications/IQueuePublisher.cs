using MLAB.PlayerEngagement.Core.Entities;

namespace MLAB.PlayerEngagement.Core.Communications;

public interface IQueuePublisher
{
    Task<bool> SendQueueAsync(string exchangeUri, ExchangeQueue exchangeQueue);
}

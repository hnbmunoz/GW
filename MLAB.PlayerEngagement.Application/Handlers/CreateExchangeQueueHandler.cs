using MediatR;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Application.Mappers;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Communications;
using MLAB.PlayerEngagement.Core.Entities;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;

namespace MLAB.PlayerEngagement.Application.Handlers;

public class CreateExchangeQueueHandler : IRequestHandler<CreateQueuePublishCommand, ExchangeResponse>
{
    private readonly IQueuePublisher _queuePublisher;
    private readonly ILogger<CreateExchangeQueueHandler> _logger;
    public CreateExchangeQueueHandler(IQueuePublisher queuePublisher, ILogger<CreateExchangeQueueHandler> logger)
    {
        _queuePublisher = queuePublisher;
        _logger = logger;
    }
    public async Task<ExchangeResponse> Handle(CreateQueuePublishCommand request, CancellationToken cancellationToken)
    {
        var exchangeEntitiy = ExchangeQueueMapper.Mapper.Map<ExchangeQueue>(request);
        var isSuccess = await _queuePublisher.SendQueueAsync(request.ExchangeUri, exchangeEntitiy);
        _logger.LogError("CreateExchangeQueueHandler result: " + isSuccess);
        var response = new ExchangeResponse { result = isSuccess };
        return response;
    }
}
